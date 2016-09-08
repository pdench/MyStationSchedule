using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Newtonsoft.Json;
using System.Net;
using MyStationSchedule.Models;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace MyStationSchedule.Controllers
{
    public class ScheduleController : Controller
    {
        protected string serviceUrlBase = ConfigurationManager.AppSettings["serviceUrlBase"];
        protected string apiKey = ConfigurationManager.AppSettings["api_key"];

        // GET: Schedule
        public ActionResult Index()
        {
            string url = serviceUrlBase + "routes?api_key=" + apiKey + "&format=json";
            List<Routes> routeList = new List<Routes>();
            WebClient client = new WebClient();
            string output = client.DownloadString(url);

            Route routes = JsonConvert.DeserializeObject<Route>(output);
            List<SelectListItem> routeListDD = new List<SelectListItem>();
            foreach (var item in routes.mode)
            {
                if (item.route_type == "2")
                {
                    foreach (var thisRoute in item.route)
                    {
                        //routeList.Add(new Routes() { route_id = thisRoute.route_id, route_name = thisRoute.route_name });
                        routeListDD.Add(new SelectListItem { Text = thisRoute.route_name, Value = thisRoute.route_id });
                        //System.Diagnostics.Debug.WriteLine("id: {0}, name: {1}", thisRoute.route_id, thisRoute.route_name);
                    }
                }
                
            }

            ViewData["routes"] = routeListDD;
            return View(routeList);
        }

        public JsonResult GetStops(string id, string inOrOut)
        {
            string url = serviceUrlBase + "stopsbyroute?api_key=" + apiKey + "&route=" + id + "&format=json";
            WebClient client = new WebClient();
            string output = client.DownloadString(url);

            List<SelectListItem> stopsList = new List<SelectListItem>();
            StationStops stops = JsonConvert.DeserializeObject<StationStops>(output);
            foreach (var item in stops.direction)
            {
                if (item.direction_id==inOrOut)
                {
                    foreach (var thisStop in item.stop)
                    {
                        System.Diagnostics.Debug.WriteLine(thisStop.parent_station_name);
                        stopsList.Add(new SelectListItem { Text = thisStop.stop_name, Value = thisStop.stop_id });
                    }
                }
            }
            return Json(new SelectList(stopsList, "Text", "Value"));
        }

        public JsonResult GetSchedules(string id)
        {
            string sql = "usp_GetStopData @Stop";
            List<Schedule> scheduledStops = new List<Schedule>();

            try
            {
                using (var context = new MyStationScheduleEntities())
                {
                    SqlParameter stop = new SqlParameter("@Stop", id);
                    var stops = context.Database.SqlQuery<usp_GetStopData_Result>(sql, stop).ToList();

                    foreach (usp_GetStopData_Result item in stops)
                    {
                        Schedule thisStop = new Schedule();
                        thisStop.ArrivalTime = GetTime(item.arrival_time);
                        thisStop.Direction = item.direction;
                        thisStop.Timing = item.Timing;
                        thisStop.TrainNumber = item.train_number;
                        //thisStop.Origination = item.origination_stop;
                        //thisStop.OriginationTime = item.origination_time;
                        scheduledStops.Add(thisStop);
                    }
                }
                return Json(scheduledStops, "Text");
            }
            catch (DataException ex)
            {
                return Json("");
            }
        }

        private string GetTime (string inputTime)
        {
            try
            {
                return DateTime.Parse(inputTime).ToString(@"hh\:mm\:ss tt");
            }
            catch (Exception ex)
            {
                return inputTime;
            }
        }

    }
}