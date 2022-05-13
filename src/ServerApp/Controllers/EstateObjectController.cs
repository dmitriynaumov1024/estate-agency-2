using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using EstateAgency.Common;

namespace ServerApp
{

    [ApiController]
    public class EstateObjectController : ControllerBase
    {
        const int MAX_PHOTOS = 10;

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

        [HttpGet("/objects/top")]
        public ActionResult<object> GetTopEstateObjects ()
        {
            return this.EA().GetTopEstateObjects();
        }

        [HttpPost("/objects/create")]
        public ActionResult<object> CreateEstateObject (EstateObjectData data)
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            if (userid == null) {
                return new ErrorResponse("notLoggedIn");
            }

            EstateObject obj = new EstateObject {
                ObjectType = data.ObjectType,
                SellerId = (int)userid,
                IsActive = true,
                PostDate = DateTime.UtcNow,
                Caption = data.Caption,
                Description = data.Description,
                Address = data.Address,
                LocationId = data.LocationId,
                Price = data.Price,
                SquareMeters = Math.Abs(data.SquareMeters),
                Tags = data.Tags
            };

            return this.EA().Backend.EstateObjects.Put(obj);
        }

        [HttpPost("/objects/attach-photos")]
        public ActionResult<object> AttachPhotosToEstateObject ()
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            if (userid == null) {
                return new ErrorResponse("notLoggedIn");
            }
            int? id = Request.Form.GetInt32("objectId");
            if (id == null) {
                return new ErrorResponse("notAllowed");
            }
            EstateObject obj = this.EA().GetEstateObject((int)id);
            if (obj.PhotoSources != null && obj.PhotoSources.Count > 0) {
                return new ErrorResponse("notAllowed");
            }

            var files = Request.Form.Files.Take(MAX_PHOTOS);
            List<string> photoSources = new List<string>();
            foreach (var file in files) {
                string ext = file.ContentType.Split('/')[1];
                string path = $"img{TimeId()}.{ext}";
                photoSources.Add(path);
                using (var stream = System.IO.File.Create($"./cdn/{path}")) {
                    file.CopyTo(stream);
                    stream.Flush();
                    stream.Close();
                }
            }
            
            obj.PhotoSources = photoSources;
            this.EA().Backend.EstateObjects.Replace((int)id, obj);
            return true;
        }

        static long TimeId () {
            System.Threading.Thread.Sleep(10);
            return DateTime.UtcNow.ToBinary();
        }
    }

    public class EstateObjectData
    {
        public EstateObjectTypes ObjectType { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int LocationId { get; set; }
        public int Price { get; set; }
        public int SquareMeters { get; set; }
        public List<string> Tags { get; set; }
    }
}
