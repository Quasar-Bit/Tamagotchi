using System.Net;

namespace TamagotchiWeb.Models
{
    public class Result<T>
    {
        public HttpStatusCode Status { get; set; }
        public T Data { get; set; }
    }
}