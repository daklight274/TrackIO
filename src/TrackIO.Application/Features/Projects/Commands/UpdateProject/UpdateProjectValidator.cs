using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIO.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .When(x => x.Description is not null);
        }
    }
}
