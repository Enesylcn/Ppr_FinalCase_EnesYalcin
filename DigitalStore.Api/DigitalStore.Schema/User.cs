using DigitalStore.Base.Schema;

namespace DigitalStore.Schema
{
    public class UserRequest : BaseRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }


    public class UserResponse : BaseResponse
    {
        public string Id { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
