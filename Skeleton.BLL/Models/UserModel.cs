using Generic.Model;
using Skeleton.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.BLL.Models
{
    public class UserModel : GenericModel<User, UserModel>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsRenting { get; set; }
        public string Password { get; set; }

        public UserModel()
        {

        }
        public UserModel(User entity)
            : base(entity)
        {

        }
    }
}
