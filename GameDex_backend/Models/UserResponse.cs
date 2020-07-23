namespace GameDex_backend.Models
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Auth_token { get; set; }
    }
}