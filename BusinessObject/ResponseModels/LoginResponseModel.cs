namespace BusinessObject.ResponseModels
{
    public class LoginResponseModel
    {
        public string Role { get; set; }
        public UserResponseModel? User { get; set; }
    }
}