using Skeleton.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.BLL.Interfaces
{
    public interface IRentBL
    {
        /// <summary>
        /// method that executing the renting action
        /// </summary>
        /// <param name="model">model containrs user id and video id</param>
        /// <returns>success/nor</returns>
        StatusModel RentVideo(RentVideoModel model);

        /// <summary>
        /// method that executing the return video action
        /// </summary>
        /// <param name="model">model containrs user id and video id</param>
        /// <returns>success/nor</returns>
        StatusModel ReturnVideo(RentVideoModel model);

        /// <summary>
        /// method that gets only user id and return his book
        /// </summary>
        /// <param name="model">contains user id</param>
        /// <returns>success/nor</returns>
        StatusModel ReturnUserVideo(int userid);
    }
}
