using Skeleton.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.BLL.Interfaces
{
    public interface ILoginBL
    {
        /// <summary>
        /// login action logic
        /// </summary>
        /// <param name="model">model contains credentials</param>
        /// <returns>true or false if authenticated and attached the relevant controller.</returns>
        StatusModel<string> Login(UserLoginModel model);
    }
}
