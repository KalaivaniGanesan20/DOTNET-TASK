using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserData;
using UserService;
namespace UserRegister
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController:Controller
    {
        private readonly Iuser myuser;
        public RegisterController(Iuser user)
        {
            myuser=user;
        }
        
       [HttpPost]
       public User PostUser(User user)
       {
             return myuser.PostUser(user);
       }
       [Authorize(Roles ="Admin")]
       [HttpGet]
       public IEnumerable<User> GetUser()
       {
         var result=myuser.GetUserData();
           return result;
       }

       [HttpPost("Login")]
       public ActionResult<string> Login(Login login)
       {
          return myuser.Login(login);
       }
    }
}