using System.Globalization;
using System.Runtime.CompilerServices;
using FluentValidation;
using SumCalculator.Models;

namespace SumCalculator.Validators;

public class CalculatorEntryValidator: AbstractValidator<CalculatorRecord>
{
    public CalculatorEntryValidator()
    {

        RuleFor(x => x.Value1)
            .Cascade(CascadeMode.Stop) // Stop if the first rule fails
            .Must(v => v.HasValue).WithMessage("Value1 must be a number.")
            .Must(HaveValidPrecision).WithMessage("Value1 must be between -999999999999.999999 and 999999999999.999999.");

        RuleFor(x => x.Value2)
            .Cascade(CascadeMode.Stop) // Stop if the first rule fails
            .Must(v => v.HasValue).WithMessage("Value2 must be a number.")
            .Must(HaveValidPrecision).WithMessage("Value2 must be between -999999999999.999999 and 999999999999.999999."); 

        RuleFor(x => x.Sum)
            .Must((x,y)=>SumNotOverflow(x.Value1, x.Value2))
            .WithMessage("The sum of value1 and value2 must be between -999999999999.999999 and 999999999999.999999");

        
    }

    private static bool SumNotOverflow(decimal? value1, decimal? value2)
    {
        //other rule will handle nulls.
        if (value1 == null || value2 == null) return true;
        try
        {
            var sum = checked(value1!.Value + value2!.Value);
            var result = sum >= -999999999999.999999m && sum <= 999999999999.999999m;
            return result;
        }catch(Exception)
        {
            return false;
        }

    }
 

 private bool HaveValidPrecision(decimal? value)
{
    if (!value.HasValue) return false;

    // Extract the value as a string
    var decimalString = value.Value.ToString(CultureInfo.InvariantCulture);
    var parts = decimalString.Split('.');

    // Count digits before and after the decimal point
    var integerDigits = parts[0].TrimStart('-').Length; // Ignore the negative sign
    var decimalDigits = parts.Length > 1 ? parts[1].Length : 0;

    // Precision = integer digits + decimal digits; Scale = decimal digits
    return integerDigits + decimalDigits <= 18 && decimalDigits <= 6;
}
}