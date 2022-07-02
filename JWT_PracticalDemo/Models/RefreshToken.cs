namespace JWT_PracticalDemo.Models
{
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime ExpirationDate { get; set; }
    }
}
