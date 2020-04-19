using MediatR;
using Persistence;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            // ? => date is optional
            public DateTime? Date { get; set; }
            public string City { get; set; }
            public string Venue {get; set; }
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

                // If request.Title is null then everything right of ?? will be executed
                // If its null, nothing changes use activity values
                // activity is being tracked by context
                activity.Title = request.Title ?? activity.Title;
                activity.Description = request.Description ?? activity.Description;
                activity.Category = request.Category ?? activity.Category;
                activity.Date = request.Date ?? activity.Date;
                activity.City = request.City ?? activity.City;
                activity.Venue = request.Venue ?? activity.Venue;

                // Asyncronously save our changes
                // If the SaveChanges returns greater than 0 (more than 0 things have been changed)
                // If activity is changed context will change because it automatically tracks
                var success = await _context.SaveChangesAsync() > 0;

                // Notify if we are successful or not
                // API Controller will return a 200 response
                if(success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}