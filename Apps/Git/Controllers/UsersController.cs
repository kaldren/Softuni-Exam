using Git.Constants;
using Git.Services;
using Git.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Git.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect(RedirectConst.AllRepositories);
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel model)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect(RedirectConst.AllRepositories);
            }

            var userId = this.usersService.GetUserId(model.Username, model.Password);
            if (userId == null)
            {
                return this.Error(ErrorConst.InvalidUsernameOrPassword);
            }

            this.SignIn(userId);
            return this.Redirect(RedirectConst.AllRepositories);
        }

        [HttpGet]
        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect(RedirectConst.AllRepositories);
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel model)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect(RedirectConst.AllRepositories);
            }

            if (string.IsNullOrEmpty(model.Username) || model.Username.Length < 5 || model.Username.Length > 20)
            {
                return this.Error(ErrorConst.InvalidUsernameLength);
            }

            if (string.IsNullOrEmpty(model.Email) || !new EmailAddressAttribute().IsValid(model.Email))
            {
                return this.Error(ErrorConst.InvalidEmail);
            }

            if (string.IsNullOrEmpty(model.Password) || model.Password.Length < 6 || model.Password.Length > 20)
            {
                return this.Error(ErrorConst.RequiredPassword);
            }

            if (model.ConfirmPassword != model.Password)
            {
                return this.Error(ErrorConst.PasswordMismatch);
            }

            if (!this.usersService.IsEmailAvailable(model.Email))
            {
                return this.Error(ErrorConst.EmailTaken);
            }

            if (!this.usersService.IsUsernameAvailable(model.Username))
            {
                return this.Error(ErrorConst.UsernameTaken);
            }

            string createdUser = this.usersService.CreateUser(model.Username, model.Email, model.Password);

            if (createdUser == string.Empty)
            {
                return this.Error(ErrorConst.UnableToCreateAccount);
            }
            else
            {
                return this.Redirect(RedirectConst.LoginPage);
            }
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect(RedirectConst.LoginPage);
            }

            this.SignOut();
            return this.Redirect(RedirectConst.AllRepositories);
        }
    }
}
