using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameTimeStamp 
{
    public int year;
    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter,
    }

    public enum DayOfTheWeek
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
    }

    public Season season;
    public int day;
    public int hour;
    public int minute;

    //Constructor to set up the class
    public GameTimeStamp(int year, Season season, int day, int hour, int minute)
    {
        this.year = year;
        this.season = season;   
        this.day = day; 
        this.hour = hour;
        this.minute = minute;
    }

    // Increments the time by 1 minute
    public void UpdateClock()
    {
        minute++;

        //60 minutes in 1 hour
        if (minute >= 60)
        {
            minute = 0;
            hour++;
        }

        // 24 hours in 1 day
        if (hour >= 24)
        {
           //Reset hours
            hour = 0;

            day++;
        }

        if(day >= 30)
        {
            //Reset days
            day = 1;

            //If at the final season, reset and change to spring
            if(season == Season.Winter)
            {
                season = Season.Spring;
                //Start of a new year
                year++;
            }
            else
            {
                season++;
            }
        }
    }

    public DayOfTheWeek GetDayOfTheWeek()
    {
        int daysPassed = YearsToDays(year) + SeasonToDays(season) + day;

        //Remainder after dividing daysPaseed by 7
        int dayIndex = daysPassed % 7;

        return (DayOfTheWeek)dayIndex;
    }

    //Convert hours to minutes
    public static int HoursToMinutes(int hour)
    {
        //60 minutes = 1 hour
        return hour *60;
    }

    //Convert Days to hours
    public static int DaysToHours(int days)
    {
        //24 Hours in a day 
        return days * 24;
    }

    //Convert Season to days 
    public static int SeasonToDays(Season season)
    {
        int seasonIndex = (int)season;
        return seasonIndex * 30;
    }

    public static int YearsToDays(int years)
    {
        return years * 4 * 30;
    }


}
