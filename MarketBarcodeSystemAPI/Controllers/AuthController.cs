using MarketBarcodeSystemAPI.Business.Abstract;
using MarketBarcodeSystemAPI.Core.Entities.Concrete;
using MarketBarcodeSystemAPI.Entities.Concrete;
using MarketBarcodeSystemAPI.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketBarcodeSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;
        private IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        //Müdürün Göreceği user listesi. Bu listeden market elemanını belirler.
        [HttpGet("GetUserList")]
        public IActionResult GetUserList() 
        {
            var result = _userService.GetUserList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //Müdürün market elemanı atama isteği.
        [HttpPost("WorkManAdd")]
        public IActionResult WorkManAdd(string eMail, Account account)
        {
            var result = _userService.WorkManAdd(eMail, account);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAccountIdForAdmin")]
        public IActionResult GetById(int userId)
        {
            var result = _userService.GetAccountIdForAdmin(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("WorkManList")]
        public IActionResult WorkManList(int AccountKey)
        {
            var result = _userService.WorkManList(AccountKey);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("DeleteWorkMan")]
        public IActionResult DeleteWorkMan(int userId)
        {
            var result = _userService.DeleteWorkMan(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetUserWithUserEmail")]
        public IActionResult GetById(string eMail)
        {
            var result = _userService.GetUserWithUserEmail(eMail);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
