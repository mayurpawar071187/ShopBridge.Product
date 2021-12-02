using System.ComponentModel.DataAnnotations;
namespace ShopBridge.Product.Model.Models
{
    public class RegisterUser:User
    {
        [Required(ErrorMessage = "Please Add Password")]
        public string Email { get; set; }
        public string UserRole { get; set; }
    }
}