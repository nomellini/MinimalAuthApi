using AuthApi.Interfaces;

namespace AuthApi.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }   

        public void ClearToken()
        {
            _httpContextAccessor
                .HttpContext?
                .Response
                .Cookies
                .Delete("JWToken");
        }

        public string? GetToken()
        {
            string? token = null;

            bool? hasToken =
                _httpContextAccessor
                    .HttpContext?
                    .Request
                    .Cookies
                    .TryGetValue("JWToken", out token);

            return hasToken == true ? token : null;
        }

        public void SetToken(string token)
        {

            _httpContextAccessor
                .HttpContext?
                .Response
                .Cookies
                .Append("JWToken", token, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1),
                        HttpOnly = true
                    });
        }
    }
}
