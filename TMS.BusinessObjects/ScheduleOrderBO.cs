using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.BusinessObjects
{
   public class ScheduleOrderBO
    {
        public Guid RouteKey { get; set; }
        public Guid OrderDetailKey { get; set; }
        public Guid OrderKey { get; set; }
        public short LegNo { get; set; }
        public short LegType { get;set; }
        public Guid SourceAddressKey { get; set; }
        public Guid DestinationAddressKey { get; set; }
        public double DistanceInMiles { get; set; }
        public double TravelTime { get; set; }
        public short Status { get; set; }
        public Guid DriverKey { get; set; }
        public TimeSpan ScheduleArrival { get; set; }
        public TimeSpan ScheduleDeparture { get; set; }
        public int Odometer { get; set; }
        public TimeSpan ActualArrival { get; set; }
        public TimeSpan ActualDeparture { get; set; }
        public short OdometerAtDestination { get; set; }
        /* legtype smallint,
  sourceaddrkey uuid,
  destinationaddrkey uuid,
  estimateddistanceinmiles numeric(18,2),
  estimatedtraveltime numeric(5,2),
  status smallint,
  driverkey uuid,
  scheduledarrival timestamp without time zone,
  scheduleddeparture timestamp without time zone,
  odometeratsource smallint,
  actualarrival timestamp without time zone,
  actualdeparture timestamp without time zone,
  odometeratdestination smallint,*/
    }
}
