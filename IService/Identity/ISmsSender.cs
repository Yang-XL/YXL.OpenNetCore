using System.Threading.Tasks;

namespace IService.Identity
{
    public interface ISmsSender
    {

        Task SendSmsMessageAsync(string phoneNumber, string context);
    }
}