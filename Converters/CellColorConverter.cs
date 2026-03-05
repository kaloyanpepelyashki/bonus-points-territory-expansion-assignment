using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Territory_Expansion_Game.Converters;

public class CellColorConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count < 2) return Brushes.LightGray;

        int owner = values[0] is int o ? o : 0;
        bool isLegal = values[1] is bool l && l;

        return owner switch
        {
            1 => Brushes.RoyalBlue,
            2 => Brushes.Crimson,
            _ => isLegal ? Brushes.LightGreen : Brushes.LightGray
        };
    }
}