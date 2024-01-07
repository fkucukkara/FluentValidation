using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace APIAUTO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        public PersonController()
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post(Person person)
        {
            await Task.CompletedTask;

            return Ok(person);
        }

        public class PersonValidator : AbstractValidator<Person>
        {
            public PersonValidator()
            {
                RuleFor(x => x.Id).NotNull().NotEmpty();
                RuleFor(x => x.Name).NotNull().Length(0, 10);
                RuleFor(x => x.Email).NotNull().EmailAddress();
                RuleFor(x => x.Age).InclusiveBetween(15, 45);
            }
        }
        public record Person(int Id, string Name, string Email, int Age);
    }
}