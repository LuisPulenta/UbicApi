using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UbicApi.Web.Data;
using UbicApi.Web.Data.Entities;
using UbicApi.Common.Enums;
using UbicApi.Web.Models.Request;
using UbicApi.Web.Helpers;

namespace UbicApi.Web.Controllers.Api
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IImageHelper _imageHelper;


        public UsersController(DataContext context, IUserHelper userHelper, IMailHelper mailHelper, IImageHelper imageHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
            _imageHelper = imageHelper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Where(x => x.UserType == UserType.User)
                .ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Route("/{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            User user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

       
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = await _userHelper.GetUserAsync(request.Email);
            if (user != null)
            {
                return BadRequest("Ya existe un usuario registrado con ese email.");
            }

            string imageId = string.Empty;
            if (request.Image != null && request.Image.Length > 0)
            {
                imageId = _imageHelper.UploadImage(request.Image, "users");
            }

            user = new User
            {
                Address1 = request.Address1,
                Latitude1 = request.Latitude1,
                Longitude1 = request.Longitude1,
                Address2 = request.Address2,
                Latitude2 = request.Latitude2,
                Longitude2 = request.Longitude2,
                Address3 = request.Address3,
                Latitude3 = request.Latitude3,
                Longitude3 = request.Longitude3,
                Document = request.Document,
                Email = request.Email,
                FirstName = request.FirstName,
                ImageId = imageId,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                UserName = request.Email,
                UserType = UserType.User,
            };

            await _userHelper.AddUserAsync(user, "123456");
            await _userHelper.AddUserToRoleAsync(user, user.UserType.ToString());

            string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            string tokenLink = Url.Action("ConfirmEmail", "Account", new
            {
                userid = user.Id,
                token = myToken
            }, protocol: HttpContext.Request.Scheme);

            _mailHelper.SendMail(request.Email, "Vehicles - Confirmación de cuenta", $"<h1>Vehicles - Confirmación de cuenta</h1>" +
                $"Para habilitar el usuario, " +
                $"por favor hacer clic en el siguiente enlace: </br></br><a href = \"{tokenLink}\">Confirmar Email</a>");

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, UserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = await _userHelper.GetUserAsync(request.Email);
            if (user == null)
            {
                return BadRequest("No existe el usuario.");
            }

            string imageId = user.ImageId;
            if (request.Image != null && request.Image.Length > 0)
            {
                imageId = _imageHelper.UploadImage(request.Image, "users");
            }

            user.Address1 = request.Address1;
            user.Address2 = request.Address2;
            user.Address3 = request.Address3;

            user.Latitude1 = request.Latitude1;
            user.Longitude1 = request.Longitude1;

            user.Latitude2 = request.Latitude2;
            user.Longitude2 = request.Longitude2;

            user.Latitude3 = request.Latitude3;
            user.Longitude3 = request.Longitude3;

            user.Document = request.Document;
            user.FirstName = request.FirstName;
            user.ImageId = imageId;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;

            await _userHelper.UpdateUserAsync(user);
            return Ok(user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            User user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
