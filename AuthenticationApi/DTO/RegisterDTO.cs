namespace AuthenticationApi.DTO
{
    public class RegisterDTO : UserDTO
    {
        public string Badge { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Guid Tenant { get; set; }
        public string Password { get; set; } = null!;
    }
}
