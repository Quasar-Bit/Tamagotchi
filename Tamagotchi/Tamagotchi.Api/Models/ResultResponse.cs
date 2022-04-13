using System.Text.Json;

namespace Tamagotchi.Api.Models;

public class ResultResponse<T>
{
    #region Constructors

    public ResultResponse()
    {
        Model = default;
        Error = null;
    }

    #endregion

    #region Get methods

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    #endregion

    #region Variables

    public T Model { get; set; }
    public string Error { get; set; }

    #endregion
}