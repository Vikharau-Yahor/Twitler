using System.Web.Mvc;
using System.Web.Security;
using Twitler.Domain.Interfaces;
using Twitler.Domain.Model;
using Twitler.Utils.Encryptors;
using Twitler.ViewModels.Account;

namespace Twitler.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository _userRepository;
        private IEncryptor _encryptor;

        public AccountController(IUserRepository userRepository,
                                 IEncryptor encryptor)
        {
            _userRepository = userRepository;
            _encryptor = encryptor;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVm loginVm)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = _encryptor.Encrypt(loginVm.Password);
                User user = _userRepository.Find(loginVm.Email, hashedPassword);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, true);
                    return RedirectToAction("Main", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(loginVm);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}