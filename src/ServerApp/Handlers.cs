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
    }
}