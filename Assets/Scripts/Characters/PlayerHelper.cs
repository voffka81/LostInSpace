using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

public static class PlayerHelper
{

    public static string GetEnumMemberValue<T>(T value)
    where T : struct, IConvertible
    {
        return typeof(T)
            .GetTypeInfo()
            .DeclaredMembers
            .SingleOrDefault(x => x.Name == value.ToString())
            ?.GetCustomAttribute<EnumMemberAttribute>(false)
            ?.Value;
    }

    public static bool IsBlockingAnimation<T>(T value)
    where T : struct, IConvertible
    {
        var enumType = typeof(T);
        var memInfo = enumType.GetMember(value.ToString());
        var attr = memInfo.FirstOrDefault()?.GetCustomAttributes(false).OfType<BlockingAnimation>().FirstOrDefault();
        return attr != null;
    }
}
