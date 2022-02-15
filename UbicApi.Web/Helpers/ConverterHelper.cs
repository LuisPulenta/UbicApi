using System;
using System.Threading.Tasks;
using UbicApi.Web.Data;
using UbicApi.Web.Data.Entities;
using UbicApi.Web.Models;

namespace UbicApi.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;

        public ConverterHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<User> ToUserAsync(UserViewModel model, string imageId, bool isNew)
        {
            return new User
            {
                Address1 = model.Address1,
                Address2 = model.Address2,
                Address3 = model.Address3,
                Document = model.Document,
                Email = model.Email,
                FirstName = model.FirstName,
                Id = isNew ? Guid.NewGuid().ToString() : model.Id,
                ImageId = imageId,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email,
                UserType = model.UserType,
            };
        }

        public UserViewModel ToUserViewModel(User user)
        {
            return new UserViewModel
            {
                Address1 = user.Address1,
                Address2 = user.Address2,
                Address3 = user.Address3,
                Document = user.Document,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                ImageId = user.ImageId,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                UserType = user.UserType,
            };
        }
    }
}
