using TamagotchiWeb.Services.Base;
using TamagotchiWeb.Models;
using TamagotchiWeb.Services.DTOs.OutPut;
using TamagotchiWeb.Services.Interfaces;
using Tamagotchi.Data.Entities;
using Tamagotchi.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TamagotchiWeb.Services
{
    public class TokenService : BaseService, ITokenService
    {
        private const string PageSegment = "oauth2/token";
        private const string GetPetFinderTokenSegment = Constants.BaseApiController + PageSegment;

        private readonly IAppSettingRepository _appSettingRepository;

        public TokenService(IAppSettingRepository appSettingRepository)
        {
            _appSettingRepository = appSettingRepository;
        }

        public async Task<bool> GetPetFinderToken()
        {
            var petFinderToken = await MakeApiCall<PetFinderToken>(GetPetFinderTokenSegment, HttpMethod.Post);
            if(petFinderToken == null)
                return await Task.FromResult(false);

            var token = await _appSettingRepository.GetChangeTrackingQuery().FirstOrDefaultAsync(x => x.Name == "PetFinderToken", new CancellationToken());
            if (token == null)
            {
                _appSettingRepository.AddAsync(new AppSetting
                {
                    Name = "PetFinderToken",
                    Value = petFinderToken.Data.access_token
                }, new CancellationToken());
            }
            else
            {
                token.Value = petFinderToken.Data.access_token;
                var result = _appSettingRepository.Update(token);
            }

            var answer = await _appSettingRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());

            return await Task.FromResult(answer > 0);
        }
    }
}