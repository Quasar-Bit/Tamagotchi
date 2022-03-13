//
//  ServiceResult.cs
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

public class ServiceResult<T> : ServiceResult
{
    public ServiceResult(T data)
    {
        Data = data;
    }

    public ServiceResult(T data, ServiceError error) : base(error)
    {
        Data = data;
    }

    public ServiceResult(ServiceError error) : base(error)
    {
    }

    public T Data { get; set; }
}

public class ServiceResult
{
    public ServiceResult(ServiceError error)
    {
        if (error == null)
            error = ServiceError.DefaultError;

        Error = error;
    }

    public ServiceResult()
    {
    }

    public bool Succeeded => Error == null;

    public ServiceError Error { get; set; }

    #region Helper Methods

    public static ServiceResult Failed(ServiceError error)
    {
        return new ServiceResult(error);
    }

    public static ServiceResult<T> Failed<T>(ServiceError error)
    {
        return new ServiceResult<T>(error);
    }

    public static ServiceResult<T> Failed<T>(T data, ServiceError error)
    {
        return new ServiceResult<T>(data, error);
    }

    public static ServiceResult<T> Success<T>(T data)
    {
        return new ServiceResult<T>(data);
    }

    #endregion
}