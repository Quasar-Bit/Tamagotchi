//
//  ServiceError.cs
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

namespace TamagotchiWeb.Exceptions.Result;

public sealed class ServiceError : IEquatable<ServiceError>
{
    public ServiceError(string message, int code)
    {
        Message = message;
        Code = code;
    }

    public ServiceError()
    {
    }

    public string Message { get; }

    public int Code { get; }

    public static ServiceError DefaultError => new("An exception occured.", 999);

    public static ServiceError ForbiddenError => new("You are not authorized to call this action.", 998);

    public static ServiceError UserNotFound => new("User with this id does not exist", 996);

    public static ServiceError UserFailedToCreate => new("Failed to create User.", 995);

    public static ServiceError Canceled => new("The request canceled successfully!", 994);

    public static ServiceError NotFound => new("The specified resource was not found.", 990);

    public static ServiceError ValidationFormat => new("Request object format is not true.", 901);

    public static ServiceError Validation => new("One or more validation errors occurred.", 900);

    public static ServiceError SearchAtLeastOneCharacter =>
        new("Search parameter must have at least one character!", 898);

    public static ServiceError ServiceProviderNotFound =>
        new("Service Provider with this name does not exist.", 700);

    public static ServiceError ServiceProvider => new("Service Provider failed to return as expected.", 600);

    public static ServiceError DateTimeFormatError =>
        new("Date format is not true. Date format must be like yyyy-MM-dd (2019-07-19)", 500);

    public static ServiceError ModelStateError(string validationError)
    {
        return new ServiceError(validationError, 998);
    }

    public static ServiceError CustomMessage(string errorMessage)
    {
        return new ServiceError(errorMessage, 997);
    }

    #region Override Equals Operator

    public override bool Equals(object obj)
    {
        var error = obj as ServiceError;

        return Code == error?.Code;
    }

    public bool Equals(ServiceError other)
    {
        return Code == other?.Code;
    }

    public override int GetHashCode()
    {
        return Code;
    }

    public static bool operator ==(ServiceError a, ServiceError b)
    {
        if (ReferenceEquals(a, b))
            return true;

        if ((object)a == null || (object)b == null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(ServiceError a, ServiceError b)
    {
        return !(a == b);
    }

    #endregion
}