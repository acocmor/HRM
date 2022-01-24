using FluentValidation;
using HRM.Models.Address;

namespace HRM.API.Core.Validators.Address
{
    public class UpdateAddressValidator : AbstractValidator<UpdateAddressDTO>
    {
        public UpdateAddressValidator()
        {
            RuleFor(x => x.Detail)
                .Matches(@"[a-zA-Z0-9]").WithMessage("Thông tin nhập vào chứa ký tự không hợp lệ"); 

            RuleFor(x => x.SubDistrict)
                .NotEmpty().WithMessage("Thông tin Xã/Phường không được để trống")
                .Matches(@"[a-zA-Z0-9]").WithMessage("Thông tin nhập vào chứa ký tự không hợp lệ");

            RuleFor(x => x.SubDistrict)
                .NotEmpty().WithMessage("Thông tin Quận/Huyện không được để trống")
                .Matches(@"[a-zA-Z0-9]").WithMessage("Thông tin nhập vào chứa ký tự không hợp lệ");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Thông tin Tỉnh/Thành phố không được để trống")
                .Matches(@"[a-zA-Z0-9]").WithMessage("Thông tin nhập vào chứa ký tự không hợp lệ");
        }
    }
}