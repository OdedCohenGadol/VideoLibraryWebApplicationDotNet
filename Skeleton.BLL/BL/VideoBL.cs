using Skeleton.BLL.Interfaces;
using Skeleton.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skeleton.DAL;

namespace Skeleton.BLL.BL
{
    public class VideoBL : IVideoBL
    {

        /// <summary>
        /// method that return all relevant videos
        /// </summary>
        /// <param name="searchModel">model contains all the search parameters</param>
        /// <returns>succes/nor and relevant videos</returns>
        public StatusModel<List<VideoDataTableModel>> GetVideos(VideoSearchModel searchModel)
        {
            var result = new StatusModel<List<VideoDataTableModel>>(true);
            try
            {

                using (var repository = new Repository())
                {
                    result.Data = repository.VideoRepository.SearchVideos(searchModel.Name, searchModel.Director, searchModel.Genre, searchModel.Year).Select(v => new VideoDataTableModel(v)).OrderBy(v=>v.IsRented).ThenByDescending(v=>v.Year).ToList();
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "unexepcted Error";
            }

            return result;
        }



        /// <summary>
        /// remove specifiec video from DB
        /// </summary>
        /// <param name="model">model contains video id</param>
        /// <returns>success/nor</returns>
        public StatusModel Remove(VideoModel model)
        {
            var result = new StatusModel(true);
            try
            {
                using (Repository repository = new Repository())
                {
                    var video = repository.VideoRepository.GetByKey(model.ID);
                    if (video == null)
                    {
                        result.Success = false; result.Message = "Video not found";
                        return result;
                    }
                    if (video.RenterID != null)
                    {
                        result.Success = false; result.Message = "Video is rented";
                        return result;
                    }

                    repository.VideoRepository.Delete(video);
                    repository.VideoRepository.Commit();
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
        /// add specifiec video to DB
        /// </summary>
        /// <param name="model">model contains video properties</param>
        /// <returns>success/nor</returns>
        public StatusModel Add(VideoModel model)
        {
            var result = new StatusModel(true);
            try
            {
                using (Repository repository = new Repository())
                {
                    var video = model.GetEntity();
                    repository.VideoRepository.Insert(video);
                    repository.VideoRepository.Commit();
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
        /// edit specifiec video from DB or add new one
        /// </summary>
        /// <param name="model">model contains video properties</param>
        /// <returns>success/nor</returns>
        public StatusModel Edit(VideoModel model)
        {
            var result = new StatusModel(true, "Save Completed Successfully");
            try
            {
                using (Repository repository = new Repository())
                {
                    if (model.ID == 0)
                    {
                        //add
                        var video = new Video();
                        video.Brief = model.Brief;
                        video.Director = model.Director;
                        video.Genre = model.Genre;
                        video.Name = model.Name;
                        video.Year = model.Year;

                        repository.VideoRepository.Insert(video);
                    }
                    else
                    {

                        //edit
                        var video = repository.VideoRepository.GetByKey(model.ID);
                        video.Brief = model.Brief;
                        video.Director = model.Director;
                        video.Genre = model.Genre;
                        video.Name = model.Name;
                        video.Year = model.Year;
                    }


                    repository.VideoRepository.Commit();
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
        /// get specifiec video from DB
        /// </summary>
        /// <param name="model">model contains video id</param>
        /// <returns>success/nor and entity model</returns>
        public StatusModel<VideoModel> Get(VideoModel model)
        {
            var result = new StatusModel<VideoModel>(true);
            try
            {
                using (Repository repository = new Repository())
                {
                    var videoEntity = repository.VideoRepository.GetByKey(model.ID);
                    if (videoEntity == null)
                    {
                        result.Success = false; result.Message = "Video not founded";
                        return result;
                    }
                    result.Data = new VideoModel(videoEntity);
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
        /// save the thumb name for specific video
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="fName"></param>
        /// <returns>success/nor</returns>
        public StatusModel SaveThumbName(int videoId, string fName)
        {
            var result = new StatusModel(true,"sacve thumb in DB success");
            try
            {
                using (Repository repository = new Repository())
                {
                    var video = repository.VideoRepository.GetByKey(videoId);
                    if (video == null)
                    {
                        result.Success = false;
                        result.Message = "video not exist";
                        return result;
                    }
                    video.Thumb = fName;
                    repository.VideoRepository.Commit();
                }
            }
            catch (Exception ex)
            {
                result.Success = false; result.Message = "error: " + ex.Message;
            }
            return result;
        }
    }
}
