using LinkDevelopmentWorkshop.Domain.Enums;

namespace LinkDevelopmentWorkshop.ModelDtos
{
    /// <summary>
    /// Register Request Dto
    /// </summary>
    public class RegisterRequestDto
    {
        /// <summary>
        /// User Name
        /// </summary>
        public string UserName { get; set; } = null!;
        /// <summary>
        /// User FullAddress
        /// </summary>
        public string Address { get; set; } = null!;
        /// <summary>
        /// User Email
        /// </summary>
        public string Email { get; set; } = null!;
        /// <summary>
        /// User PhoneNumber
        /// </summary>
        public string PhoneNumber { get; set; } = null!;
        /// <summary>
        /// User BirthDate
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// User Password
        /// </summary>
        public string Password { get; set; } = null!;
    }
}
