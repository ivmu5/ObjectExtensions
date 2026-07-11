using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ObjectExtensions;

public static class EnumExtensions
{
    public static DisplayAttribute? GetDisplayAttribute(this Enum value)
    {
        var member = value.GetType()
            .GetMember(value.ToString())
            .FirstOrDefault();

        return member?.GetCustomAttribute<DisplayAttribute>();
    }
}
