namespace BEWebtoon.Helpers
{
    public class SessionManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetSessionValue(string key)
        {
            return _httpContextAccessor.HttpContext.Session.GetString(key);
        }

        public void SetSessionValue(string key, string value)
        {
            _httpContextAccessor.HttpContext.Session.SetString(key, value);
        }
        public void Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear(); 
        }
    }
}
