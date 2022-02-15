using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UbicApi.Web.Data;
using UbicApi.Web.Data.Entities;
using UbicApi.Web.Helpers;

namespace UbicApi.Web.Controllers.Api
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]

    public class Users2Controller : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IImageHelper _imageHelper;


        public Users2Controller(DataContext context, IUserHelper userHelper, IMailHelper mailHelper, IImageHelper imageHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
            _imageHelper = imageHelper;
        }

        // GET: api/Users/5
        [HttpGet("{document}")]
        [Route("GetUserByDocumentNoToken/{document}")]
        public async Task<ActionResult<User>> GetUserByDocumentNoToken(string document)
        {
            User user = await _context.Users
                .FirstOrDefaultAsync(x => x.Document == document);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("{document}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetUserByDocument/{document}")]
        public async Task<ActionResult<User>> GetUserByDocument(string document)
        {
            User user = await _context.Users
                .FirstOrDefaultAsync(x => x.Document == document);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}