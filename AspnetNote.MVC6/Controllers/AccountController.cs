using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetNote.MVC6.DataContext;
using AspnetNote.MVC6.Models;
using AspnetNote.MVC6.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetNote.MVC6.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 로그인
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new AspnetNoteDbContext())
                {
                    // Linq Method Chaining
                    var user = db.Users.FirstOrDefault(u => u.UserId.Equals(model.UserId) && 
                                                            u.UserPassword.Equals(model.UserPassword));
                    if(user != null)
                    {
                        // Login Success
                        return RedirectToAction("LoginSuccess", "Home");   
                    }                    
                }
                // Login Fail
                ModelState.AddModelError(string.Empty, "Invalid User");
            }
            return View(model);
        }
        

        /// <summary>
        /// 회원 가입
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 회원가입 전송
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // 전송하는 Action Annotation 선언
        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                using(var db = new AspnetNoteDbContext())
                {
                    db.Users.Add(model);
                    db.SaveChanges(); // DB Commit
                }
                // Home 컨트롤러의 Index로 넘김
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
