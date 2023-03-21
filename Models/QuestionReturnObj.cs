using System.Collections.Generic;

namespace SchoolProjectAPI.Models
{
    public class QuestionReturnObj
    {

        public string categoryName { get; set; }    
        public string subCategoryName { get; set; }
        public List<QuestionModel> questions { get; set; }
    }
}
