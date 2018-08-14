﻿using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using MyDLLs;

namespace NSCOperationalPlan
{
    public class OPGlobals
    {
        public static string dbProvider;
        public static string connString;

        public static DateTime FinancialYearStarts;
        public static int CurrentQuarter;
        public static string currentYear;
        public static int currentMonth;

        public static string prevoiusYear;
        public static int previousMonth;

        public static string nextYear;
        public static int nextMonth;

        public static string reportParth;

        //public static string currentuserID;
        //public static string currentuserFullName;
        //public static UserRights currentuserPrimission;

        public static clsUser CurrentUser;
        public static clsUser PreviousUser;

        public static int i = 0;

        public static bool CapitalWorksEnabled = false;

        public static Database db;
        public static bool DeliveryProgramEnabled = false;

        public static int GetPreviousMonth(int cmonth, string cyear)
        {
            int pmonth;

            if (cmonth == 1) 
            {
                pmonth = 12;

            } else { 
                pmonth = cmonth - 1; 
            }

            return pmonth;
        }
        public static string GetPreviousYear(int cmonth, string cyear)
        {

            string pyear;
            if (cmonth == 1) 
            {
                int y1, y2;
                y1 = int.Parse(cyear.Substring(0, 2));
                y2 = int.Parse(cyear.Substring(3, 2));

                pyear = (y1 - 1).ToString() + "/" + (y2 - 1).ToString();
            }
            else
            { 
                pyear = cyear; 
            }

            return pyear;
        }
        public static string GetNextYear(string cyear)
        {
            string pyear;
            int y1, y2;
            y1 = int.Parse(cyear.Substring(0, 2));
            y2 = int.Parse(cyear.Substring(3, 2));

            pyear = (y1 + 1).ToString() + "/" + (y2 + 1).ToString();
            return pyear;
        }

        public static int GetQuarter(int cmonth)
        {
            if (cmonth >= 7 && cmonth <=9) { return 1; }
            if (cmonth >= 10 && cmonth <= 12) { return 2; }
            if (cmonth >= 1 && cmonth <= 3) { return 3; }
            if (cmonth >= 4 && cmonth <= 6) { return 4; }
            return 0;
        }

        #region ---Strategy Measure Months and Years ---
        public static int GetStrategyMeasureMonth()
        {
            return GetStrategyMeasureMonth(currentMonth);
        }
        public static int GetStrategyMeasureMonth(int cmonth)
        {
            if (cmonth >= 1 && cmonth <= 6) { return 6; } else return 12;
        }
        public static int GetStrategyMeasureRank(string cyear, int cmonth)
        {
            switch(string.Concat(cyear.Trim(), cmonth.ToString().Trim()))
            {
                case "17/1812":
                    return 1;
                    break;
                case "17/186":
                    return 2;
                    break;
                case "18/1912":
                    return 3;
                    break;
                case "18/196":
                    return 4;
                    break;
                case "19/2012":
                    return 5;
                    break;
                case "19/206":
                    return 6;
                    break;
                case "20/2112":
                    return 7;
                    break;
                case "20/216":
                    return 8;
                    break;
                default:
                    return 0;
            }
        }
        public static int GetStrategyMeasurePrevouosMonth()
        {
            return GetStrategyMeasurePrevouosMonth(OPGlobals.GetStrategyMeasureMonth());
        }
        public static int GetStrategyMeasurePrevouosMonth(int cmonth)
        {
            if (cmonth == 6) { return 12; } else return 6;
        }
        public static string GetStrategyMeasurePrevouosYear(string cyear, int cMonth)
        {
            string pyear = cyear;
            int y1, y2;
            y1 = int.Parse(cyear.Substring(0, 2));
            y2 = int.Parse(cyear.Substring(3, 2));
            if(cMonth==6) { pyear = (y1 - 1).ToString() + "/" + (y2 - 1).ToString(); }
                
            return pyear;
        }
        #endregion
        public static string ChangeToSentenceCase(string str)
        {
            // start by converting entire string to lower case
            var lowerCase = str.ToLower();
            // matches the first sentence of a string, as well as subsequent sentences
            var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);
            // MatchEvaluator delegate defines replacement of setence starts to uppercase
            var result = r.Replace(lowerCase, s => s.Value.ToUpper());

            // result is: "This is a group. Of uncapitalized. Letters."
            return result.ToString();
        }
    }

}
