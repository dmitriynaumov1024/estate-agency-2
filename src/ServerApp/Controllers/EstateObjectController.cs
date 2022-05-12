using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using EstateAgency.Common;

namespace ServerApp
{
    [ApiController]
    public class EstateObjectController : ControllerBase
    {
        [HttpGet("/object")]
        public ActionResult<object> GetEstateObject (int? id)
        {
            if (id == null) return new ErrorResponse("objectNotFound");
            EstateObject obj = this.EA().GetEstateObject((int)id);
            if (obj == null) return new ErrorResponse("objectNotFound");

            return new Dictionary<string, object> {
                ["item"] = obj,
                ["seller"] = this.EA().GetPerson(obj.SellerId),
                ["location"] = this.EA().GetLocation(obj.LocationId),
                ["bookmarkCount"] = this.EA().GetObjectBookmarksCount((int)id)
            };
        }
    }
}