using System.ComponentModel.DataAnnotations;

namespace ViewModels.AdminWeb.User
{
   public  class ForgotPasswordModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入登录名")]
        [Display(Name = "电子邮箱地址")]
        [DataType(DataType.EmailAddress)]
        public  string Email { get; set; }
    }
}
