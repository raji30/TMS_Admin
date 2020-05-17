using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
    public class ContainerBO
    {
        public string ContainerId { get; set; }
        public string ContainerNo { get; set; }
        public short ContainerSize { get; set; }
        public string ContainerSizeDesc { get; set; }
        public string Chassis { get; set; }
        public string SealNo { get; set; }
        public string Weight { get; set; }
    }
}
