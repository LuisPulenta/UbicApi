using System;
using System.Threading.Tasks;
using UbicApi.Web.Data.Entities;
using UbicApi.Web.Models;

namespace UbicApi.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<User> ToUserAsync(UserViewModel model, string imageId, bool isNew);

        UserViewModel ToUserViewModel(User user);
    }
}
