﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyStationSchedule.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MyStationScheduleEntities : DbContext
    {
        public MyStationScheduleEntities()
            : base("name=MyStationScheduleEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Schedule_Times> Schedule_Times { get; set; }
    
        public virtual ObjectResult<usp_GetStopData_Result> usp_GetStopData(string stop)
        {
            var stopParameter = stop != null ?
                new ObjectParameter("Stop", stop) :
                new ObjectParameter("Stop", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_GetStopData_Result>("usp_GetStopData", stopParameter);
        }
    
        public virtual int usp_GetTrainStopData(string stop)
        {
            var stopParameter = stop != null ?
                new ObjectParameter("Stop", stop) :
                new ObjectParameter("Stop", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_GetTrainStopData", stopParameter);
        }
    }
}
