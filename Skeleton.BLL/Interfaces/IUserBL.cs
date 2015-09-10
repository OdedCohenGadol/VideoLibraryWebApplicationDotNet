using Skeleton.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.BLL.Interfaces
{
    public interface IUserBL
    {
        StatusModel<UserModel> Get(int id);
        /// <summary>
        /// get user id, make sure he's real, return his user id and his video id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user id and video id if rented, else 0</returns>
        StatusModel<RentVideoModel> GetRentUser(int id);

    }
}
