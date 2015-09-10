using Skeleton.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.BLL.Interfaces
{
    public interface IVideoBL
    {
        /// <summary>
        /// method that return all relevant videos
        /// </summary>
        /// <param name="searchModel">model contains all the search parameters</param>
        /// <returns>succes/nor and relevant videos</returns>
        StatusModel<List<VideoDataTableModel>> GetVideos(VideoSearchModel searchModel);

        /// <summary>
        /// remove specifiec video from DB
        /// </summary>
        /// <param name="model">model contains video id</param>
        /// <returns>success/nor</returns>
        StatusModel Remove(VideoModel model);

        /// <summary>
        /// edit specifiec video from DB
        /// </summary>
        /// <param name="model">model contains video properties</param>
        /// <returns>success/nor</returns>
        StatusModel Edit(VideoModel model);

        /// <summary>
        /// add specifiec video to DB
        /// </summary>
        /// <param name="model">model contains video properties</param>
        /// <returns>success/nor</returns>
        StatusModel Add(VideoModel model);

        /// <summary>
        /// get specifiec video from DB
        /// </summary>
        /// <param name="model">model contains video id</param>
        /// <returns>success/nor and entity model</returns>
        StatusModel<VideoModel> Get(VideoModel model);

        /// <summary>
        /// save the thumb name for specific video
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="fName"></param>
        /// <returns>success/nor</returns>
        StatusModel SaveThumbName(int videoId, string fName);


    }
}
