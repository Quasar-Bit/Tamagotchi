namespace TamagotchiWeb.Services.Interfaces
{
    public interface ITokenService
    {
        Task<bool> GetPetFinderToken();
    }
}