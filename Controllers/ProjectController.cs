using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProjectAPI.Models;
using SchoolProjectAPI.Services;
using System.Threading.Tasks;

namespace SchoolProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectInterfaceService _services;
        public ProjectController(IProjectInterfaceService services)
        {
            _services = services;
        }

        [HttpGet]
        [Route("revolut")]
        public async Task<IActionResult> GetAllRevolut()
        {
            var services = await _services.GetRevolutAll();
            return Ok(services);
        }


        [HttpGet]
        [Route("amazon")]
        public async Task<IActionResult> GetAllAmazonQuestion()
        {
            var services = await _services.GetAmazonAll();
            return Ok(services);
        }

        [HttpGet]
        [Route("revolutresponse")]
        public async Task<IActionResult> GetAllUserRevolutResponse(int userId)
        {
            var services = await _services.GetUserRevolutResponseSummary(userId);
            return Ok(services);
        }

        [HttpGet]
        [Route("amazonresponse")]
        public async Task<IActionResult> GetAllUserAmazonResponse(int userId)
        {
            var services = await _services.GetUserAmazonResponseSummary(userId);
            return Ok(services);
        }

        [HttpPost]
        [Route("userlogin")]
        public async Task<IActionResult> UserLogin([FromBody] LoginModel model)
        {
            var services = await _services.UserLogin(model);
            return Ok(services);
        }

        [HttpPost]
        [Route("casestudy")]
        public async Task<IActionResult> CaseStudy([FromBody] CaseStudyModel model)
        {
            var services = await _services.CaseStudy(model);
            return Ok(services);
        }

        [HttpPost]
        [Route("userresponse")]
        public async Task<IActionResult> UserResponse([FromBody] UserResponseModel model)
        {
            var services = await _services.UserResponse(model);
            return Ok(services);
        }
    }
}
