namespace MyProject.Application.DTOs.AppUserDto
{
    public class ChangeUserPasswordDto
    {
        public string Email { get; set; }
        public string newPassword { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
