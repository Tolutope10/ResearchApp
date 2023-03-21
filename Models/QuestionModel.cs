namespace SchoolProjectAPI.Models
{
    public class QuestionModel
    {
        public int QuestionId { get; set; }
        public string Descriptions { get; set; }

        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }

        public bool IsQuestionResponse { get; set; }
    }
}
