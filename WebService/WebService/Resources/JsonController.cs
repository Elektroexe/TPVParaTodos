using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace catipadvalidate.Services
{
    public class JsonController : Controller
    {
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        private class JsonNetResult : JsonResult
        {
            public JsonNetResult()
            {
                Settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                };
            }

            public JsonSerializerSettings Settings { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                if (context == null)
                    throw new ArgumentNullException("context");
                if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("JSON GET is not allowed");

                HttpResponseBase response = context.HttpContext.Response;
                response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

                if (ContentEncoding != null)
                    response.ContentEncoding = ContentEncoding;
                if (Data == null)
                    return;

                var scriptSerializer = JsonSerializer.Create(Settings);

                using (var sw = new StringWriter())
                {
                    scriptSerializer.Serialize(sw, Data);
                    response.Write(sw.ToString());
                }
            }
        }
    }
}