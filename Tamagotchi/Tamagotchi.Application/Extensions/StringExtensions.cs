//
//  StringExtensions.cs
//
//  Author:
//       Songurov Fiodor <songurov@gmail.com>
//
//  Copyright (c) 2021 
//
//  This library is free software; you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as
//  published by the Free Software Foundation; either version 2.1 of the
//  License, or (at your option) any later version.
//
//  This library is distributed in the hope that it will be useful, but
//  WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//  Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public
//  License along with this library; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Tamagotchi.Application.Extensions;

public static class StringExtensions
{
    public static T Deserialize<T>(this string value) where T : class
    {
        if (string.IsNullOrWhiteSpace(value)) return default;

        return JsonSerializer.Deserialize<T>(value, new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true
        });
    }

    public static string CapitalizeFirst(this string value)
    {
        return string.IsNullOrEmpty(value)
            ? string.Empty
            : char.ToUpper(value[0]) + value[1..];
    }

    public static string LowerCamelCase(this string value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? string.Empty
            : value[0].ToString().ToLowerInvariant() + value[1..];
    }

    public static string NonSpecialCharacters(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return string.Empty;

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var bytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(value);

        value = Encoding.Default.GetString(bytes);

        return new Regex("[^0-9a-zA-Z._ ]+?").Replace(value, string.Empty);
    }

    public static string AbsoluteWebUrl(this string value)
    {
        return string.IsNullOrEmpty(value) || value.StartsWith("http") ? value : value.Insert(0, "http://");
    }

    public static bool ContainsInsensitive(this string value, string query)
    {
        return !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(query)
                && value.ToUpper().Contains(query.ToUpper());
    }

    public static string NumericCharacters(this string value)
    {
        return string.IsNullOrWhiteSpace(value) ? string.Empty : Regex.Replace(value, "[^0-9]", string.Empty);
    }

    public static string RemoveExtraSpaces(this string value)
    {
        return string.IsNullOrWhiteSpace(value) ? string.Empty : Regex.Replace(value, @"\s+", " ");
    }
}