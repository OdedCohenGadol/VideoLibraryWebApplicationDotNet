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
    public class RentBL : IRentBL
    {
        /// <summary>
        /// method that executing the renting action
        /// </summary>
        /// <param name="model">model containrs user id and video id</param>
        /// <returns>success/nor</returns>
        public StatusModel RentVideo(RentVideoModel model)
        {
            var result = new StatusModel(); result.Success = true;
            try
            {

                using (var repository = new Repository())
                {
                    //check if user already renting video and if video is 'free'
                    var user = repository.UserRepository.GetByKey(model.UserID);
                    var video = repository.VideoRepository.GetByKey(model.VideoID);

                    if (user == null || video == null)
                    {
                        result.Success = false;
                        result.Message = "Bad parameters";
                        return result;
                    }

                    if (user.IsRenting || video.RenterID != null)
                    {
                        result.Success = false;
                        result.Message = "rent proccess cannot be exceuted";
                        return result;
                    }

                    //commit renting
                    user.IsRenting = true;
                    video.RenterID = user.ID;
                    repository.UserRepository.Commit();
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "unexpected ERROR";
            }

            return result;

        }

        /// <summary>
        /// method that executing the return video action
        /// </summary>
        /// <param name="model">model containrs user id and video id</param>
        /// <returns>success/nor</returns>
        public StatusModel ReturnVideo(RentVideoModel model)
        {
            var result = new StatusModel(); result.Success = true;
            try
            {
                using (var repository = new Repository())
                {
                    //check if user is renting currently video and if video's renter id is that user
                    var user = repository.UserRepository.GetByKey(model.UserID);
                    var video = repository.VideoRepository.GetByKey(model.VideoID);

                    if (user == null || video == null)
                    {
                        result.Success = false;
                        result.Message = "Bad parameters";
                        return result;
                    }

                    if (!user.IsRenting || video.RenterID != user.ID)
                    {
                        result.Success = false;
                        result.Message = "return video proccess cannot be exceuted";
                        return result;
                    }

                    //commit returning video
                    user.IsRenting = false;
                    video.RenterID = null;
                    repository.UserRepository.Commit();
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "unexpected ERROR";
            }
            return result;
        }

        /// <summary>
        /// method that gets only user id and return his book
        /// </summary>
        /// <param name="model">contains user id</param>
        /// <returns>success/nor</returns>
        public StatusModel ReturnUserVideo(int userid)
        {
            var result = new StatusModel(); result.Success = true;
            try
            {
                using (var repository = new Repository())
                {
                    //check if user is renting currently video and if video's renter id is that user
                    var user = repository.UserRepository.GetByKey(userid);
                    if (user == null || !user.IsRenting)
                    {
                         result.Success = false;
                        result.Message = "user doesnt renting any movie ";
                        return result;
                    }
                    var video = repository.VideoRepository.GetUserMovie(userid);

                    if ( video == null)
                    {
                        result.Success = false;
                        result.Message = "user movie not founded";
                        return result;
                    }

                    if (!user.IsRenting || video.RenterID != user.ID)
                    {
                        result.Success = false;
                        result.Message = "return video proccess cannot be exceuted";
                        return result;
                    }

                    //commit returning video
                    user.IsRenting = false;
                    video.RenterID = null;
                    repository.UserRepository.Commit();
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "unexpected ERROR";
            }
            return result;
        }
    }
}
