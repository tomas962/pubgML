//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using Microsoft.ML.Data;

namespace PUBGStatisticsCoreML.Model.DataModels
{
    public class ModelInput
    {
        [ColumnName("PlayerName"), LoadColumn(0)]
        public string PlayerName { get; set; }


        [ColumnName("TrackerID"), LoadColumn(1)]
        public float TrackerID { get; set; }


        [ColumnName("KillDeathRatio"), LoadColumn(2)]
        public string KillDeathRatio { get; set; }


        [ColumnName("WinRatio"), LoadColumn(3)]
        public string WinRatio { get; set; }


        [ColumnName("TimeSurvived"), LoadColumn(4)]
        public string TimeSurvived { get; set; }


        [ColumnName("RoundsPlayed"), LoadColumn(5)]
        public float RoundsPlayed { get; set; }


        [ColumnName("Wins"), LoadColumn(6)]
        public float Wins { get; set; }


        [ColumnName("WinTop10Ratio"), LoadColumn(7)]
        public string WinTop10Ratio { get; set; }


        [ColumnName("Top10s"), LoadColumn(8)]
        public float Top10s { get; set; }


        [ColumnName("Top10Ratio"), LoadColumn(9)]
        public string Top10Ratio { get; set; }


        [ColumnName("Losses"), LoadColumn(10)]
        public float Losses { get; set; }


        [ColumnName("Rating"), LoadColumn(11)]
        public string Rating { get; set; }


        [ColumnName("BestRating"), LoadColumn(12)]
        public string BestRating { get; set; }


        [ColumnName("DamagePg"), LoadColumn(13)]
        public string DamagePg { get; set; }


        [ColumnName("HeadshotKillsPg"), LoadColumn(14)]
        public string HeadshotKillsPg { get; set; }


        [ColumnName("HealsPg"), LoadColumn(15)]
        public string HealsPg { get; set; }


        [ColumnName("KillsPg"), LoadColumn(16)]
        public string KillsPg { get; set; }


        [ColumnName("MoveDistancePg"), LoadColumn(17)]
        public string MoveDistancePg { get; set; }


        [ColumnName("RoadKillsPg"), LoadColumn(18)]
        public string RoadKillsPg { get; set; }


        [ColumnName("TeamKillsPg"), LoadColumn(19)]
        public string TeamKillsPg { get; set; }


        [ColumnName("TimeSurvivedPg"), LoadColumn(20)]
        public string TimeSurvivedPg { get; set; }


        [ColumnName("Top10sPg"), LoadColumn(21)]
        public string Top10sPg { get; set; }


        [ColumnName("Kills"), LoadColumn(22)]
        public float Kills { get; set; }


        [ColumnName("Assists"), LoadColumn(23)]
        public float Assists { get; set; }


        [ColumnName("Suicides"), LoadColumn(24)]
        public float Suicides { get; set; }


        [ColumnName("TeamKills"), LoadColumn(25)]
        public float TeamKills { get; set; }


        [ColumnName("HeadshotKills"), LoadColumn(26)]
        public float HeadshotKills { get; set; }


        [ColumnName("HeadshotKillRatio"), LoadColumn(27)]
        public string HeadshotKillRatio { get; set; }


        [ColumnName("VehicleDestroys"), LoadColumn(28)]
        public float VehicleDestroys { get; set; }


        [ColumnName("RoadKills"), LoadColumn(29)]
        public float RoadKills { get; set; }


        [ColumnName("DailyKills"), LoadColumn(30)]
        public float DailyKills { get; set; }


        [ColumnName("WeeklyKills"), LoadColumn(31)]
        public float WeeklyKills { get; set; }


        [ColumnName("RoundMostKills"), LoadColumn(32)]
        public float RoundMostKills { get; set; }


        [ColumnName("MaxKillStreaks"), LoadColumn(33)]
        public float MaxKillStreaks { get; set; }


        [ColumnName("Days"), LoadColumn(34)]
        public float Days { get; set; }


        [ColumnName("LongestTimeSurvived"), LoadColumn(35)]
        public string LongestTimeSurvived { get; set; }


        [ColumnName("MostSurvivalTime"), LoadColumn(36)]
        public string MostSurvivalTime { get; set; }


        [ColumnName("AvgSurvivalTime"), LoadColumn(37)]
        public string AvgSurvivalTime { get; set; }


        [ColumnName("WinPoints"), LoadColumn(38)]
        public float WinPoints { get; set; }


        [ColumnName("WalkDistance"), LoadColumn(39)]
        public string WalkDistance { get; set; }


        [ColumnName("RideDistance"), LoadColumn(40)]
        public string RideDistance { get; set; }


        [ColumnName("MoveDistance"), LoadColumn(41)]
        public string MoveDistance { get; set; }


        [ColumnName("AvgWalkDistance"), LoadColumn(42)]
        public string AvgWalkDistance { get; set; }


        [ColumnName("AvgRideDistance"), LoadColumn(43)]
        public string AvgRideDistance { get; set; }


        [ColumnName("LongestKill"), LoadColumn(44)]
        public string LongestKill { get; set; }


        [ColumnName("Heals"), LoadColumn(45)]
        public float Heals { get; set; }


        [ColumnName("Boosts"), LoadColumn(46)]
        public float Boosts { get; set; }


        [ColumnName("DamageDealt"), LoadColumn(47)]
        public string DamageDealt { get; set; }


    }
}
