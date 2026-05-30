using FluentValidation.TestHelper;
using ViajaBrasil.Application.DTOs.Requests;
using ViajaBrasil.Application.Validators;

namespace ViajaBrasil.Tests.Unit.TouristSpot.Validators;

public class TouristSpotValidatorTests
{
    private readonly TouristSpotValidator _validator = new();

    [Fact(DisplayName = "Deve validar uma requisição válida")]
    public void Validate_Should_Not_Have_Errors_When_Request_Is_Valid()
    {
        var request = new TouristSpotRequest
        {
            Name = "MASP",
            Description = "Museu",
            Location = "Av. Paulista",
            CityIbgeCode = "3550308"
        };

        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Deve retornar erro quando o nome não for informado")]
    public void Validate_Should_Have_Error_When_Name_Is_Empty()
    {
        var request = new TouristSpotRequest { Name = string.Empty };
        var result = _validator.TestValidate(request);
        
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact(DisplayName = "Deve retornar erro quando o código IBGE for inválido")]
    public void Validate_Should_Have_Error_When_IbgeCode_Is_Invalid()
    {
        var request = new TouristSpotRequest { CityIbgeCode = "ABC123" };
        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.CityIbgeCode);
    }
}