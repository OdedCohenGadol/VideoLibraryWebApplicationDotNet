using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.BLL.Models
{
    public class StatusModel
    {
      

        #region Parameters
        public bool Success { get; set; }
        public string Message { get; set; }

        #endregion

        #region Constructors

        public StatusModel()
        {
        }

        public StatusModel(bool status)
        {
            Success = status;
        }

        public StatusModel(bool p1, string p2)
        {
            // TODO: Complete member initialization
            Success = p1;
            Message = p2;
        }
        #endregion

    }

    public class StatusModel<T> : StatusModel
    {
        public T Data { get; set; }

        public StatusModel()
            : base()
        {
        }

        public StatusModel(bool status)
            : base()
        {
            Success = status;
        }
    }
}
