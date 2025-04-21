using System.ComponentModel.DataAnnotations;

namespace Satellite.Astronaut.Tracking.Attribute;

public class PastDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        return value is DateTime date && date < DateTime.UtcNow;
    }
}