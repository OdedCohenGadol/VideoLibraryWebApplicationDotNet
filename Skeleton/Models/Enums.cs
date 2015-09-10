using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Skeleton.Models
{
    
        public enum ModalType
        {
            [Description("modal-full")]
            Full = 0,
            [Description("modal-lg")]
            Large = 1,
            [Description("modal-sm")]
            Small = 2,
            [Description("")]
            Other = 3
        }
    
}