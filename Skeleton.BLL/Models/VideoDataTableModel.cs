using Skeleton.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.BLL.Models
{
    public class VideoDataTableModel
    {

        public VideoDataTableModel(SearchVideos_Result v)
        {
            ID = v.ID;
            Name = v.Name;
            Director = v.Director;
            Genre = v.Genre;
            Brief = v.Brief;
            Year = v.Year;
            Thumb = v.Thumb;
            IsRented = v.IsRented;
        }


        public int ID { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public string Brief { get; set; }
        public int Year { get; set; }
        public string Thumb { get; set; }
        /// <summary>
        /// 0 = not rented
        /// 1 = rented
        /// </summary>
        public int IsRented { get; set; }
    }
}
