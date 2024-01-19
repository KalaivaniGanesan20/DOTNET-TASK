using UserService;
using UserData;
using UserDatabase;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace UserService
{
    public class Userservice:Iuser
    {
        public ApplicationDbContext dbcontext;

        private readonly ILoggerManager _logger;

        private readonly IConfiguration _config;
        public Userservice(ApplicationDbContext Dbcontext,ILoggerManager logger,IConfiguration config)
        {
          dbcontext= Dbcontext;
          _logger=logger;
          _config=config;
        }
        public IEnumerable<User> GetUserData()
        {
           _logger.LogInfo("User List !!!!!");
           return dbcontext.user.ToList();
        }

        public User PostUser(User myuser)
        {
          CreatePasswordHash(myuser.password,out byte[] passwordhash,out byte[] passwordsalt);
            var result=new User{
              userid=myuser.userid,
              username=myuser.username,
              emailid=myuser.emailid,
              password=myuser.password,
              conformpassword=myuser.conformpassword,
              passwordhash=passwordhash,
              passwordsalt=passwordsalt,
              mobilenumber=myuser.mobilenumber
            };
            dbcontext.user.Add(result);
            dbcontext.SaveChanges();

          _logger.LogInfo($"New User Added {myuser.username}");

            return result;
        }
        public ActionResult<string> Login(Login login)
        {
          var userexit=dbcontext.user.FirstOrDefault(user=>user.emailid==login.emailid);
          if(userexit==null)
          {
            _logger.LogError("Unauthorized user");
            
            return "UNAUTHORIZED";
          }
          if(!Verify(login.password,userexit.passwordhash,userexit.passwordsalt))
          {
            _logger.LogError("Incorrect credentials");

            return "INCORRECT PASSWORD";
          }

          var token=CreateToken(userexit);

          return "LOGIN SUCCESS "+ token;
        }
        private void CreatePasswordHash(string password,out byte[]passwordhash,out byte[] passwordsalt)
        {
          using(var hmac=new HMACSHA512())
          {
            passwordsalt=hmac.Key;
            passwordhash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
          }
        }

        private bool Verify(string password, byte[] passwordhash,byte[] passwordsalt)
        {
            var hmac=new HMACSHA512(passwordsalt);
            var ComputeHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return ComputeHash.SequenceEqual(passwordhash);
        }

        private string CreateToken(User user)
        {
           if(user.emailid=="admin2024@gmail.com")
           {
              List<Claim>claims=new List<Claim>()
              {
                new Claim(ClaimTypes.Name,user.username),
                new Claim(ClaimTypes.Role,"Admin"),
              };
              var key=new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSetting:Token").Value));
              var credentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
              var token=new JwtSecurityToken(claims:claims,expires:DateTime.Now.AddDays(1),signingCredentials:credentials);
             
             //compact and serialized format of token
              var jwt=new JwtSecurityTokenHandler().WriteToken(token);

              return jwt;
           }
           else
           {
             List<Claim>claims=new List<Claim>()
              {
                new Claim(ClaimTypes.Name,user.username),
                new Claim(ClaimTypes.Role,"User"),
              };
              var key=new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSetting:Token").Value));
              var credentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
              var token=new JwtSecurityToken(claims:claims,expires:DateTime.Now.AddDays(1),signingCredentials:credentials);
             //compact and serialized format of token
              var jwt=new JwtSecurityTokenHandler().WriteToken(token);

              return jwt;
           }
        }
    }
}