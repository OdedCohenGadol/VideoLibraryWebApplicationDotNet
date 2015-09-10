using Skeleton.BLL.Interfaces;
using Skeleton.BLL.Models;
using Skeleton.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Skeleton.BLL.BL
{
    public class LoginBL : ILoginBL 
    {
        /// <summary>
        /// login action logic
        /// </summary>
        /// <param name="model">model contains credentials</param>
        /// <returns>true or false if authenticated and attached the relevant controller.</returns>
        public StatusModel<string> Login(UserLoginModel model)
        {
            var result = new StatusModel<string>();
            try
            {
                using (var repository = new Repository())
                {

                    var user = repository.UserRepository.GetByKey(model.ID);
                    if (user == null)
                    {
                        result.Success = false;
                        result.Message = "User not found";
                        return result;
                    }
                    //user founded - compare passwords
                    if (user.Password == model.Password)
                    {
                        result.Success = true;
                        result.Message = "Log In Success";
                        //access granted
                        switch (user.Name)
                        {
                            case "Admin":
                                result.Data = "Admin";
                                break;
                            default:
                                result.Data = "User/Index?id=" + model.ID;
                                break;
                        }
                    }
                    else
                    {
                        //access denied
                        result.Success = false;
                        result.Message = "Bad Credentials";
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Unexpected Error";
            }

            return result;
        }
    }
}
