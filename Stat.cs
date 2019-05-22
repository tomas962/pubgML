using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUBGStatistics
{
    class Stat
    {
        public string PlayerName { get; set; }
        public int TrackerID { get; set; }
        public double KillDeathRatio { get; set; }
        public double WinRatio { get; set; }
        public double TimeSurvived { get; set; }
        public int RoundsPlayed { get; set; }
        public int Wins { get; set; }
        public double WinTop10Ratio { get; set; }
        public int Top10s { get; set; }
        public double Top10Ratio { get; set; }
        public int Losses { get; set; }
        public double Rating { get; set; }
        public double BestRating { get; set; }
        public double DamagePg { get; set; }
        public double HeadshotKillsPg { get; set; }
        public double HealsPg { get; set; }
        public double KillsPg { get; set; }
        public double MoveDistancePg { get; set; }
        public double RoadKillsPg { get; set; }
        public double TeamKillsPg { get; set; }
        public double TimeSurvivedPg { get; set; }
        public double Top10sPg { get; set; }
        public int Kills { get; set; }
        public int Assists { get; set; }
        public int Suicides { get; set; }
        public int TeamKills { get; set; }
        public int HeadshotKills { get; set; }
        public double HeadshotKillRatio { get; set; }
        public int VehicleDestroys { get; set; }
        public int RoadKills { get; set; }
        public int DailyKills { get; set; }
        public int WeeklyKills { get; set; }
        public int RoundMostKills { get; set; }
        public int MaxKillStreaks { get; set; }
        public int Days { get; set; }
        public double LongestTimeSurvived { get; set; }
        public double MostSurvivalTime { get; set; }
        public double AvgSurvivalTime { get; set; }
        public int WinPoints { get; set; }
        public double WalkDistance { get; set; }
        public double RideDistance { get; set; }
        public double MoveDistance { get; set; }
        public double AvgWalkDistance { get; set; }
        public double AvgRideDistance { get; set; }
        public double LongestKill { get; set; }
        public int Heals { get; set; }
        public int Boosts { get; set; }
        public double DamageDealt { get; set; }

        public Stat()
        {
        }
        public Stat(string PlayerName, int TrackerID, double KillDeathRatio, double WinRatio, double TimeSurvived, int RoundsPlayed, int Wins, 
         double WinTop10Ratio, int Top10s, double Top10Ratio, int Losses,
         double Rating, double BestRating, double DamagePg, double HeadshotKillsPg, double HealsPg, double KillsPg,
         double MoveDistancePg, double RoadKillsPg, double TeamKillsPg, double TimeSurvivedPg, double Top10sPg,
         int Kills, int Assists, int Suicides, int TeamKills, int HeadshotKills,
         double HeadshotKillRatio, int VehicleDestroys, int RoadKills, int DailyKills, int WeeklyKills,
         int RoundMostKills, int MaxKillStreaks, int Days, double LongestTimeSurvived, double MostSurvivalTime,
         double AvgSurvivalTime, int WinPoints, double WalkDistance, double RideDistance, double MoveDistance,
         double AvgWalkDistance, double AvgRideDistance, double LongestKill, int Heals, int Boosts, double DamageDealt)
        {
            this.PlayerName = PlayerName;
            this.TrackerID = TrackerID;
            this.KillDeathRatio = KillDeathRatio;
            this.WinRatio = WinRatio;
            this.TimeSurvived = TimeSurvived;
            this.RoundsPlayed = RoundsPlayed;
            this.Wins = Wins;
            this.WinTop10Ratio = WinTop10Ratio;
            this.Top10s = Top10s;
            this.Top10Ratio = Top10Ratio;
            this.Losses = Losses;
            this.Rating = Rating;
            this.BestRating = BestRating;
            this.DamagePg = DamagePg;
            this.HeadshotKillsPg = HeadshotKillsPg;
            this.HealsPg = HealsPg;
            this.KillsPg = KillsPg;
            this.MoveDistancePg = MoveDistancePg;
            this.RoadKillsPg = RoadKillsPg;
            this.TeamKillsPg = TeamKillsPg;
            this.TimeSurvivedPg = TimeSurvivedPg;
            this.Top10sPg = Top10sPg;
            this.Kills = Kills;
            this.Assists = Assists;
            this.Suicides = Suicides;
            this.TeamKills = TeamKills;
            this.HeadshotKills = HeadshotKills;
            this.HeadshotKillRatio = HeadshotKillRatio;
            this.VehicleDestroys = VehicleDestroys;
            this.RoadKills = RoadKills;
            this.DailyKills = DailyKills;
            this.WeeklyKills = WeeklyKills;
            this.RoundMostKills = RoundMostKills;
            this.MaxKillStreaks = MaxKillStreaks;
            this.Days = Days;
            this.LongestTimeSurvived = LongestTimeSurvived;
            this.MostSurvivalTime = MostSurvivalTime;
            this.AvgSurvivalTime = AvgSurvivalTime;
            this.WinPoints = WinPoints;
            this.WalkDistance = WalkDistance;
            this.RideDistance = RideDistance;
            this.MoveDistance = MoveDistance;
            this.AvgWalkDistance = AvgWalkDistance;
            this.AvgRideDistance = AvgRideDistance;
            this.LongestKill = LongestKill;
            this.Heals = Heals;
            this.Boosts = Boosts;
            this.DamageDealt = DamageDealt;
        }
        public override string ToString()
        {
            return String.Format("{0,-15}|{1,-10}|{2,-10}|{3,-10}|{4,-10}|{5,-10}|{6,-10}|{7,-10}|{8,-10}|{9,-10}" +
                "|{10,-10}|{11,-10}|{12,-10}|{13,-10}|{14,-10}|{15,-10}|{16,-10}|{17,-10}|{18,-10}|{19,-10}" +
                "|{20,-10}|{21,-10}|{22,-10}|{23,-10}|{24,-10}|{25,-10}|{26,-10}|{27,-10}|{28,-10}|{29,-10}" +
                "|{30,-10}|{31,-10}|{32,-10}|{33,-10}|{34,-10}|{35,-10}|{36,-10}|{37,-10}|{38,-10}|{39,-10}" +
                "|{40,-10}|{41,-10}|{42,-10}|{43,-10}|{44,-10}|{45,-10}|{46,-10}|{47,-10}|", 
                PlayerName, TrackerID, KillDeathRatio, WinRatio, TimeSurvived, RoundsPlayed, Wins,
               WinTop10Ratio, Top10s, Top10Ratio, Losses,
               Rating, BestRating, DamagePg, HeadshotKillsPg, HealsPg, KillsPg,
               MoveDistancePg, RoadKillsPg, TeamKillsPg, TimeSurvivedPg, Top10sPg,
               Kills, Assists, Suicides, TeamKills, HeadshotKills,
               HeadshotKillRatio, VehicleDestroys, RoadKills, DailyKills, WeeklyKills,
               RoundMostKills, MaxKillStreaks, Days, LongestTimeSurvived, MostSurvivalTime,
               AvgSurvivalTime, WinPoints, WalkDistance, RideDistance, MoveDistance,
               AvgWalkDistance, AvgRideDistance, LongestKill, Heals, Boosts, DamageDealt
                ); ;
        }
    }

}
