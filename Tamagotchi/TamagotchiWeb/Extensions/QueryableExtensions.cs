//
//  QueryableExtensions.cs
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

using System.Linq.Expressions;

namespace TamagotchiWeb.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, string property, object value)
    {
        return queryable.Filter(property, string.Empty, value);
    }

    public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, string property, string comparison,
        object value)
    {
        if (string.IsNullOrWhiteSpace(property) || value is null || string.IsNullOrWhiteSpace(value.ToString()))
            return queryable;

        var parameter = Expression.Parameter(typeof(T));

        var left = Create(property, parameter);

        try
        {
            value = Change(value, left.Type);
        }
        catch
        {
            return Enumerable.Empty<T>().AsQueryable();
        }

        var right = Expression.Constant(value, left.Type);

        var body = Create(left, comparison, right);

        var expression = Expression.Lambda<Func<T, bool>>(body, parameter);

        return queryable.Where(expression);
    }

    public static IQueryable<T> Order<T>(this IQueryable<T> queryable, string property, bool ascending)
    {
        if (queryable is null || string.IsNullOrWhiteSpace(property)) return queryable;

        var parameter = Expression.Parameter(typeof(T));

        var body = Create(property, parameter);

        var expression = (dynamic)Expression.Lambda(body, parameter);

        return ascending
            ? Queryable.OrderBy(queryable, expression)
            : Queryable.OrderByDescending(queryable, expression);
    }

    public static IQueryable<T> Page<T>(this IQueryable<T> queryable, int index, int size)
    {
        if (queryable is null || index <= 0 || size <= 0) return queryable;

        return queryable.Skip((index - 1) * size).Take(size);
    }

    private static object Change(object value, Type type)
    {
        if (type.BaseType == typeof(Enum)) value = Enum.Parse(type, value.ToString());

        return Convert.ChangeType(value, type);
    }

    private static Expression Create(string property, Expression parameter)
    {
        return property.Split('.').Aggregate(parameter, Expression.Property);
    }

    private static Expression Create(Expression left, string comparison, Expression right)
    {
        if (string.IsNullOrWhiteSpace(comparison) && left.Type == typeof(string))
            return Expression.Call(left, nameof(string.Contains), Type.EmptyTypes, right);

        var type = comparison switch
        {
            "<" => ExpressionType.LessThan,
            "<=" => ExpressionType.LessThanOrEqual,
            ">" => ExpressionType.GreaterThan,
            ">=" => ExpressionType.GreaterThanOrEqual,
            "!=" => ExpressionType.NotEqual,
            _ => ExpressionType.Equal
        };

        return Expression.MakeBinary(type, left, right);
    }
}