using LinkDevelopmentWorkshop.Application.Localization;
using LinkDevelopmentWorkshop.Domain.Entities;
using LinkDevelopmentWorkshop.Domain.Enums;
using LinkDevelopmentWorkshop.Repo.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<CreateUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                var Response = new UserResponse();
                var UserEntity = Map(request,_logger);
                _unitOfWork.User.Create(UserEntity!);
                _unitOfWork.Save();
                Response.UserID = UserEntity.UserId;
                return Response;
            }
        }

        private User Map(CreateUserCommand request, ILogger<CreateUserCommandHandler> _logger)
        {
            var result = new User();

            result.UserName = request.UserName;
            result.Password = request.Password;
            result.PhoneNumber = request.PhoneNumber;
            result.Registered = true;
            result.Address = request.Address;
            result.BirthDate = request.BirthDate;
            result.Email = request.Email;
            result.RoleID = (int)RoleTypes.User;
            return result;
        }
        
    }
}
