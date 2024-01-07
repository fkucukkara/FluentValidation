using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IValidator<Person>, PersonValidator>();

var app = builder.Build();

app.MapPost("/person", async (IValidator<Person> validator, Person person) =>
{
    var validationResults = await validator.ValidateAsync(person);

    if (!validationResults.IsValid)
    {
        return Results.ValidationProblem(validationResults.ToDictionary());
    }

    return Results.Ok(person);
});

app.Run();

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
        RuleFor(x => x.Name).NotNull().Length(0, 10);
        RuleFor(x => x.Email).NotNull().EmailAddress();
        RuleFor(x => x.Age).InclusiveBetween(18, 60);
    }
}
public record Person(int Id, string Name, string Email, int Age);