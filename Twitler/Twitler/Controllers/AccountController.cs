using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Twitler.Domain.Interfaces;
using Twitler.Domain.Model;
using Twitler.Filters;
using Twitler.Services.Queries;
using Twitler.Utils.Encryptors;
using Twitler.ViewModels.Account;

namespace Twitler.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IEncryptor _encryptor;

        public AccountController(IRepository<User> userRepository,
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
                var query = new UserByCredsQuery(hashedPassword, loginVm.Email);
                User user = _userRepository.Get(query).SingleOrDefault();

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, true);
                    return RedirectToAction("Main", "User");
                }

                ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
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