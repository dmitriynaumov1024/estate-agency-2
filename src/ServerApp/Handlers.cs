using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.IO;

using EstateAgency.Common;

namespace ServerApp
{
    public static class Handlers
    {
        // Default request handler - sends index.html to client
        public static async Task DefaultGet (HttpContext context) 
        {
            await context.Response.SendFileAsync("./wwwroot/index.html");
        }

        public static async Task ApiHandler (HttpContext context) 
        {  
            context.Response.Headers["Access-Control-Allow-Origin"] = "*";
            context.Response.IsPlainText();
            await context.Response.WriteAsync("bebebe with bababa");
        }

        public static async Task ObjectsGet (HttpContext context) 
        {  
            context.Response.Headers["Access-Control-Allow-Origin"] = "*";
            context.Response.IsPlainText();
            await context.Response.WriteAsync("objects also have bebebe with bababa");
        }

        public static async Task PrivateGet (HttpContext context) 
        {  
            context.Response.IsPlainText();
            string userId = context.Session.GetString("userId");
            if (userId != null) {
                await context.Response.WriteAsync($"Allowed! userId={userId}");
            }
            else {
                await context.Response.WriteAsync("You need to authorise first.");
            }
        }

        public static async Task PostLogin (HttpContext context) 
        {  
            string phone = context.Request.Form.GetString("phone");
            if (phone == null) {
                await BadRequest(context, "Phone is required.");
                return;
            }
            string password = context.Request.Form.GetString("password");
            if (password == null) {
                await NotFound (context, "Password is required.");
                return;
            }
            Account acc = EA.Accounts.Get(phone);
            if (acc == null) {
                await BadRequest(context, $"Account with phone {phone} does not exist.");
                return;
            }
            if (acc.PasswordHash == password.Hash()) {
                await context.Response.WriteAsJsonAsync(acc.PersonId);
                return;
            }
            else {
                await BadRequest(context, "Wrong password.");
                return;
            }
        }

        public static async Task PostSignup (HttpContext context) 
        {  
            var form = context.Request.Form;
            string phone = form.GetString("phone");
            if (phone == null) {
                await BadRequest (context, "Phone is required.");
                return;
            }
            if (EA.Accounts.Get(phone) != null) {
                await BadRequest (context, "User with this phone number already exists. Try to log in.");
                return;
            }
            try {
                int newId = EA.Persons.Put ( new Person {
                    Phone = phone,
                    Email = form.GetString("email"),
                    LocationId = (int)(form.GetInt32("locationId")),
                    Surname = form.GetString("surname"),
                    Name = form.GetString("name")
                });
                EA.Accounts.Replace (phone, new Account {
                    PersonId = newId,
                    PasswordHash = form.GetString("password").Hash(),
                    RegDate = DateTime.UtcNow,
                    AccountType = AccountTypes.User,
                    AccountState = AccountStates.Normal
                });
                await context.Response.WriteAsJsonAsync(newId);
            }
            catch (Exception) {
                await BadRequest (context, "Something went wrong");
            }
        }

        public static async Task GetPerson (HttpContext context)
        {
            int? id = context.Request.Query.GetInt32("id");
            if (id != null) {
                Person person = EA.Persons.Get((int)id);
                await context.Response.WriteAsJsonAsync(person);
            }
            else {
                await NotFound (context);
            }
        }

        public static async Task GetObject (HttpContext context)
        {
            int? id = context.Request.Query.GetInt32("id");
            if (id != null) {
                EstateObject obj = EA.EstateObjects.Get((int)id);
                if (obj == null) {
                    await NotFound (context);
                    return;
                }
                Location loc = EA.Locations.Get(obj.LocationId);
                Person seller = EA.Persons.Get(obj.SellerId);
                await context.Response.WriteAsJsonAsync (
                    new Dictionary<string, object> {
                        ["item"] = obj,
                        ["location"] = loc,
                        ["seller"] = seller
                    }
                );
            }
            else {
                await BadRequest (context, "Bad request: object id was not set");
                return;
            }
        }

        public static async Task NotFound (HttpContext context, string message = "404 Not found") 
        {
            context.Response.IsPlainText();
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync (new ErrorResponse {
                Error = message
            });
        }

        public static async Task BadRequest (HttpContext context, string message)
        {
            context.Response.IsPlainText();
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync (new ErrorResponse {
                Error = message
            });
        }

        public static async Task ServerError (HttpContext context, string message)
        {
            context.Response.IsPlainText();
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync (new ErrorResponse {
                Error = message
            });
        }

        static EABackend EA { 
            get { return EAProvider.Instance; } 
        }
    }
}