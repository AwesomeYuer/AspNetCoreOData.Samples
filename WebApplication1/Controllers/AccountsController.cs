//-----------------------------------------------------------------------------
// <copyright file="AccountsController.cs" company=".NET Foundation">
//      Copyright (c) .NET Foundation and Contributors. All rights reserved.
//      See License.txt in the project root for license information.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;


namespace ODataRoutingSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(ODataQueryOptions<JToken> queryOptions)
        {
            var jArray = JArray.Parse
                (
                    """
[
    { "a" : 1}
]

"""
                );
            var querable = jArray.AsQueryable();
            IQueryable<JToken> queryable = jArray.AsQueryable();

            var filteredQueryable = queryOptions.ApplyTo(queryable) as IQueryable<JToken>;

            var sortedQueryable = filteredQueryable!.OrderBy(t => (int)t["age"]!);


            var finalQuery = queryOptions
                                    .ApplyTo
                                            (querable);
            return Ok(finalQuery);
        }

    }
}
