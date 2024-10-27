using FluentValidation;
using Sofomo.Weather.Application.Commands;


namespace Sofomo.Weather.Application.Commands.Validators
{
    public class CreateGeographicalCoordinatesCommandValidator : AbstractValidator<CreateGeographicalCoordinatesCommand>
    {
        public CreateGeographicalCoordinatesCommandValidator()
        {
            RuleFor(x => x.latitude)
                .InclusiveBetween(-90, 90)
                .WithMessage("Latitude must be between -90 and 90");
            RuleFor(x => x.longitude)
                .InclusiveBetween(-180, 180)
                .WithMessage("Longitude must be between -180 and 180");
        }

    }
}
