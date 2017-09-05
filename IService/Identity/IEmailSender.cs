using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IService;

namespace IService.Identity
{
    public interface IEmailSender
    {
        //Task SendEmailAsync(EmailDto emailDto);
        Task SendEmailAsync(string emailAddress, string title,string context);
    }
}