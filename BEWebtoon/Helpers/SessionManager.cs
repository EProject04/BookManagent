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

        public void SetSessionValueInt(string key, int value)
        {
            _httpContextAccessor.HttpContext.Session.SetInt32(key, value);
        }

        public int GetSessionValueInt(string key)
        {
            return (int) _httpContextAccessor.HttpContext.Session.GetInt32(key);
        }
        public void Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear(); 
        }
        public bool CheckRole(string[] allowedRoleIds)
        {
            var checkRoleId = GetSessionValue("RoleId");

            if (allowedRoleIds.Contains(checkRoleId))
            {
                return true;
            }
            else
            {
                throw new CustomException("Ban chua duoc phan quyen");
            }
        }
    }
}
