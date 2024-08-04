using Microsoft.AspNetCore.Http;

namespace DigitalStore.Base
{
    public interface ISessionContext
    {
        public HttpContext HttpContext { get; set; }
        public Session Session { get; set; }
    }
}
