using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CustomAuth.Infrastructure;
using CustomAuth.Providers;
using CustomAuth.Utils;
using CustomAuth.ViewModels;
using DalToWeb.Interfacies;

namespace CustomAuth.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserRepository _repository;

        public AccountController(IUserRepository repository)
        {
            this._repository = repository;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var type = HttpContext.User.GetType();
            var iden = HttpContext.User.Identity.GetType();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogOnViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(viewModel.Email, viewModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, viewModel.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Article");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
            }
            return View(viewModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel viewModel, HttpPostedFileBase img)
        {
            if (viewModel.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Incorrect input.");
                return View(viewModel);
            }

            var anyUser = _repository.GetAll().Any(u => u.Name == viewModel.Email);

            if (anyUser)
            {
                ModelState.AddModelError("", "User with this address already registered.");
                return View(viewModel);
            }
            var str = new StringBuilder();
            if (ModelState.IsValid)
            {
                if (img != null)
                    str.Append(FileHelper.SaveFileToDisk(img, Server.MapPath("~/")));
                
                var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                    .CreateUser(viewModel.Email, viewModel.Password,str.ToString());

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, false);
                    return RedirectToAction("Index", "Article");
                }
                else
                {
                    ModelState.AddModelError("", "Error registration.");
                }
                
            }
            return View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult Captcha()
        {
            Session[CaptchaImage.CaptchaValueKey] =
                new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString(CultureInfo.InvariantCulture);
            var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Helvetica");

            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            ci.Dispose();
            return null;
        }

        [ChildActionOnly]
        public ActionResult LoginPartial() => PartialView("_LoginPartial");
    }
}