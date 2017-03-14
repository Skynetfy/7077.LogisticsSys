using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Sys.WebUI.Models
{
    public class FormatDateJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (Data != null)
            {
                var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };//这里使用自定义日期格式，默认是ISO8601格式        
                response.Write(JsonConvert.SerializeObject(Data, Formatting.Indented, timeConverter));
            }
        }
    }
}