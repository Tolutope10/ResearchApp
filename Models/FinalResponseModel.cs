namespace SchoolProjectAPI.Models
{
    public class FinalResponseModel
    {
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string Question { get; set; }
        public bool IsUserResponse { get; set; }
        public bool IsQuestionResponse { get;set; }
    }
}
