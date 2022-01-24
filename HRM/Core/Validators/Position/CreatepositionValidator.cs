using FluentValidation;
using HRM.Models.Position;

namespace HRM.Core.Validators.Position
{
    public class CreatepositionValidator : AbstractValidator<CreatePositionDTO>
    {
        public CreatepositionValidator()
        {
            //validation name
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Hãy nhập đủ thông tin")
                .MinimumLength(3).WithMessage("Độ dài tên phòng ban từ 3 đến 100 ký tự")
                .MaximumLength(100).WithMessage("Độ dài tên phòng ban từ 3 đến 100 ký tự")
                .Matches("^[^£#!@$%^&*{}“”]*$").WithMessage("Tên không được chứa ký tự đặc biệt.");
        }
    }
}