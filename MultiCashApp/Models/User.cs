using Microsoft.AspNetCore.Identity;
using MultiCashApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiCashApp.Models
{ 

    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }   
        public string Name { get; set; }
        public string EmplyeeID { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }  
    }
}
