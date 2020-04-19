using MediatR;
using Persistence;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
               public Guid Id { get; set; }

        }

        public class Handler : IRequestHandler<Command>
        {
            
            private readonly DataContext _context;

            public Handler(DataContext context) {
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                //Handler logic here
                var activity = await _context.Activities.FindAsync(request.Id);

                if(activity == null) {
                    throw new Exception("Could not find activity");
                }

                // Remove the activity from the context
                // If it is successful we save changes
                _context.Remove(activity);


                // Asyncronously save our changes
                // If the SaveChanges returns greater than 0 (more than 0 things have been changed)
                var success = await _context.SaveChangesAsync() > 0;

                // Notify if we are successful or not
                // API Controller will return a 200 response
                if(success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}