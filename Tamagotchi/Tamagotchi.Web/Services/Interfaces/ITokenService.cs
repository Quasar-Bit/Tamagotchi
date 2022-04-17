namespace Tamagotchi.Web.Services.Interfaces
{
    public interface ITokenService
    {
        Task<bool> GetPetFinderToken();
    }
}