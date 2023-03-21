namespace SchoolProjectAPI.Models
{
    public class UserResponseModel
    {
        public string Description { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; } 
        public bool IsUserResponse { get; set; }    
        
}

    public class UsersResponseModel
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public bool IsUserResponse { get; set; }

    }
}
