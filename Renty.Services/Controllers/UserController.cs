using Renty.Services.Attributes;
using Renty.Services.DataTransferObects;
using Renty.Services.ModelMappers;
using Renty.Services.Models;
using Renty.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ValueProviders;

namespace Renty.Services.Controllers
{
    public class UserController : BaseController
    {
        [HttpPost, ActionName("register")]
        public HttpResponseMessage RegisterUser(UserLoginRegisterModel userToRegister)
        {
            var dbContext = new RentyDbContext();
            UserValidator.ValidateAuthCode(userToRegister.AuthCode);
            UserValidator.ValidateUsername(userToRegister.Username);
            userToRegister.Username = userToRegister.Username.ToLower();

            bool isExistingUsername =
              dbContext.Users.Any(x => x.Username.ToLower() == userToRegister.Username.ToLower());

            if (isExistingUsername)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Username is Taken");
            }

            User newUser = null;
            try
            {
                newUser = UsersMapper.ToUserEntity(userToRegister);
            }
            catch (Exception)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid User register model provided!");
            }

            dbContext.Users.Add(newUser);
            newUser.SessionKey = UserValidator.GenerateSessionKey(newUser.UserId);
            dbContext.SaveChanges();
            
            UserLoggedModel loggedUser = new UserLoggedModel()
            {
                SessionKey = newUser.SessionKey,
                Username = newUser.Username
            };

            HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.Created, loggedUser);
            return response;
        }

        [HttpPost, ActionName("login")]
        public HttpResponseMessage LoginUser(UserLoginRegisterModel userToLogin)
        {
            var dbContext = new RentyDbContext();

            UserValidator.ValidateAuthCode(userToLogin.AuthCode);
            UserValidator.ValidateUsername(userToLogin.Username);

            User user = null;
            try
            {
                user = dbContext.Users.Single(x => x.AuthCode == userToLogin.AuthCode);
            }
            catch (Exception)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid username or password!");
            }

            user.SessionKey = UserValidator.GenerateSessionKey(user.UserId);
            dbContext.SaveChanges();

            UserLoggedModel loggedUser = new UserLoggedModel()
            {
                Username = user.Username,
                SessionKey = user.SessionKey
            };

            HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK, loggedUser);
            return response;
        }

        [HttpPut, ActionName("logout")]
        public HttpResponseMessage LogoutUser([ValueProvider(typeof(HeaderValueProviderFactory<string>))]string sessionKey)
        {

            var dbContext = new RentyDbContext();

            User user = null;
            try
            {
                user = dbContext.Users.Single(x => x.SessionKey == sessionKey);
                if (user == null)
                {
                    throw new ArgumentException("User is missing or not logged in!");
                }
            }
            catch (Exception)
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid session key provided!");
            }

            user.SessionKey = null;
            dbContext.SaveChanges();
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
