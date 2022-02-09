
using FluentValidation;
using HRM.Entity.Constracts;
using HRM.Models.User;

namespace HRM.API.Core.Validators.User
{
    public class CreateUserValidator : AbstractValidator<CreateUserDTO>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            //validation email
            RuleFor(o => o.Email)
                .NotEmpty().WithMessage("Hãy nhập địa chỉ email.")
                .EmailAddress().WithMessage("Địa chỉ email nhập vào không đúng định dạng.");

            //validation password
            RuleFor(o => o.Password)
                .MaximumLength(32).WithMessage("Độ dài mật khẩu phải từ 8 đến 32 ký tự.")
                .MinimumLength(8).WithMessage("Độ dài mật khẩu phải từ 8 đến 32 ký tự.")
                .Matches("[A-Z]").WithMessage("Mật khẩu phải chứa ít nhất một chữ cái viết hoa.")
                .Matches("[a-z]").WithMessage("Mật khẩu phải chứa ít nhất một chữ cái viết thường.")
                .Matches(@"\d").WithMessage("Mật khẩu phải chứa ít nhất một chữ số.")
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Mật khẩu phải chứa ít nhất một ký tự đặc biệt.")
                .Matches("^[^£# “”]*$").WithMessage("Mật khẩu không được chứa các ký tự không cho phép: khoảng trắng, '£', '#', '“”'.")
                .NotEmpty().WithMessage("Hãy nhập mật khẩu");

            //validation confirm passwod
            RuleFor(o => o.ConfirmPassword)
                .MaximumLength(32).WithMessage("Độ dài mật khẩu phải từ 8 đến 32 ký tự")
                .MinimumLength(8).WithMessage("Độ dài mật khẩu phải từ 8 đến 32 ký tự")
                .NotEmpty().WithMessage("Hãy xác nhận mật khẩu.")
                .Equal(o => o.Password).WithMessage("Xác nhận mật khẩu không đúng");
        }
        
        public bool HaveUniqueNumber(string email)
        {
            var result = _userRepository.GetByEmail(email);
            return result != null;
            //return true;
        }
    }
}