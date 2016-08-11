using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace MyStationSchedule.Models
{


    public class Routes
    {
        public string route_id { get; set; }
        public string route_name { get; set; }
        public string route_hide { get; set; }
    }

    public class Mode
    {
        public string route_type { get; set; }
        public string mode_name { get; set; }
        public IEnumerable<Routes> route { get; set; }
    }

    public class Route
    {
        public IEnumerable<Mode> mode { get; set; }
    }

    public class Stop
    {
        public string stop_order { get; set; }
        public string stop_id { get; set; }
        public string stop_name { get; set; }
        public string parent_station { get; set; }
        public string parent_station_name { get; set; }
        public string stop_lat { get; set; }
        public string stop_lon { get; set; }
    }

    public class Direction
    {
        public string direction_id { get; set; }
        public string direction_name { get; set; }
        public List<Stop> stop { get; set; }
    }

    public class StationStops
    {
        public List<Direction> direction { get; set; }
    }
}