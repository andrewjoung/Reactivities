using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{   
    // Always needs to have route 
    // Attribute based routing 
    // The route we need to access this particular controller
    // [controller] is replaced with name of controller/first value 
    // So in this case Values
    [Route("api/[controller]")]
    [ApiController]

    // We need access to the Database Context (Persistence) so that we have access to the database and can make queries 
    // VIA Entity Framework
    public class ValuesController : ControllerBase
    {

        private readonly DataContext _context;
        public ValuesController(DataContext context) 
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]

        //This will be happening on a different thread
        public async Task <ActionResult<IEnumerable<Value>>> Get()
        {
            // We wait for the task to finish
            var values =  await _context.Values.ToListAsync();
            // It is recommended to make get from database asynchronous
            // Makes it more scalable too
            return Ok(values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task <ActionResult<Value>> Get(int id)
        {
            var value = await _context.Values.FindAsync(id);
            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}