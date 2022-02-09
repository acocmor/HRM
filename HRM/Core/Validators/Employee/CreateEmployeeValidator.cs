using System;
using FluentValidation;
using HRM.API.Core.Validators.Address;
using HRM.Models.Employee;
using HRM.Models.User;

namespace HRM.Core.Validators.Employee
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDTO>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(o => o.LastName)
                .NotEmpty().WithMessage("Hãy nhập địa chỉ email.");

            RuleFor(o => o.DayOfBirth)
                .NotEmpty().WithMessage("Hãy nhập ngày sinh.");

            RuleFor(o => o.MonthOfBirth)
                .NotEmpty().WithMessage("Hãy nhập tháng.")
                .Must(month => month >= 1 && month <= 12).WithMessage("Tháng nhập vào không hợp lệ");
            
            RuleFor(o => o.YearOfBirth)
                .NotEmpty().WithMessage("Hãy nhập năm.");

            RuleFor(o => o).Must(o =>
            {
                try
                {
                    DateTime mydate = new DateTime(o.YearOfBirth, o.MonthOfBirth, o.DayOfBirth);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }).WithMessage("Ngày, tháng, năm sinh không hợp lệ");
            
            RuleFor(o => o.GenderId)
                .NotEmpty().WithMessage("Hãy chọn giới tính.");
            
            
            
            RuleFor(x => x.Address).SetValidator(new CreateAddressValidator());
            
        }
    }
}