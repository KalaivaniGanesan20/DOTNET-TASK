namespace InterfaceExample
{
    public class Employee
    {
        public int employeeid{get;set;}
        public string firstname{get;set;}
        public string lastname{get;set;}
        public string address{get;set;}
        public long mobilenumber{get;set;}
    }
    public class Manager:Employee
    {
      public string projectid{get;set;}
      public string projectname{get;set;}
      public DateTime deploymentdate{get;set;}
    }
    public class Admin:Employee
    {
       public int AdminaccessId{get;set;}

    }
}