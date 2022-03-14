using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Exam.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Exam.Controllers
{
    public class UserController : Controller
    {
        UserRepository _userRepository;
        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> LoginAsync(User user)
        {
            if (user.UserName != null && user.Password != null)
            {
                User result = _userRepository.GetUser(user);
                if (result == null)
                    TempData["Message"] = "Kullanıcı adı ve/veya şifre hatalı";
                else
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name , result.UserName),
                        new Claim(ClaimTypes.Role , result.Role)
                    };
                    var useridentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                    await HttpContext.SignInAsync(principal);
                    if (result.Role == Role.Admin)
                        return RedirectToAction("ListExams", "ExamDefAdmin");
                    else
                        return RedirectToAction("ListExams", "Exam");
                }
            }
            return View();
        }
    }
}
