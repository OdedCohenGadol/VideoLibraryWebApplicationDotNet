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
    public class UserBL : IUserBL
    {
        public StatusModel<UserModel> Get(int id)
        {
            var result = new StatusModel<UserModel>(true);
            try
            {
                using (Repository repository = new Repository())
                {
                    var userEntity = repository.UserRepository.GetByKey(id);
                    if (userEntity == null)
                    {
                        result.Success = false; result.Message = "user not founded"; result.Data = new UserModel();
                        return result;
                    }
                    result.Data = new UserModel(userEntity);
                }
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "unexpected ERROR";
            }
            return result;
        }

        /// <summary>
        /// get user id, make sure he's real, return his user id and his video id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user id and video id if rented, else 0</returns>
        public StatusModel<RentVideoModel> GetRentUser(int id)
        {
            var result = new StatusModel<RentVideoModel>();
            result.Data = new RentVideoModel();
            result.Data.UserID = id; 
            try
            {
                using (Repository repository = new Repository())
                {
                    var userEntity = repository.UserRepository.GetByKey(id);
                    if (userEntity == null)
                    {
                        result.Success = false; result.Message = "user not founded"; result.Data = new RentVideoModel();
                        return result;
                    }
                    result.Data.UserName = userEntity.Name;
                    var video = repository.VideoRepository.GetUserMovie(id);
                    if (video != null)
                    {
                        result.Data.VideoID = video.ID;
                    }
	
                }
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "unexpected ERROR";
            }
            return result;
        }
    }
}
