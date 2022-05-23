using LinkDevelopmentWorkshop.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand: IRequest<UserResponse>
    {
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
    }
}
