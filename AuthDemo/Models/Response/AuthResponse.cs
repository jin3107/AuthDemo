namespace AuthDemo.Models.Response
{
    public class AuthResponse
    {
        public string? AccessToken { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
