using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain;
using Application.Activities;
using System;

// This where our Routing happens
namespace API.Controllers
{
    // at /api/activities
    [Route("api/[controller]")]
    [ApiController]

    // Create a new ActivitiesController class that extends the ControllerBase base class
    public class ActivitiesController : ControllerBase
    {
        
        // Reference to mediator that is initalized in the constructor
        private readonly IMediator _mediator;
        public ActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // An async method named List that returns a task of type ActionResult that is a list of Activities
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> List()
        {
            return await _mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> Details(Guid id)
        {
            // Details.Query => create query calss and pass in the id from root id 
            // Because we specified a getter and setter for Id the id is set in Detail class
            return await _mediator.Send(new Details.Query{Id = id});
        }

        // Allow us to create a new Activity
        // Passed in what we are receiving in the body of the request
        // Returns the Unit.Value from Create.cs notifying if we are successful or not 
        // [ApiController] knows where to look for the body 
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command) 
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, Edit.Command command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id) 
        {
            // Create new delete command and have mediator send that
            return await _mediator.Send(new Delete.Command{Id = id});
        }
    }
}