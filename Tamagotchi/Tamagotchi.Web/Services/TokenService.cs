using Tamagotchi.Web.Services.Base;
using Tamagotchi.Web.Services.DTOs.OutPut;
using Tamagotchi.Web.Services.Interfaces;
using MediatR;
using Tamagotchi.Application.Settings.Queries.GetAll.DTOs;
using Tamagotchi.Application.Settings.Commands.Update.DTOs;
using MapsterMapper;

namespace Tamagotchi.Web.Services
{
    public class TokenService : BaseService, ITokenService
    {
        private const string PageSegment = "oauth2/token";
        private const string GetPetFinderTokenSegment = Constants.BaseApiController + PageSegment;

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public TokenService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<bool> GetPetFinderToken()
        {
            var petFinderToken = await MakeApiCall<PetFinderToken>(GetPetFinderTokenSegment, HttpMethod.Post);
            if(petFinderToken == null)
                return await Task.FromResult(false);

            var token = await _mediator.Send(new GetAppSettingsQuery { Name = "PetFinderToken" });
            token.Value = petFinderToken.Data.access_token;
            await _mediator.Send(_mapper.Map<UpdateAppSettingsCommand>(token));

            return await Task.FromResult(true);
        }
    }
}