using System.Net;

namespace Tamagotchi.Web.Models
{
    public class Result<T>
    {
        public HttpStatusCode Status { get; set; }
        public T Data { get; set; }
    }
}