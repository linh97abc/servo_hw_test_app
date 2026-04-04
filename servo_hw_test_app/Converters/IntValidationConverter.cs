using Avalonia;
using Avalonia.Data.Converters;
using servo_hw_test_app.ViewModels;
using System;
using System.Globalization;

namespace servo_hw_test_app.Converters;

public class IntValidationConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString() ?? "";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        string? propertyName = parameter?.ToString();
        string? strValue = value?.ToString();

        // Nếu để trống hoặc nhập chữ
        if (string.IsNullOrWhiteSpace(strValue) || !int.TryParse(strValue, out var result))
        {
            // Ném exception để binding fail (bôi đỏ)
            // throw new InvalidCastException("Must input a valid number");
            return null; // Return null to indicate invalid input, which will trigger validation error in the UI
        }

        return result;
    }
}

