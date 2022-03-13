//
//  ClaimExtensions.cs
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

using System.Security.Claims;

namespace TamagotchiWeb.Extensions;

public static class ClaimExtensions
{
    public static void AddRoles(this ICollection<Claim> claims, string[] roles)
    {
        roles.ToList().ForEach(role => claims.Add(new Claim("role", role)));
    }

    public static Claim Claim(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        return claimsPrincipal?.FindFirst(claimType);
    }

    public static IEnumerable<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal?.Claims("role");
    }

    public static IEnumerable<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        return claimsPrincipal?.FindAll(claimType).Select(x => x.Value).ToList();
    }

    public static IEnumerable<T> Roles<T>(this ClaimsPrincipal claimsPrincipal) where T : Enum
    {
        return claimsPrincipal.ClaimRoles().Select(value => (T)Enum.Parse(typeof(T), value)).ToList();
    }

    public static T RolesFlag<T>(this ClaimsPrincipal claimsPrincipal) where T : Enum
    {
        var roles = claimsPrincipal.Roles<T>().Sum(value => Convert.ToInt64(value));

        return (T)Enum.Parse(typeof(T), roles.ToString(), true);
    }
}