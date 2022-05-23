using LinkDevelopmentWorkshop.Application.Users.Commands.CreateUser;
using LinkDevelopmentWorkshop.Application.Users.Queries.GetUser;
using LinkDevelopmentWorkshop.ModelDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace LinkDevelopmentWorkshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtTokensController : ControllerBase
    {
        private readonly IMediator _mediator;
       public JwtTokensController(IMediator mediator)
        {
            _mediator=  mediator;
                
        }
        [HttpPost]
        [Route("Registration")]
        public async Task<int> Registration([FromBody] RegisterRequestDto user)
        {
           
                var command = new CreateUserCommand
                {
                    UserName = user.UserName,
                    Address = user.Address,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    BirthDate = user.BirthDate,
                    Password = BCrypt.Net.BCrypt.HashPassword( user.Password),
                };
               var response= _mediator.Send(command).Result;

            return response.UserID;
           
        }



        [HttpPost]
        [Route("login")]
        public async Task<string> login([FromBody] LoginDto user)
        {

            var query = new GetUserQuery
            {
              
                Email = user.Email,
                Password = user.Password,
                
            };
            var response = _mediator.Send(query).Result;

            Response.Cookies.Append("jwt", response.Jwt,new CookieOptions() { HttpOnly=true});
            return response.Jwt;

        }

       
    }
}
