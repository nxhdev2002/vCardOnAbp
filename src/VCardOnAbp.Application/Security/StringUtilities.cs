namespace VCardOnAbp.Security;
using Microsoft.Security.Application;
using System.Reflection;

public static class StringUtilities
{
    public static object SanitizeInput(this object userInput)
    {
        if (userInput == null) return null;

        // Duyệt qua tất cả các property của class
        var properties = userInput.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            // Kiểm tra nếu property là string và có setter
            if (property.PropertyType == typeof(string) && property.CanWrite)
            {
                var value = property.GetValue(userInput) as string;

                // Chỉ sanitize nếu value không null
                if (!string.IsNullOrEmpty(value))
                {
                    property.SetValue(userInput, Sanitizer.GetSafeHtmlFragment(value));
                }
            }
        }

        return userInput;
    }
}
