using System.ComponentModel.DataAnnotations;

namespace ViewModels.AdminWeb.User
{
    public class AccountLogin
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入登录名")]
        [Display(Name = "登录名")]
        public string LoginName { get; set; }

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false,ErrorMessage = "请输入密码")]
        [Display(Name = "密码")]
        public string LoginPassword { get; set; }

        [Display(Name = "记住我？")]
        public bool Remember { get; set; }

        public  string ReturnUrl { get; set; }
    }
}