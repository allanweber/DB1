﻿using MediatR;
using RH.Domain.Core.Entities;

namespace RH.Domain.CommandHandlers.Commands
{
    public class OpportunityTechDeleteCommand: BaseEntity, IRequest<ICommandResult>
    {
    }
}
