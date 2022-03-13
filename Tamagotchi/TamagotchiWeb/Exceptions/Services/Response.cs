using System.Collections.Generic;

namespace TamagotchiWeb.Exceptions
{
    public class Response<T>
    {
        public IList<T> errors { get; set; } = new List<T>();
    }
}