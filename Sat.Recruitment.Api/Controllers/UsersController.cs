using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Application.EntitesVM;
using Sat.Recruitment.Application.User.Command.InsertUserCommand;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator,
                               IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestVM userRequest)
        {
            var userRquest = _mapper.Map<UserRequest>(userRequest);
            var result = await _mediator.Send(new InsertUserCommandRequest(userRquest));
            return Ok(result);
        }
    }
}
