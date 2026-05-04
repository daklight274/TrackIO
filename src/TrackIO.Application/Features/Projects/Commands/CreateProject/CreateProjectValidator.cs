using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIO.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên project không được để trống")
                .MinimumLength(3).WithMessage("Tên project tối thiểu 3 ký tự")
                .MaximumLength(100).WithMessage("Tên project tối đa 100 ký tự");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả tối đa 500 ký tự")
                .When(x => x.Description is not null);
        }
    }
}
