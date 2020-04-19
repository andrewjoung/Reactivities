using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Persistence;
using Domain;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public DateTime Date { get; set; }
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
                // We create a new Activity entity from the request => think of this like req.body
                var activity = new Activity
                {
                    Id = request.Id,
                    Title = request.Title,
                    Description = request.Description,
                    Category = request.Category,
                    Date = request.Date,
                    Venue = request.Venue
                };

                // We get our context that we initalize in the constructor
                // Find the Activities table
                // Add our new Activity to the Activities folder
                _context.Activities.Add(activity);

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