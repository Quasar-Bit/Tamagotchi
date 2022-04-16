
//
//  BaseController.cs
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

using Microsoft.AspNetCore.Mvc;
using Tamagotchi.Data.DataTableProcessing;
using TamagotchiWeb.Services.Interfaces;

namespace TamagotchiWeb.Controllers.Base;

public abstract class BaseController<T> : BaseController
{
    protected BaseController(ITokenService tokenService, ILogger<T> logger)
    {
        Logger = logger;
        TokenService = tokenService;
    }

    protected ITokenService TokenService { get; }
    protected ILogger<T> Logger { get; }
}

public abstract class BaseController : Controller
{
    protected IActionResult GetErrorView(Exception ex)
    {
        if (Request.Method == "GET") return View("_Error", ex.Message + "\n" + ex.StackTrace);

        var view = new PartialViewResult
        {
            ViewName = "_Error",
            ViewData =
            {
                ["error"] = ex.Message + "\n" + ex.StackTrace
            }
        };

        return view;
    }

    protected DtParameters GetStandardParameters()
    {
        return new DtParameters
        {
            Start = 0,
            Draw = 1,
            Length = 10,
            Search = new DtSearch(),
            Order = new List<DtOrder> { new DtOrder { Column = 0, Dir = DtOrderDir.Asc } }.ToArray(),
            AdditionalValues = new List<string> { string.Empty }.AsEnumerable(),
            Columns = new List<DtColumn>
            {
                new DtColumn { Searchable = true, Orderable = true, Data = "id", Search = new DtSearch() },
                new DtColumn { Searchable = true, Orderable = true, Data = "name", Search = new DtSearch() },
                new DtColumn { Searchable = true, Orderable = true, Data = "type", Search = new DtSearch() },
                new DtColumn { Searchable = true, Orderable = true, Data = "primaryBreed", Search = new DtSearch() },
                new DtColumn { Searchable = true, Orderable = true, Data = "gender", Search = new DtSearch() },
                new DtColumn { Searchable = true, Orderable = true, Data = "age", Search = new DtSearch() },
                new DtColumn { Searchable = true, Orderable = true, Data = "primaryColor", Search = new DtSearch() },
                new DtColumn { Searchable = true, Orderable = true, Data = "organizationId", Search = new DtSearch() }
            }.ToArray()
        };
    }
}