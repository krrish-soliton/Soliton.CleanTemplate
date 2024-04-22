using Microsoft.AspNetCore.Mvc;
using Soliton.CleanTemplate.Core;
using Soliton.CleanTemplate.WebApi.Dto;
using System.Threading.Tasks;

namespace Soliton.CleanTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ISolitonService _solitonService) : ControllerBase
    {
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser([FromRoute] string userId)
        {
            var result = await _solitonService.GetUserById(userId);
            return result && result.Data != null ? Ok(result.Data) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] SolitonUserCreationRequest createRequest)
        {
            var result = await _solitonService.CreateUserAsync(new SolitonUser
            {
                EmailId = createRequest.EmailId,
                EmployeeId = createRequest.EmployeeId,
                Name = createRequest.Name,
            });
            return result ? Ok(result.Data) : (IActionResult)StatusCode(500, "Unable to create user");
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUser([FromBody] SolitonUserDto userDto)
        {
            var result = await _solitonService.UpdateUserAsync(new SolitonUser
            {
                EmailId = userDto.EmailId,
                EmployeeId = userDto.EmployeeId,
                Name = userDto.Name,
                Id = userDto.Id
            });
            return result ? Ok(result) : (IActionResult)StatusCode(500, "Unable to create user");
        }
    }
}
