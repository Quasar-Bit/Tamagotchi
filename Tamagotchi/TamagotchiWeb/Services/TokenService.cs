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

        private readonly IAppSettingsRepository _appSettingsRepository;

        public TokenService(IAppSettingsRepository appSettingsRepository)
        {
            _appSettingsRepository = appSettingsRepository;
        }

        public async Task<bool> GetPetFinderToken()
        {
            var petFinderToken = await MakeApiCall<PetFinderToken>(GetPetFinderTokenSegment, HttpMethod.Post);
            if(petFinderToken == null)
                return await Task.FromResult(false);

            var token = await _appSettingsRepository.GetChangeTrackingQuery().FirstOrDefaultAsync(x => x.Name == "PetFinderToken", new CancellationToken());
            if (token == null)
            {
                _appSettingsRepository.AddAsync(new AppSetting
                {
                    Name = "PetFinderToken",
                    Value = petFinderToken.Data.access_token
                }, new CancellationToken());
            }
            else
            {
                token.Value = petFinderToken.Data.token_type + " " + petFinderToken.Data.access_token;
                var result = _appSettingsRepository.Update(token);
            }

            var answer = await _appSettingsRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());

            return await Task.FromResult(answer > 0);
        }
    }
}