//
//  EnumExtensions.cs
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

using System.ComponentModel;

namespace TamagotchiWeb.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        if (value is null) return default;

        var attribute = value.GetAttribute<DescriptionAttribute>();

        return attribute is null ? value.ToString() : attribute.Description;
    }

    public static string[] ToArray(this Enum value)
    {
        return value?.ToString().Split(", ");
    }

    private static T GetAttribute<T>(this Enum value) where T : Attribute
    {
        if (value is null) return default;

        var member = value.GetType().GetMember(value.ToString());

        var attributes = member[0].GetCustomAttributes(typeof(T), false);

        return (T)attributes[0];
    }
}