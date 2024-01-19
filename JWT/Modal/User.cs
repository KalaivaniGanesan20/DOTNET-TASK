using System.ComponentModel.DataAnnotations;
namespace UserData
{
    public class User
    {
        [Required]
        public int userid{get;set;}
        [Required]
        public string username{get;set;}
        [Required]
        public string emailid{get;set;}
        [Required]
        public string password{get;set;}
        [Required]
        public string conformpassword{get;set;}

        public byte[] passwordhash{get;set;}
        public byte[] passwordsalt{get;set;}

        [Required]
        public long mobilenumber{get;set;}
    }

    public class Login{
        [Required]
        public string emailid{get;set;}

        [Required]
        public string password{get;set;}
    }
}