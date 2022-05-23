using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Application.Users.Queries.GetUser
{
    public class GetUserQuery:IRequest<GetUserResponse>
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
