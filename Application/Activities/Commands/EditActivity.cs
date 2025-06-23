using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Activities.Commands
{
    public class EditActivity
    {
        public class Command : IRequest
        {
            public required Activity Activity { get; set; }
        }

        public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var storedActivity = await context.Activities
                    .FindAsync([request.Activity.Id], cancellationToken)
                    ?? throw new Exception("Cannot find activity");

                mapper.Map(request.Activity, storedActivity);
                
                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
