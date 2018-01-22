using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RH.Domain.CommandHandlers.Commands
{
    public class CandidateTechDeleteCommand : IRequest<ICommandResult>
    {
        public CandidateTechDeleteCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
