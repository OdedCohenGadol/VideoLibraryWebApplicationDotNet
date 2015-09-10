using Generic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.DAL.Repositories
{
    public class VideoRepository: GenericRepository<Video,int>
    {
        public VideoRepository(VideoEntities context, bool isAutoCommit)
            : base(context, isAutoCommit) 
        {
        }

        public IQueryable<SearchVideos_Result> SearchVideos(string Name,string Director,string Genre,int? Year)
        {
            var result = this.Exec<SearchVideos_Result>("SearchVideos", Name, Director, Genre, Year);
            return result.AsQueryable();
        }



        public Video GetUserMovie(int userId)
        {
            return this.DBSet.FirstOrDefault(v=>v.RenterID == userId);
        }
    }
}
