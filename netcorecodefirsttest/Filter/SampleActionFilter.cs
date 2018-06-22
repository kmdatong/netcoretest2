using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcorecodefirsttest.Filter
{
    public class SampleActionAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //判断是否登录
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectResult("~/account/login");
              
                return;
                
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
           
        }
    }
}
