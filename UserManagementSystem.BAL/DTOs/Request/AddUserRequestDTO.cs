using FluentValidation;

namespace UserManagementSystem.BAL.DTOs.Request
{
    public class AddUserRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }

        public class AddUserValidator : AbstractValidator<AddUserRequestDTO>
        {
            public AddUserValidator()
            {
                RuleFor(x => x.PersonalNumber).NotEmpty().MaximumLength(11);
            }
        }
    }
}