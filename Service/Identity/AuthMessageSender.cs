using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utility.Extensions;
using IService;
using IService.Identity;
using PermissionSystem;

namespace Service.Identity
{
    public class AuthMessageSender : ISmsSender
    {
        //private readonly EmailManager _emailManager;

        public AuthMessageSender(IApplicationParameterService applicationParameterService)
        {


            //var emailOption = new EmailOptions
            //{
            //    EnableSsl = Convert.ToBoolean(applicationParameterService.Get(a => a.KeyValue == "APPLICATION.SEND.EMAILE.163.SMTP.ENABLESSL").FirstOrDefault()
            //        .DataValue),
            //    SenderAccount = applicationParameterService
            //        .Get(a => a.KeyValue == "APPLICATION.SEND.EMAILE.163.SENDER.ACCOUNT").FirstOrDefault()
            //        .DataValue,
            //    SmtpServerAddress = applicationParameterService
            //        .Get(a => a.KeyValue == "APPLICATION.SEND.EMAILE.163.SMTP.SERVERADDRESS").FirstOrDefault()
            //        .DataValue,
            //    SmtpServerPort = Convert.ToInt32(applicationParameterService
            //        .Get(a => a.KeyValue == "APPLICATION.SEND.EMAILE.163.SMTP.SERVERPORT").FirstOrDefault()
            //        .DataValue),
            //    SenderPassword = applicationParameterService
            //        .Get(a => a.KeyValue == "APPLICATION.SEND.EMAILE.163.SENDER.PASSWORD").FirstOrDefault()
            //        .DataValue,
            //    SenderDisplayName = applicationParameterService
            //        .Get(a => a.KeyValue == "APPLICATION.SEND.EMAILE.163.SENDER.DISPLAYNAME").FirstOrDefault()
            //        .DataValue
            //};


            //_emailManager = new EmailManager(emailOption);
        }

        //public async Task SendEmailAsync(EmailDto emailDto)
        //{
        //    await _emailManager.SendAsync(emailDto);
        //}

        //public async Task SendEmailAsync(string emailAddress, string title, string context)
        //{
        //    await   _emailManager.SendAsync(emailAddress, title, context);
        //}

        public Task SendSmsMessageAsync(string phoneNumber, string context)
        {
            throw new NotImplementedException();
        }
    }
}
