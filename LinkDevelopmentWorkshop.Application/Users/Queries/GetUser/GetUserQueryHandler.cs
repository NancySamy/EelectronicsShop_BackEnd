using LinkDevelopmentWorkshop.Application.Localization;
using LinkDevelopmentWorkshop.Application.Services;
using LinkDevelopmentWorkshop.Repo.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Application.Users.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse>
    {
        private IUnitOfWork _unitOfWork;
        private readonly JwtService _Service;
        private readonly ILogger<GetUserQueryHandler> _logger;

        public GetUserQueryHandler(IUnitOfWork unitOfWork, JwtService Service, ILogger<GetUserQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger= logger;    
            _Service = Service; 
            
        }
        public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var existedUser= _unitOfWork.User.FindFirstOrDefualtByCondition(u=>u.Email==request.Email);
            if (existedUser == null)
            {
                _logger.LogWarning(Resources.InvalidCredentials);
                return new GetUserResponse() { Jwt = "" };
            }
            var isValidPassword=  BCrypt.Net.BCrypt.Verify(request.Password, existedUser.Password);
            if(!isValidPassword)
            {
                _logger.LogWarning(Resources.InvalidCredentials);
                return new GetUserResponse() { Jwt = "" };
            }
            return new GetUserResponse() { Jwt = _Service.GenerateToken(existedUser) };
        }
    }
}
