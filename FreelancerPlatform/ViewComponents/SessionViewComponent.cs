using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace YourProjectName.ViewComponents
{
    public class SessionViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionViewComponent(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            var sessionValue = _httpContextAccessor.HttpContext.Session.GetString("sessionKey");
            return View("Session", sessionValue);
        }
    }
}
