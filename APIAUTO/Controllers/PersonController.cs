using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace APIAUTO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Person person)
        {
            

            return Ok(person);
        }

        public class PersonValidator : AbstractValidator<Person>
        {
            public PersonValidator()
            {
                RuleFor(x => x.Id).NotNull();
                RuleFor(x => x.Name).NotNull().Length(0, 10);
                RuleFor(x => x.Email).NotNull().EmailAddress();
                RuleFor(x => x.Age).InclusiveBetween(18, 60);
            }
        }
        public record Person
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
            public int Age { get; set; }
        }
    }
}