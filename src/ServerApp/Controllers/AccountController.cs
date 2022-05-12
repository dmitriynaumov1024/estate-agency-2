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
    public class AccountController : ControllerBase
    {
        [HttpPost("/login")]
        public ActionResult<object> LogIn (LoginData data)
        {
            AccountInfo result = this.EA().LogIn(data.Phone, data.Password);
            if (result.PersonId != null) {
                HttpContext.Session.SetInt32("UserId", (int)result.PersonId);
            }
            return result;
        }

        [HttpGet("/restore-session")]
        public ActionResult<object> RestoreSession ()
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            if (userid != null) {
                return new AccountInfo {
                    PersonId = userid
                };
            }
            else {
                return new ErrorResponse("notLoggedIn");
            }
        }

        [HttpPost("/signup")]
        public ActionResult<object> SignUp (SignupData data)
        {
            AccountInfo result = this.EA().SignUp(data, data.Password);
            if (result.PersonId != null) {
                HttpContext.Session.SetInt32("UserId", (int)result.PersonId);
            }
            return result;
        }

        [HttpGet("/account")]
        public ActionResult<object> Account (int? id) 
        {
            int? userid = HttpContext.Session.GetInt32("UserId");

            if (id == null) {
                if (userid != null) {
                    return GetAccount((int)userid);
                }
                else {
                    return new ErrorResponse("notLoggedIn");
                }
            }
            else {
                if (userid != null && this.EA().PersonCanSeeOthers((int)userid)) {
                    return GetAccount((int)id);
                }
                else {
                    return new ErrorResponse("notAllowed");
                }
            }
        }

        ActionResult<object> GetAccount (int id) 
        {
            object result = this.EA().GetAggregatePerson(id);
            if (result == null) {
                return new ErrorResponse("accountNotFound");
            }
            return result;
        }
    }

    public class LoginData 
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }

    public class SignupData : Person
    {
        public string Password { get; set; }
    }
}
