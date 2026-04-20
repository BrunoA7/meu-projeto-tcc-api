using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace webAPI_ASPNET.Models
{
    public class User
    {
        [Key]
        [Required]
        public int ID { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string FULLNAME { get; set; }
        public string EMAIL { get; set; }
    }

    public class UserWithDepartment
    {
        public User User { get; set; }
        public DepartmentRelation DepartmentRelation { get; set; }
        public Department Department { get; set; }
    }

    public class UserWithButton
    {
        public User User { get; set; }
        public ButtonRelation ButtonRelation { get; set; }
        public Button Button { get; set; }
    }

    public class UserLogin
    {
        [Key]
        [Required]
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
    }
}