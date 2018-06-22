﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcorecodefirsttest.Components
{
    public class OneViewComponent: ViewComponent
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OneViewComponent()
        {

        }
        /// <summary>
        /// 异步调用
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //这里通过 HttpContext.User.Claims 可以将我们在Login这个Action中存储到cookie中的所有
                //claims键值对都读出来，比如我们刚才定义的UserName的值Wangdacui就在这里读取出来了
                ViewBag.userName = HttpContext.User.Claims.First().Value;
            }

            return View();
        }
    }
}
