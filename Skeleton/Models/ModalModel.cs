using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skeleton.Models
{
    public class ModalModel
    {
        #region Parameters
        public string Title { get; set; }
        public string Title2 { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string AreaName { get; set; }
        public int ID { get; set; }
        public Dictionary<string, string> Params { get; set; }
        public ModalType ModalType { get; set; }
        #endregion

        #region Constructors
        public ModalModel()
        {
            Params = new Dictionary<string, string>();
        }
        #endregion
    }
}