using System.ComponentModel.DataAnnotations;
namespace ShopBridge.Product.Model.Models
{
    public class User
    {
        [Required(ErrorMessage = "Please Add User Name ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Add Email")]
        public string Password { get; set; }
    }
}