using System;

namespace ViewModels.IdentitySite.Options
{
    public class AccountOption
    {
        /// <summary>
        ///     允许本地登录
        /// </summary>
        public bool AllowLocalLogin { get; set; }

        /// <summary>
        ///     允许记住登录
        /// </summary>
        public bool AllowRememberLogin { get; set; }

        /// <summary>
        ///     登录登录有效期
        /// </summary>
        public TimeSpan RememberMeLoginDuration => TimeSpan.FromDays(RememberMeLoginDay);

        /// <summary>
        ///     登录有效期天数
        /// </summary>
        public int RememberMeLoginDay { get; set; }

        /// <summary>
        ///     登出是否提示
        /// </summary>
        public bool ShowLogoutPrompt { get; set; }

        /// <summary>
        ///     注册后自动重定向
        /// </summary>
        public bool AutomaticRedirectAfterSignOut { get; set; }

        /// <summary>
        ///     开启windows身份验证
        /// </summary>
        public bool WindowsAuthenticationEnabled { get; set; }

        public  bool IncludeWindowsGroups { get; set; }

        /// <summary>
        ///     specify the Windows authentication schemes you want to use for authentication
        ///     指定要用于身份验证的Windows身份验证方案
        /// </summary>
        public string[] WindowsAuthenticationSchemes { get; set; }

        /// <summary>
        /// </summary>
        public string WindowsAuthenticationProviderName { get; set; }

        /// <summary>
        ///     Windows验证显示名称
        /// </summary>
        public string WindowsAuthenticationDisplayName { get; set; }

        /// <summary>
        ///     凭证错误消息无效
        ///     错误消息
        /// </summary>
        public string InvalidCredentialsErrorMessage { get; set; }
    }
}