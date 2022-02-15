using System.Threading.Tasks;
using UbicApi.Web.Data.Entities;
using UbicApi.Web.Helpers;
using UbicApi.Common.Enums;
using System.Linq;

namespace UbicApi.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsycn();
            await CheckUserAsync("Avon","1010", "Luis", "Núñez", "luis@yopmail.com", "351 681 4963", "Espora 2051", "Cuesta Blanca", "Dirección 3", UserType.Admin);
            await CheckUserAsync("Natura", "2020", "Pablo", "Lacuadri", "pablo@yopmail.com", "351 681 4963", "Villa Santa Ana", "Zapala Neuquén", "Dirección 3", UserType.Admin);
            await CheckUserAsync("Avon", "3030", "Lionel", "Messi", "messi@yopmail.com", "311 322 4620", "Barcelona", "Rosario", "Dirección 3", UserType.User);
            await CheckUserAsync("Natura", "4040", "Diego", "Maradona", "maradona@yopmail.com", "311 322 4620", "Villa Fiorito", "Dubai", "Dirección 3", UserType.User);
            await CheckEventsAsycn();
        }

        private async Task CheckRolesAsycn()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckEventsAsycn()
        {
            if (!_context.Events.Any())
            {
                AddEvent("Registrar nuevo usuario", 2.4);
                AddEvent("Registro de direcciones", 1.3);
                AddEvent("Cambio de Password", 1.1);
                AddEvent("Olvido de Password", 2.2);
                AddEvent("Consulta Datos de un Usuario", 1.4);
                await _context.SaveChangesAsync();
            }
        }

        private void AddEvent(string eventName, double kb)
        {
            _context.Events.Add(new Event { EventName = eventName, Kb = kb });
        }

        private async Task CheckUserAsync(string modulo,string document, string firstName, string lastName, string email, string phoneNumber, string address1, string address2, string address3, UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    Modulo=modulo,
                    Address1 = address1,
                    Address2 = address2,
                    Address3 = address3,
                    Document = document,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    UserName = email,
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

            }
        }
    }
}
