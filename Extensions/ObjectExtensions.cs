namespace MauiUiComponents;

public static class ObjectExtensions
{
    public static T For<T>(
        this T mainObject,
        int endIndex,
        Action<T, int> action,
        int startIndex = 0,
        int step = 1)
    {
        for (int i = startIndex; i < endIndex; i += step)
        {
            action(mainObject, i);
        }

        return mainObject;
    }

    public static T Foreach<T, TObj>(
        this T mainObject,
        IEnumerable<TObj> objects,
        Action<T, TObj> action)
    {
        foreach (TObj curObject in objects)
        {
            action(mainObject, curObject);
        }

        return mainObject;
    }

    public static TMain CopyFrom<TMain, TSource>(
        this TMain mainObject,
        TSource sourceObject)
    {
        ArgumentNullException.ThrowIfNull(mainObject);
        ArgumentNullException.ThrowIfNull(sourceObject);

        var sourceProperties = sourceObject.GetType().GetProperties();
        var mainProperties = mainObject.GetType()
            .GetProperties()
            .ToDictionary(p => p.Name);

        foreach (var sourceProperty in sourceProperties)
        {
            if (!sourceProperty.CanRead)
                continue;

            if (sourceProperty.GetIndexParameters().Length != 0)
                continue;

            if (!mainProperties.TryGetValue(sourceProperty.Name, out var mainProperty))
                continue;

            if (mainProperty.GetIndexParameters().Length != 0)
                continue;

            if (!mainProperty.CanWrite)
                continue;

            if (!mainProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                continue;

            var value = sourceProperty.GetValue(sourceObject);

            if (Equals(mainProperty.GetValue(mainObject), value))
                continue;

            mainProperty.SetValue(mainObject, value);
        }

        return mainObject;
    }
}
