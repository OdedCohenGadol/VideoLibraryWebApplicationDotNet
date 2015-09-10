using Generic.Model;
using Skeleton.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.BLL.Models
{
    public class VideoModel: GenericModel<Video,VideoModel>
    {
        #region Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public string Brief { get; set; }
        public int Year { get; set; }
        public string Thumb { get; set; }
        #endregion

        #region Ctors
        public VideoModel()
        {

        }

        public VideoModel(Video entity)
            :base(entity)
        {

        }

        public VideoModel(SearchVideos_Result result)
        {
            Name = result.Name;
            ID = result.ID;
            Brief = result.Brief?? string.Empty;
            Director = result.Director;
            Thumb = result.Thumb ?? string.Empty ;
            Genre = result.Genre;
            Year = result.Year;


        }
        #endregion
    }
}
