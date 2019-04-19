using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SqlInjectionDemo.Application;
using SqlInjectionDemo.Common;
using SqlInjectionDemo.Web.Public.Models;

namespace SqlInjectionDemo.Web.Public.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await userService.GetUnsafe("");

            var model = new UserSearchInputModel(users);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserSearchInputModel model)
        {
            try
            {
                List<User> users;
                if (model.IsSafe)
                {
                    users = await userService.GetSafe(model.SearchName);
                }
                else
                {
                    users = await userService.GetUnsafe(model.SearchName);
                }

                model.SetUsers(users);

                return View(model);
            }
            catch (FunSpoilerException e)
            {
                var users = await userService.GetSafe("");
                model.SetUsers(users);
                model.SetErrorMessage(e.Message);
                return View(model);
            }
        }
    }
}
