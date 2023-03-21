using SchoolProjectAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolProjectAPI.Services
{
    public interface IProjectInterfaceService
    {

        Task<object> GetRevolutAll();
        Task<object> UserLogin(LoginModel model);
        Task<object> CaseStudy(CaseStudyModel caseStudyModel);
        Task<object> GetAmazonAll();
        Task<object> GetUserRevolutResponseSummary(int userId);
        Task<object> GetUserAmazonResponseSummary(int userId);
        Task<object> UserResponse(UserResponseModel userModel);
        //object GetSubCategoryAll();


    }



}
