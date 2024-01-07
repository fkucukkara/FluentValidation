using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IValidator<Person> _validator;
        public PersonController(IValidator<Person> validator)
        {
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Person person)
        {
            var result = await _validator.ValidateAsync(person);

            if (result.IsValid == false)
            { return BadRequest(result); }

            return Ok(result);
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

        public record Person(int Id, string Name, string Email, int Age);
    }
}