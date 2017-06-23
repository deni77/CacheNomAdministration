using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CacheNomAdministration.WebApi.Models.Totalizers
{
    public class ControlPointView
    {
        public string TotalizerTag { get; set; }

        public int ControlPointID { get; set; }

         public int FlowDirection { get; set; }

         public int ControlerCode { get; set; }

         public int PositionNumber { get; set; }

       public string ControlPointName { get; set; }

      public string EngineeringUnit { get; set; }

       public string ProductCode { get; set; }
    }
}