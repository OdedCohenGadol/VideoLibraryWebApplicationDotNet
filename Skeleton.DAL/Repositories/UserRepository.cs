using Generic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.DAL.Repositories
{
    public class UserRepository: GenericRepository<User,int>
    {
        public UserRepository(VideoEntities context, bool isAutoCommit)
            : base(context, isAutoCommit) 
        {
        }

        

       
    }
}
