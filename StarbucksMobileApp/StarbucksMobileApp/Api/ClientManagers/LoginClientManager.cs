using StarbucksMobileApp.Api.DataStorage;
using StarbucksMobileApp.Api.ResponseModels;
using StarbucksMobileApp.Helpers;
using StarbucksMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarbucksMobileApp.Api.ClientManagers
{
    public class LoginClientManager : BaseClientManager
    {
        public LoginClientManager(string url) : base(url)
        {
        }

        public MemberResponseModel Login(string password, string email = "", string phone = "")
        {
            try
            {
                if (String.IsNullOrEmpty(email) && String.IsNullOrEmpty(phone))
                {
                    return new MemberResponseModel
                    {
                        Errors = new List<ErrorRequestModel>
                        {
                            new ErrorRequestModel { Code = "1001", Message="Email yada Telefon girin!" }
                        }
                    };
                }
                else if (String.IsNullOrEmpty(password))
                {
                    return new MemberResponseModel
                    {
                        Errors = new List<ErrorRequestModel>
                        {
                            new ErrorRequestModel { Code = "1002", Message="Şifre girin!" }
                        }
                    };
                }

                Member member = null;
                if (!String.IsNullOrEmpty(email))
                    member = DataContext.Members.Where(x => x.Email == email && x.Password == password).FirstOrDefault();

                else if (!String.IsNullOrEmpty(phone))
                    member = DataContext.Members.Where(x => x.Phone == phone && x.Password == password).FirstOrDefault();

                
                if (member != null)
                {
                    MemberResponseModel result = AutoMapperBootstrapper.mapper.Map<MemberResponseModel>(member);
                    return result;
                }
                else
                {
                    return new MemberResponseModel
                    {
                        Errors = new List<ErrorRequestModel>
                        {
                            new ErrorRequestModel { Code = "9001", Message="Not found member!" }
                        }
                    };
                }
            }
            catch (Exception err)
            {
                return new MemberResponseModel
                {
                    Errors = new List<ErrorRequestModel>
                    {
                        new ErrorRequestModel
                        {
                            Code=err.Source,
                            Message=AppHelper.GetErrorMessage(err)
                        }
                    }
                };
            }
        }

        public async Task<MemberResponseModel> LoginSync(string password, string email = "", string phone = "")
        {
            try
            {
                await Task.Delay(10);

                if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(phone))
                {
                    return new MemberResponseModel
                    {
                        Errors = new List<ErrorRequestModel>
                        {
                            new ErrorRequestModel { Code = "1001", Message="Email yada Telefon girin!" }
                        }
                    };
                }
                else if (String.IsNullOrEmpty(password))
                {
                    return new MemberResponseModel
                    {
                        Errors = new List<ErrorRequestModel>
                        {
                            new ErrorRequestModel { Code = "1002", Message="Şifre girin!" }
                        }
                    };
                }

                Member member = null;
                if (!String.IsNullOrEmpty(email))
                    member = DataContext.Members.Where(x => x.Email == email && x.Password == password).FirstOrDefault();

                else if (!String.IsNullOrEmpty(phone))
                    member = DataContext.Members.Where(x => x.Phone == phone && x.Password == password).FirstOrDefault();

                //MemberResponseModel result = _autoMapper.Map<MemberResponseModel>(member);
                if (member != null)
                {
                    return new MemberResponseModel
                    {
                        Balance = member.Balance,
                        Description = member.Description,
                        Email = member.Email,
                        IsPerson = member.IsPerson,
                        Name = member.Name,
                        Phone = member.Phone,
                        Star = member.Star
                    };
                }
                else
                {
                    return new MemberResponseModel
                    {
                        Errors = new List<ErrorRequestModel>
                        {
                            new ErrorRequestModel { Code = "9001", Message="Not found member!" }
                        }
                    };
                }

            }
            catch (Exception err)
            {
                return new MemberResponseModel
                {
                    Errors = new List<ErrorRequestModel>
                    {
                        new ErrorRequestModel
                        {
                            Code=err.Source,
                            Message=AppHelper.GetErrorMessage(err)
                        }
                    }
                };
            }
        }
    }

    public class NotificationClientManager : BaseClientManager
    {
        public NotificationClientManager(string url) : base(url)
        {
        }

        public List<Notification> GetList()
        {
            return DataContext.Notifications;
        }
    }
}
