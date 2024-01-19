using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using UserData;
namespace UserService
{
    public interface Iuser
    {
        IEnumerable<User> GetUserData();
        public User PostUser(User user);

        public ActionResult<string> Login(Login login);
    }
}