using Microsoft.Extensions.Configuration;
using MySqlConnector;
using SchoolProjectAPI.context;
using SchoolProjectAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProjectAPI.Services
{
    public class ProjectInterfaceService : IProjectInterfaceService
    {
        private readonly IConfiguration _config;
        public ProjectInterfaceService(IConfiguration config)
        {
            _config = config;

        }
        public async Task<object> GetRevolutAll()
        {
            List<CategoryModel> list = new List<CategoryModel>();

            List<QuestionReturnObj> questionResponseList = new List<QuestionReturnObj>();
            var connString = _config.GetSection("ConnectionStrings").GetSection("Default").Value;
            using var connection = new MySqlConnection(connString);
            await connection.OpenAsync();
            using var getAllAmazonQuestions = new MySqlCommand("Select questions.questionId, questions.Descriptions, questions.STID, questions.QRID, questions.IsQuestionResponse From questions Left Join qualityrequirement on questions.QRID = qualityrequirement.QRID Left Join casestudytype on qualityrequirement.CSTID = casestudytype.CSTID where casestudytype.CSTID = 4;", connection);
            using var reader = await getAllAmazonQuestions.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var value = reader.GetValue(0);
                list.Add(new CategoryModel()
                {
                    questionId = Convert.ToInt32(reader["questionId"]),
                    question = reader["Descriptions"].ToString(),
                    STID = Convert.ToInt32(reader["STID"]),
                    QRID = Convert.ToInt32(reader["QRID"])
                });

            }
            connection.Dispose();
            foreach (var model in list)
            {
                var subCategoryName = string.Empty;
                List<QuestionModel> questionList = new List<QuestionModel>();
                //await connection.OpenAsync();
                using var newConnection = new MySqlConnection(connString);
                await newConnection.OpenAsync();
                using var getCategory = new MySqlCommand($"SELECT * FROM qualityrequirement WHERE QRID ={model.QRID}", newConnection);
                using var cat = await getCategory.ExecuteReaderAsync();
                await cat.ReadAsync();
                var categoryName = cat["Name"].ToString();
                newConnection.Dispose();
                if (model.STID != 0)
                {
                    using var newsConnection = new MySqlConnection(connString);
                    await newsConnection.OpenAsync();
                    using var getSubCategory = new MySqlCommand($"SELECT * FROM qualitysubtypes WHERE STID={model.STID}", newsConnection);
                    using var sub = await getSubCategory.ExecuteReaderAsync();
                    await sub.ReadAsync();
                    subCategoryName = sub["Name"].ToString();
                    newsConnection.Dispose();
                }
                if (model.STID != 0)
                {
                    using var questionConnection = new MySqlConnection(connString);
                    await questionConnection.OpenAsync();
                    using var getQuestions = new MySqlCommand($"SELECT * FROM questions WHERE STID = {model.STID}", questionConnection);
                    using var question = await getQuestions.ExecuteReaderAsync();
                    while (await question.ReadAsync())
                    {
                        questionList.Add(new QuestionModel()
                        {
                            QuestionId = Convert.ToInt32(question["questionId"]),
                            Descriptions = question["Descriptions"].ToString()
                        });
                    }
                    questionConnection.Dispose();
                }
                else
                {
                    using var newsQuestionConnection = new MySqlConnection(connString);
                    await newsQuestionConnection.OpenAsync();
                    using var getNewQuestions = new MySqlCommand($"SELECT * FROM questions WHERE QRID = {model.QRID}", newsQuestionConnection);
                    using var questions = await getNewQuestions.ExecuteReaderAsync();
                    while (await questions.ReadAsync())
                    {
                        questionList.Add(new QuestionModel()
                        {
                            QuestionId = Convert.ToInt32(questions["questionId"]),
                            Descriptions = questions["Descriptions"].ToString()
                        });
                    }

                    getNewQuestions.Dispose();
                }
                var validCategoryName = questionResponseList.Where(x => x.categoryName == categoryName && x.subCategoryName == subCategoryName).FirstOrDefault();
                var validSubCategoryName = questionResponseList.Where(x => x.subCategoryName == subCategoryName).FirstOrDefault();
                if (validCategoryName is null)
                {
                    questionResponseList.Add(new QuestionReturnObj()
                    {
                        categoryName = categoryName,
                        subCategoryName = subCategoryName,
                        questions = questionList
                    });
                }
            }
            // return questionResponseList;
            if (questionResponseList.Count > 0)
            {
                var model = new
                {
                    status = 200,
                    response = questionResponseList.ToArray()
                };
                return model;

            }

            return null;
        }



        public async Task<object> GetAmazonAll()
        {
            List<CategoryModel> list = new List<CategoryModel>();

            List<QuestionReturnObj> questionResponseList = new List<QuestionReturnObj>();
            var connString = _config.GetSection("ConnectionStrings").GetSection("Default").Value;
            using var connection = new MySqlConnection(connString);
            await connection.OpenAsync();
            using var getAllAmazonQuestions = new MySqlCommand("Select questions.questionId, questions.Descriptions, questions.STID, questions.QRID, questions.IsQuestionResponse From questions Left Join qualityrequirement on questions.QRID = qualityrequirement.QRID Left Join casestudytype on qualityrequirement.CSTID = casestudytype.CSTID where casestudytype.CSTID = 5;", connection);
            using var reader = await getAllAmazonQuestions.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var value = reader.GetValue(0);
                list.Add(new CategoryModel()
                {
                    questionId = Convert.ToInt32(reader["questionId"]),
                    question = reader["Descriptions"].ToString(),
                    STID = Convert.ToInt32(reader["STID"]),
                    QRID = Convert.ToInt32(reader["QRID"])
                });

            }
            connection.Dispose();
            foreach (var model in list)
            {
                var subCategoryName = string.Empty;
                List<QuestionModel> questionList = new List<QuestionModel>();
                //await connection.OpenAsync();
                using var newConnection = new MySqlConnection(connString);
                await newConnection.OpenAsync();
                using var getCategory = new MySqlCommand($"SELECT * FROM qualityrequirement WHERE QRID ={model.QRID}", newConnection);
                using var cat = await getCategory.ExecuteReaderAsync();
                await cat.ReadAsync();
                var categoryName = cat["Name"].ToString();
                newConnection.Dispose();
                if (model.STID != 0)
                {
                    using var newsConnection = new MySqlConnection(connString);
                    await newsConnection.OpenAsync();
                    using var getSubCategory = new MySqlCommand($"SELECT * FROM qualitysubtypes WHERE STID={model.STID}", newsConnection);
                    using var sub = await getSubCategory.ExecuteReaderAsync();
                    await sub.ReadAsync();
                    subCategoryName = sub["Name"].ToString();
                    newsConnection.Dispose();
                }
                if (model.STID != 0)
                {
                    using var questionConnection = new MySqlConnection(connString);
                    await questionConnection.OpenAsync();
                    using var getQuestions = new MySqlCommand($"SELECT * FROM questions WHERE STID = {model.STID}", questionConnection);
                    using var question = await getQuestions.ExecuteReaderAsync();
                    while (await question.ReadAsync())
                    {
                        questionList.Add(new QuestionModel()
                        {
                            QuestionId = Convert.ToInt32(question["questionId"]),
                            Descriptions = question["Descriptions"].ToString()
                        });
                    }
                    questionConnection.Dispose();
                }
                else
                {
                    using var newsQuestionConnection = new MySqlConnection(connString);
                    await newsQuestionConnection.OpenAsync();
                    using var getNewQuestions = new MySqlCommand($"SELECT * FROM questions WHERE QRID = {model.QRID}", newsQuestionConnection);
                    using var questions = await getNewQuestions.ExecuteReaderAsync();
                    while (await questions.ReadAsync())
                    {
                        questionList.Add(new QuestionModel()
                        {
                            QuestionId = Convert.ToInt32(questions["questionId"]),
                            Descriptions = questions["Descriptions"].ToString()
                        });
                    }

                    getNewQuestions.Dispose();
                }
                var validCategoryName = questionResponseList.Where(x => x.categoryName == categoryName && x.subCategoryName == subCategoryName).FirstOrDefault();
                var validSubCategoryName = questionResponseList.Where(x => x.subCategoryName == subCategoryName).FirstOrDefault();
                if (validCategoryName is null)
                {
                    questionResponseList.Add(new QuestionReturnObj()
                    {
                        categoryName = categoryName,
                        subCategoryName = subCategoryName,
                        questions = questionList
                    });
                }
            }
            // return questionResponseList;
            if (questionResponseList.Count > 0)
            {
                var model = new
                {
                    status = 200,
                    response = questionResponseList.ToArray()
                };
                return model;

            }

            return null;
        }

        public async Task<object> GetUserRevolutResponseSummary(int userId)
        {
            List<ResponseModel> list = new List<ResponseModel>();

            List<QuestionModel> questionModelList = new List<QuestionModel>();
            List<FinalResponseModel> finalResponseModel = new List<FinalResponseModel>();
            List<QuestionReturnObj> questionResponseList = new List<QuestionReturnObj>();
            var connString = _config.GetSection("ConnectionStrings").GetSection("Default").Value;
            using var connection = new MySqlConnection(connString);
            await connection.OpenAsync();
            using var getNewResponse = new MySqlCommand($"SELECT * FROM responses WHERE UserId = {userId}", connection);
            using var responses = await getNewResponse.ExecuteReaderAsync();
            while (await responses.ReadAsync())
            {
                list.Add(new ResponseModel()
                {
                    QuestionId = Convert.ToInt32(responses["questionId"]),
                    Response = responses.GetBoolean("IsUserResponse"),

                });
            }
            await responses.DisposeAsync();
            using var questionConnection = new MySqlConnection(connString);
            await questionConnection.OpenAsync();
            using var getNewQuestions = new MySqlCommand($"Select questions.questionId, questions.Descriptions, questions.STID, questions.QRID, questions.IsQuestionResponse From questions Left Join qualityrequirement on questions.QRID = qualityrequirement.QRID Left Join casestudytype on qualityrequirement.CSTID = casestudytype.CSTID where casestudytype.CSTID = 4", connection);
            using var question = await getNewQuestions.ExecuteReaderAsync();
            while (await question.ReadAsync())
            {
                questionModelList.Add(new QuestionModel()
                {
                    QuestionId = Convert.ToInt32(question["questionId"]),
                    IsQuestionResponse = question.GetBoolean("IsQuestionResponse"),
                    Descriptions = question["Descriptions"].ToString(),
                    SubCategoryId = Convert.ToInt32(question["STID"]),
                    CategoryId = Convert.ToInt32(question["QRID"]),


                });
            }
            await question.DisposeAsync();
            foreach (var questionModel in questionModelList)
            {
                var response = list.Where(x => x.QuestionId == questionModel.QuestionId).FirstOrDefault();
                if (response != null) 
                if (questionModel.IsQuestionResponse != response.Response)
                {
                        using var catConnection = new MySqlConnection(connString);
                        await catConnection.OpenAsync();
                        using var getCategory = new MySqlCommand($"SELECT * FROM qualityrequirement WHERE QRID={questionModel.CategoryId}", catConnection);
                        using var category = await getCategory.ExecuteReaderAsync();
                        await category.ReadAsync();
                        var categoryName = category["Name"].ToString();
                        await catConnection.DisposeAsync();

                        using var subConnection = new MySqlConnection(connString);
                        await subConnection.OpenAsync();
                        var subCategoryName = string.Empty;
                        if (questionModel.SubCategoryId > 0)
                        {
                            using var getSubCategory = new MySqlCommand($"SELECT * FROM qualitysubtypes WHERE STID={questionModel.SubCategoryId}", subConnection);
                            using var subCategory = await getSubCategory.ExecuteReaderAsync();
                            await subCategory.ReadAsync();
                            subCategoryName = subCategory["Name"].ToString();
                            subCategory.Dispose();
                        }
                        finalResponseModel.Add(new FinalResponseModel()
                    {
                        CategoryName = categoryName,
                        SubCategoryName = subCategoryName,
                        Question = questionModel.Descriptions,
                        IsQuestionResponse = questionModel.IsQuestionResponse,
                        IsUserResponse = response.Response
                        });
                }

            }
            return finalResponseModel;
        }


        public async Task<object> GetUserAmazonResponseSummary(int userId)
        {
            List<ResponseModel> list = new List<ResponseModel>();

            List<QuestionModel> questionModelList = new List<QuestionModel>();
            List<FinalResponseModel> finalResponseModel = new List<FinalResponseModel>();
            List<QuestionReturnObj> questionResponseList = new List<QuestionReturnObj>();
            var connString = _config.GetSection("ConnectionStrings").GetSection("Default").Value;
            using var connection = new MySqlConnection(connString);
            await connection.OpenAsync();
            using var getNewResponse = new MySqlCommand($"SELECT * FROM responses WHERE UserId = {userId}", connection);
            using var responses = await getNewResponse.ExecuteReaderAsync();
            while (await responses.ReadAsync())
            {
                list.Add(new ResponseModel()
                {
                    QuestionId = Convert.ToInt32(responses["questionId"]),
                    Response = responses.GetBoolean("IsUserResponse"),

                });
            }
            await responses.DisposeAsync();
            using var questionConnection = new MySqlConnection(connString);
            await questionConnection.OpenAsync();
            using var getNewQuestions = new MySqlCommand($"Select questions.questionId, questions.Descriptions, questions.STID, questions.QRID, questions.IsQuestionResponse From questions Left Join qualityrequirement on questions.QRID = qualityrequirement.QRID Left Join casestudytype on qualityrequirement.CSTID = casestudytype.CSTID where casestudytype.CSTID = 5", connection);
            using var question = await getNewQuestions.ExecuteReaderAsync();
            while (await question.ReadAsync())
            {
                questionModelList.Add(new QuestionModel()
                {
                    QuestionId = Convert.ToInt32(question["questionId"]),
                    IsQuestionResponse = question.GetBoolean("IsQuestionResponse"),
                    Descriptions = question["Descriptions"].ToString(),
                    SubCategoryId = Convert.ToInt32(question["STID"]),
                    CategoryId = Convert.ToInt32(question["QRID"]),


                });
            }
            await question.DisposeAsync();
            foreach (var questionModel in questionModelList)
            {
                var response = list.Where(x => x.QuestionId == questionModel.QuestionId).FirstOrDefault();
                if (response != null)
                    if (questionModel.IsQuestionResponse != response.Response)
                    {
                        using var catConnection = new MySqlConnection(connString);
                        await catConnection.OpenAsync();
                        using var getCategory = new MySqlCommand($"SELECT * FROM qualityrequirement WHERE QRID={questionModel.CategoryId}", catConnection);
                        using var category = await getCategory.ExecuteReaderAsync();
                        await category.ReadAsync();
                        var categoryName = category["Name"].ToString();
                        await catConnection.DisposeAsync();

                        using var subConnection = new MySqlConnection(connString);
                        await subConnection.OpenAsync();
                        var subCategoryName = string.Empty;
                        if (questionModel.SubCategoryId > 0)
                        {
                            using var getSubCategory = new MySqlCommand($"SELECT * FROM qualitysubtypes WHERE STID={questionModel.SubCategoryId}", subConnection);
                            using var subCategory = await getSubCategory.ExecuteReaderAsync();
                            await subCategory.ReadAsync();
                            subCategoryName = subCategory["Name"].ToString();
                            subCategory.Dispose();
                        }
                        finalResponseModel.Add(new FinalResponseModel()
                        {
                            CategoryName = categoryName,
                            SubCategoryName = subCategoryName,
                            Question = questionModel.Descriptions,
                            IsQuestionResponse = questionModel.IsQuestionResponse,
                            IsUserResponse = response.Response
                        });
                    }

            }
            return finalResponseModel;
        }

        public async Task<object> UserLogin(LoginModel data)
        {
            List<UserLoginResponse> userList = new List<UserLoginResponse>();
            var connString = _config.GetSection("ConnectionStrings").GetSection("Default").Value;
            using var newsConnection = new MySqlConnection(connString);
            await newsConnection.OpenAsync();
            using var login = new MySqlCommand($"SELECT * FROM users WHERE email=@email", newsConnection);
            //login.Parameters.Add("@email", data.Email);
            login.Parameters.AddWithValue("@email", data.Email);
            using var userLogin = await login.ExecuteReaderAsync();
            while (await userLogin.ReadAsync())
            {
                userList.Add(new UserLoginResponse()
                {
                    email = userLogin["email"].ToString(),
                    roleId = Convert.ToInt32(userLogin["roleId"]),
                    userId = Convert.ToInt32(userLogin["userId"])
                });
            }
            var model = new
            {
                status = 200,
                response = userList.ToArray()
            };
            return model;
        }

        public async Task<object> CaseStudy(CaseStudyModel caseStudyModel)
        {
            List<UserLoginResponse> userList = new List<UserLoginResponse>();
            var connString = _config.GetSection("ConnectionStrings").GetSection("Default").Value;
            using var newsConnection = new MySqlConnection(connString);
            await newsConnection.OpenAsync();
           
            using var login = new MySqlCommand(@"INSERT INTO `casestudy` (`CSTID`, `UserId`, `IsResponse`) VALUES (@CSTID,@UserId,@IsResponse);", newsConnection);
            login.Parameters.AddWithValue("@CSTID", caseStudyModel.CSTID);
            login.Parameters.AddWithValue("@UserId", caseStudyModel.userId);
            login.Parameters.AddWithValue("@IsResponse", caseStudyModel.isResponse);

            int result = await login.ExecuteNonQueryAsync();
            var model = new
            {
                status = 200,
                response = userList.ToArray()
            };
            return model;
        }

        public async Task<object> UserResponse(UserResponseModel userModel)
        {
            List<UserResponseModel> userList = new List<UserResponseModel>();
            var connString = _config.GetSection("ConnectionStrings").GetSection("Default").Value;
            using var newsConnection = new MySqlConnection(connString);
            await newsConnection.OpenAsync();
            using var login = new MySqlCommand(@"INSERT INTO `responses` (`UserId`, `QuestionId`, `IsUserResponse`) VALUES (@UserId,@QuestionId,@IsUserresponse);", newsConnection);
            login.Parameters.AddWithValue("@UserId", userModel.UserId);
            login.Parameters.AddWithValue("@QuestionId", userModel.QuestionId);
            login.Parameters.AddWithValue("@IsUserresponse", userModel.IsUserResponse);
            int result = await login.ExecuteNonQueryAsync();
            var model = new
            {
                status = 200,
                response = userList.ToArray()
            };
            return model;
        }
    }
}
