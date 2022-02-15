using UbicApi.Common.Models;

namespace UbicApi.Web.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }
}
