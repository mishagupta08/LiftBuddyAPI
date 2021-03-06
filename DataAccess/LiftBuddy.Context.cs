﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class LiftBuddyEntities : DbContext
    {
        public LiftBuddyEntities()
            : base("name=LiftBuddyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblFollower> tblFollowers { get; set; }
        public virtual DbSet<tblLevelLedger> tblLevelLedgers { get; set; }
        public virtual DbSet<tblNotification> tblNotifications { get; set; }
        public virtual DbSet<tblNotificationMaster> tblNotificationMasters { get; set; }
        public virtual DbSet<tblOtpTransaction> tblOtpTransactions { get; set; }
        public virtual DbSet<tblPointLedger> tblPointLedgers { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<tbleOfferRide> tbleOfferRides { get; set; }
        public virtual DbSet<tblRequestRide> tblRequestRides { get; set; }
        public virtual DbSet<tblVehicleType> tblVehicleTypes { get; set; }
        public virtual DbSet<tblDriveVehicleType> tblDriveVehicleTypes { get; set; }
    
        [DbFunction("LiftBuddyEntities", "Split")]
        public virtual IQueryable<Split_Result> Split(string line, string splitOn)
        {
            var lineParameter = line != null ?
                new ObjectParameter("Line", line) :
                new ObjectParameter("Line", typeof(string));
    
            var splitOnParameter = splitOn != null ?
                new ObjectParameter("SplitOn", splitOn) :
                new ObjectParameter("SplitOn", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<Split_Result>("[LiftBuddyEntities].[Split](@Line, @SplitOn)", lineParameter, splitOnParameter);
        }
    
        public virtual int AllDateWiseUserStatus(Nullable<int> userID, Nullable<System.DateTime> strt_date, Nullable<System.DateTime> end_date)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(int));
    
            var strt_dateParameter = strt_date.HasValue ?
                new ObjectParameter("strt_date", strt_date) :
                new ObjectParameter("strt_date", typeof(System.DateTime));
    
            var end_dateParameter = end_date.HasValue ?
                new ObjectParameter("end_date", end_date) :
                new ObjectParameter("end_date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AllDateWiseUserStatus", userIDParameter, strt_dateParameter, end_dateParameter);
        }
    
        public virtual int DateWiseUserStatus(Nullable<int> userID, Nullable<System.DateTime> strt_date, Nullable<System.DateTime> end_date)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(int));
    
            var strt_dateParameter = strt_date.HasValue ?
                new ObjectParameter("strt_date", strt_date) :
                new ObjectParameter("strt_date", typeof(System.DateTime));
    
            var end_dateParameter = end_date.HasValue ?
                new ObjectParameter("end_date", end_date) :
                new ObjectParameter("end_date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DateWiseUserStatus", userIDParameter, strt_dateParameter, end_dateParameter);
        }
    
        public virtual ObjectResult<spGetVehicleTypeList_Result> spGetVehicleTypeList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetVehicleTypeList_Result>("spGetVehicleTypeList");
        }
    
        public virtual int spAddRequestRideDetail(string recordXml)
        {
            var recordXmlParameter = recordXml != null ?
                new ObjectParameter("recordXml", recordXml) :
                new ObjectParameter("recordXml", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spAddRequestRideDetail", recordXmlParameter);
        }
    
        public virtual int sp_AssignCategory(string categoryIDs, Nullable<int> registerID)
        {
            var categoryIDsParameter = categoryIDs != null ?
                new ObjectParameter("CategoryIDs", categoryIDs) :
                new ObjectParameter("CategoryIDs", typeof(string));
    
            var registerIDParameter = registerID.HasValue ?
                new ObjectParameter("RegisterID", registerID) :
                new ObjectParameter("RegisterID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_AssignCategory", categoryIDsParameter, registerIDParameter);
        }
    
        public virtual int spDeleteReduestRideDetail(Nullable<int> rideId)
        {
            var rideIdParameter = rideId.HasValue ?
                new ObjectParameter("rideId", rideId) :
                new ObjectParameter("rideId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spDeleteReduestRideDetail", rideIdParameter);
        }
    
        public virtual int spEditRequestRideDetail(string recordXml)
        {
            var recordXmlParameter = recordXml != null ?
                new ObjectParameter("recordXml", recordXml) :
                new ObjectParameter("recordXml", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spEditRequestRideDetail", recordXmlParameter);
        }
    
        public virtual ObjectResult<tblRequestRide> spGetReduestRideDetailById(Nullable<int> id, string request)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var requestParameter = request != null ?
                new ObjectParameter("request", request) :
                new ObjectParameter("request", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<tblRequestRide>("spGetReduestRideDetailById", idParameter, requestParameter);
        }
    
        public virtual ObjectResult<tblRequestRide> spGetReduestRideDetailById(Nullable<int> id, string request, MergeOption mergeOption)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var requestParameter = request != null ?
                new ObjectParameter("request", request) :
                new ObjectParameter("request", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<tblRequestRide>("spGetReduestRideDetailById", mergeOption, idParameter, requestParameter);
        }
    
        public virtual ObjectResult<RequestListContainer> spGetReduestRideDetailListById(Nullable<int> id, string request)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var requestParameter = request != null ?
                new ObjectParameter("request", request) :
                new ObjectParameter("request", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RequestListContainer>("spGetReduestRideDetailListById", idParameter, requestParameter);
        }
    
        public virtual ObjectResult<tblDriveVehicleType> spAddDriveVehicleTypeDetail(string recordXml)
        {
            var recordXmlParameter = recordXml != null ?
                new ObjectParameter("recordXml", recordXml) :
                new ObjectParameter("recordXml", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<tblDriveVehicleType>("spAddDriveVehicleTypeDetail", recordXmlParameter);
        }
    
        public virtual ObjectResult<tblDriveVehicleType> spAddDriveVehicleTypeDetail(string recordXml, MergeOption mergeOption)
        {
            var recordXmlParameter = recordXml != null ?
                new ObjectParameter("recordXml", recordXml) :
                new ObjectParameter("recordXml", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<tblDriveVehicleType>("spAddDriveVehicleTypeDetail", mergeOption, recordXmlParameter);
        }
    
        public virtual int spAddOfferRideDetail(string recordXml)
        {
            var recordXmlParameter = recordXml != null ?
                new ObjectParameter("recordXml", recordXml) :
                new ObjectParameter("recordXml", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spAddOfferRideDetail", recordXmlParameter);
        }
    
        public virtual int spDeleteOfferRideDetail(Nullable<int> rideId)
        {
            var rideIdParameter = rideId.HasValue ?
                new ObjectParameter("rideId", rideId) :
                new ObjectParameter("rideId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spDeleteOfferRideDetail", rideIdParameter);
        }
    
        public virtual int spDeletetblDriveVehicleTypeDetail(Nullable<int> rideId)
        {
            var rideIdParameter = rideId.HasValue ?
                new ObjectParameter("rideId", rideId) :
                new ObjectParameter("rideId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spDeletetblDriveVehicleTypeDetail", rideIdParameter);
        }
    
        public virtual int spEditDriveVehicleType(string recordXml)
        {
            var recordXmlParameter = recordXml != null ?
                new ObjectParameter("recordXml", recordXml) :
                new ObjectParameter("recordXml", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spEditDriveVehicleType", recordXmlParameter);
        }
    
        public virtual int spEditOfferRideDetail(string recordXml)
        {
            var recordXmlParameter = recordXml != null ?
                new ObjectParameter("recordXml", recordXml) :
                new ObjectParameter("recordXml", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spEditOfferRideDetail", recordXmlParameter);
        }
    
        public virtual ObjectResult<tbleOfferRide> spGetOfferDetailById(Nullable<int> id, string request)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var requestParameter = request != null ?
                new ObjectParameter("request", request) :
                new ObjectParameter("request", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<tbleOfferRide>("spGetOfferDetailById", idParameter, requestParameter);
        }
    
        public virtual ObjectResult<tbleOfferRide> spGetOfferDetailById(Nullable<int> id, string request, MergeOption mergeOption)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var requestParameter = request != null ?
                new ObjectParameter("request", request) :
                new ObjectParameter("request", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<tbleOfferRide>("spGetOfferDetailById", mergeOption, idParameter, requestParameter);
        }
    
        public virtual ObjectResult<spGetOfferRideDetailListById_Result> spGetOfferRideDetailListById(Nullable<int> id, string request)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var requestParameter = request != null ?
                new ObjectParameter("request", request) :
                new ObjectParameter("request", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetOfferRideDetailListById_Result>("spGetOfferRideDetailListById", idParameter, requestParameter);
        }
    
        public virtual int spGetDriveVehicleTypeById(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spGetDriveVehicleTypeById", idParameter);
        }
    }
}
