using System.Collections.Generic;

namespace Tamagotchi.Application.Exceptions;

public class Response<T>
{
    public IList<T> errors { get; set; } = new List<T>();
}