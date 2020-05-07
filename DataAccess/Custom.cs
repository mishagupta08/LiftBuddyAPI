using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public partial class tblUserWeeklyStatu
    {
        public string FoodHabbitDesc { get; set; }
        public string MealDesc { get; set; }
    
        public string fileBytes { get; set; }
        public string LastUpdated { get; set; }
    }

    public class ReportFilters
    {
        public int UserId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
    }

    public class FriendStats
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }        
        public string ProfilePic { get; set; }
        public decimal Steps { get; set; }
    }
    public class MobileNumber
    {
        public List<string> NumberList { get; set; }
    }

    public class Follower
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string ProfilePic { get; set; }
        public string MobileNo { get; set; }
        public string IsFollowing { get; set; }
    }

    public class UserPoints
    {
        public int UserId { get; set; }
        public decimal Points { get; set; }       
    }

    public class Report
    {
        public int UserId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }       
        public decimal TotalSteps { get; set; }
        public decimal TotalDistance { get; set; }
        public decimal TotalCalories { get; set; }
        public List<StepRecord> StepRecords { get; set; }
        public List<DistanceRecord> DistanceRecords { get; set; }
        public List<CaloriesRecord> CaloriesRecords { get; set; }
    }

    public class PointReport
    {
        public int UserId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public decimal TotalSteps { get; set; }
        public decimal TotalPoints { get; set; }
        public decimal TotalEarnedPoints { get; set; }
        public decimal TotalReedeemPoints { get; set; }
        public List<PointRecord> PointRecords { get; set; }      
    }

    public class Profile
    {
        public int UserId { get; set; }        
        public string Name { get; set; }
        public string UserName { get; set; }
        public string ProfilePic { get; set; }
        public int followers { get; set; }
        public int following { get; set; }
        public decimal Steps { get; set; }
        public decimal Points { get; set; }
        public decimal AvgSteps { get; set; }
        public decimal AvgPoints { get; set; }
        public int Level { get; set; }
        public List<StepRecord> LastSevenDaysHistory { get; set; }
    }     

    public class StepRecord
    {
        public string Date { get; set; }       
        public decimal Steps { get; set; }       
    }

    public class DistanceRecord
    {
        public string Date { get; set; }
        public decimal Distance { get; set; }
    }

    public class CaloriesRecord
    {
        public string Date { get; set; }
        public decimal Calories { get; set; }
    }

    public class PointRecord
    {
        public string Date { get; set; }
        public decimal Steps { get; set; }
        public decimal Points { get; set; }
    }

    public class HourlyRecord
    {
        public string Hour { get; set; }
        public decimal Steps { get; set; }
    }

    public class UserFriend
    {
        public int UserId { get; set; }
        public int ProfileId { get; set; }
    }
    public class LeaderBoardStatus
    {
        public List<FriendStats> ThisWeek { get; set; }
        public List<FriendStats> LastWeek { get; set; }
    }


}
