using FluentValidation;
using ViajaBrasil.Application.DTOs.Requests;

namespace ViajaBrasil.Application.Validators;

public class TouristSpotValidator : AbstractValidator<TouristSpotRequest>
{
    public TouristSpotValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Location)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.City)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.State)
            .NotEmpty()
            .Length(2);
    }
}