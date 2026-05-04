using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIO.Application.Features.Projects.Commands.DeleteProject
{
    // Command này không trả về gì — IRequest (không có type parameter)
    public record DeleteProjectCommand(string ProjectId) : IRequest;
}
