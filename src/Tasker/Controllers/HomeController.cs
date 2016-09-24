using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using DomainModel.Repositories;
using Tasker.Models;
using DomainModel.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Tasker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository user_repo_;

        public HomeController(IUserRepository repo)
        {
            user_repo_ = repo;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            try
            {
                //UserDTO user = user_repo_.getUser("57dbfcadcc2963cd7fea8798");
                //ApplicationUser userModel = new ApplicationUser
                //{
                //    ID = user.Id,
                //    FullName = user.FullName,
                //    Bio = user.Bio,
                //    Boards = user.Boards,
                //    Initials = user.Initials,
                //    Picture = user.Picture,
                //    UserName = user.UserName
                //};
                return View();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}