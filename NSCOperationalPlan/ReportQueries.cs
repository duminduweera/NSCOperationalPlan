using MyDLLs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace NSCOperationalPlan
{
    public static class ReportQueries
    {

        //public static string QMonthlyProgress(string opYear, int opMonth, Boolean allList)
        //{
        //    string strsql = "";

        //    if ((OPGlobals.CurrentUser.Permission == UserRights.Administrator) || (allList == true))
        //    {

        //    }
        //    else if (OPGlobals.CurrentUser.Permission == UserRights.Director)
        //    {
        //        //strsql = " WHERE director_id='" + OPGlobals.currentuserID + "'";
        //        strsql = " WHERE director_id='" + OPGlobals.CurrentUser.UserID + "'";
        //    }
        //    else if (OPGlobals.CurrentUser.Permission == UserRights.Manager)
        //    {
        //        //strsql = " WHERE manager_id='" + OPGlobals.currentuserID + "'";
        //        strsql = " WHERE manager_id='" + OPGlobals.CurrentUser.UserID + "'";
        //    }
        //    else
        //    {
        //        return strsql;
        //    }

        //    string opmonth = Enum.GetName(typeof(Months), opMonth);

        //    // concat(A.action_id, ' ', A.action_description) as action_description

        //    strsql = "Select A.theme_id, A.theme_short, A.theme_color, A.strategy_objective_id,"
        //        + " concat(A.strategy_objective_id,' ', A.strategy_objective) as strategy_objective,"
        //        + " A.strategy_objective_rank, A.strategy_id, A.strategy_rank, concat(A.strategy_id, ' ', A.strategy) as strategy,"
        //        + " A.action_id, A.action_rank, A.action_description as action_description, A.action_partner_org,"
        //        + " A.manager_id, A.manager_name, A.manager_description, A.sub_department, A.director_id, A.director_name,"
        //        + " A.director_description, A.department, A.delivery_program_id, A.delivery_program_year, A.delivery_program_TargetDate,"
        //        + " A.service_plan_id,  A.service_plan,"
        //        + " B.progress_id, B.status_id, B.progress_description, B.progress_pecentage, B.status_short, B.status_color,"
        //        + " B.progress_year, B.progress_month, '" + opmonth + "' as delivery_program_month from (Select * From view_action_by_year"
        //        + " Where view_action_by_year.delivery_program_year = '" + opYear + "') A Left Join"
        //        + " (Select progress.id As progress_id, progress.progress_description, progress.progress_pecentage, status.id As status_id,"
        //        + " status.status_short, status.status_color, progress.action_id, progress.progress_year, progress.progress_month"
        //        + " From progress Inner Join status On progress.status_id = status.id Where"
        //        + " progress.progress_year = '" + opYear + "' And progress.progress_month = " + opMonth + ") B On A.action_id = B.action_id"
        //        + strsql
        //        + " Order by A.theme_id, A.strategy_objective_rank,A.strategy_rank, A.action_rank;";
        //    return strsql;

        //}

        //public static string QMonthlyProgress(string opYear, int opMonth, Boolean allList, string opDirector, string opManager)
        //{
        //    string strsql = "";
        //    if(allList != true)
        //    {
        //        if(opDirector != "-0-" && opManager != "-0-")
        //        {
        //            strsql = " WHERE director_id='" + opDirector + "' AND manager_id='" + opManager + "'";
        //        } else if (opDirector != "-0-")
        //        {
        //            strsql = " WHERE director_id='" + opDirector + "'";
        //        }
        //    }

        //    string opmonth = Enum.GetName(typeof(Months), opMonth);

        //    strsql = "Select A.theme_id, A.theme_short, A.theme_color, A.strategy_objective_id,"
        //        + " concat(A.strategy_objective_id,' ', A.strategy_objective) as strategy_objective,"
        //        + " A.strategy_objective_rank, A.strategy_id, A.strategy_rank, concat(A.strategy_id, ' ', A.strategy) as strategy,"
        //        + " A.action_id, A.action_rank, A.action_description as action_description, A.action_partner_org,"
        //        + " A.manager_id, A.manager_name, A.manager_description, A.sub_department, A.director_id, A.director_name,"
        //        + " A.director_description, A.department, A.delivery_program_id, A.delivery_program_year, A.delivery_program_TargetDate,"
        //        + " A.service_plan_id, A.service_plan,"
        //        + " B.progress_id, B.status_id, B.progress_description, B.progress_pecentage, B.status_short, B.status_color,"
        //        + " B.progress_year, B.progress_month, '" + opmonth + "' as delivery_program_month"
        //        + " from (Select * From view_action_by_year Where view_action_by_year.delivery_program_year = '" + opYear + "') A Left Join"
        //        + " (Select progress.id As progress_id, progress.progress_description, progress.progress_pecentage, status.id As status_id,"
        //        + " status.status_short, status.status_color, progress.action_id, progress.progress_year, progress.progress_month"
        //        + " From progress Inner Join status On progress.status_id = status.id Where"
        //        + " progress.progress_year = '" + opYear + "' And progress.progress_month = " + opMonth + ") B On A.action_id = B.action_id"
        //        + strsql
        //        + " Order by A.theme_id, A.strategy_objective_rank,A.strategy_rank, A.action_rank;";
        //    return strsql;

        //}

        public static string QActionCompletedByTheme(string opYear)
        {
            return "SELECT DISTINCT theme_id, theme_short,theme_color, theme_color,action_by_theme,"
                + " null as status_id, null as status_short, null as status_color, null as completed_action, null as progress_month"
                + " FROM view_dashboard_top WHERE delivery_program_year = '" + opYear + "' ORDER BY theme_id";
        }
        public static string QActionCompletedByTheme(string opYear, int opMonth)
        {
            return "SELECT * FROM view_dashboard_top WHERE delivery_program_year = '" + opYear + "'"
                + " AND progress_month=" + opMonth + " ORDER BY theme_id, status_id";
        }

        public static string QActionCompletedByStatus(string opYear, int opMonth)
        {
            return "Select status.id, status.status_short, status.status_color, A.action_completed, A.progress_year, A.progress_month"
                + " From status Left Join"
                + " (Select progress.progress_year, progress.progress_month, Count(progress.id) As action_completed, progress.status_id"
                + " From progress Where progress_year = '" + opYear + "' and progress_month = " + opMonth
                + " Group By progress.progress_year, progress.progress_month, progress.status_id) A On status.id = A.status_id";
        }

        public static string QPrintStrategyDashboard(string opYear, int opMonth)
        {
            return "Select B.status_id, B.status_short, B.status_color, B.completed_actions_by_status_by_strategy," + opMonth + " as progress_month, A.strategy_objective_id,"
                + " Concat(A.strategy_objective_id, ' - ', A.strategy_objective) As strategy_objective,"
                + " A.strategy_id, Concat(A.strategy_id, ' - ', A.strategy) As strategy, A.theme_id, A.theme_short, A.theme_color, A.number_of_actions, A.delivery_program_year"
                + " From (Select view_action_count_by_strategy.* From view_action_count_by_strategy"
                + " Where view_action_count_by_strategy.delivery_program_year = '" + opYear + "' Order By"
                + " view_action_count_by_strategy.theme_id, view_action_count_by_strategy.strategy_objective_id, view_action_count_by_strategy.strategy_id) A Left Join"
                + " (Select view_completed_action_by_strategy_status.* From view_completed_action_by_strategy_status Where"
                + " view_completed_action_by_strategy_status.progress_year = '" + opYear + "' And view_completed_action_by_strategy_status.progress_month = " + opMonth
                + " Order By view_completed_action_by_strategy_status.status_id) B On A.delivery_program_year = B.progress_year And A.strategy_id = B.strategy_id;";
        }

        //public static string QMonthlyKPIProgress(string opYear, int opMonth)
        //{
        //    string strsql = "";
        //    string opmonth = Enum.GetName(typeof(Months), opMonth);

        //    strsql = "Select A.kpi_id, A.kpm_id, A.kpm_description, A.manager_id, A.manager_name, A.manager_description, A.director_description,"
        //    + " A.director_name, A.director_id, A.department, A.sub_department, A.efficiency_description, A.kpi_prefix_id, A.kpi_estimate_year,"
        //    + " A.kpi_prefix, A.kpi_prefix_short, A.kpi_estimate, A.unit_id, A.kpi_unit, A.kpi_unit_short, A.service_plan_id, A.service_plan,"
        //    + " B.kpi_year, B.kpi_month, B.kpi_progress, B.kpi_remark From view_kpi A Left Join"
        //    + " (Select kpi_progress.kpi_id, kpi_progress.kpi_year, kpi_progress.kpi_month, kpi_progress.kpi_progress, kpi_progress.kpi_remark"
        //    + " From kpi_progress Where kpi_progress.kpi_year = '" + opYear + "' And kpi_progress.kpi_month = " + opMonth + ") B On A.kpi_id = B.kpi_id" + strsql;

        //    strsql = "Select a.kpi_id As kpi_id, a.kpm_id As kpm_id, a.kpm_description As kpm_description,a.manager_id As manager_id,"
        //        + " a.manager_name As manager_name, a.manager_description As manager_description,"
        //        + " a.director_id as director_id, a.director_name As director_name,"
        //        + " a.director_description As director_description, a.department As department, a.sub_department As sub_department,"
        //        + " a.efficiency_description As efficiency_description, a.kpi_prefix_id As kpi_prefix_id, a.kpi_prefix As kpi_prefix,"
        //        + " a.kpi_prefix_short As kpi_prefix_short, a.kpi_estimate As kpi_estimate, a.unit_id As unit_id, a.kpi_unit As kpi_unit,"
        //        + " a.kpi_unit_short As kpi_unit_short, a.kpi_estimate_year As kpi_estimate_year, B.kpi_year, B.kpi_month, B.kpi_progress, B.kpi_remark"
        //        + " From view_kpi_progress a Left Join"
        //        + " (Select kpi_progress.kpi_id, kpi_progress.kpi_year, kpi_progress.kpi_month, kpi_progress.kpi_progress, kpi_progress.kpi_remark"
        //        + " From kpi_progress Where kpi_progress.kpi_year = '" + opYear + "' And kpi_progress.kpi_month = " + opMonth + ") B On a.kpi_id = B.kpi_id";
        //    return strsql;
        //}
        //public static string QMonthlyKPIProgress(string opYear, int opMonth, string opDirector, string opManager)
        //{
        //    string strsql = "";
        //    string opmonth = Enum.GetName(typeof(Months), opMonth);
        //    if (string.IsNullOrEmpty(opManager) || opManager=="-0-")
        //    {
        //        strsql = " WHERE director_id='" + opDirector + "'";
        //    }
        //    else
        //    {
        //        strsql = " WHERE director_id='" + opDirector + "' AND manager_id='" + opManager + "'";
        //    }

        //    strsql = "Select A.kpi_id, A.kpm_id, A.kpm_description, A.manager_id, A.manager_name, A.manager_description, A.director_description,"
        //    + " A.director_name, A.director_id, A.department, A.sub_department, A.efficiency_description, A.kpi_prefix_id, A.kpi_estimate_year,"
        //    + " A.kpi_prefix, A.kpi_prefix_short, A.kpi_estimate, A.unit_id, A.kpi_unit, A.kpi_unit_short, A.service_plan_id, A.service_plan,"
        //    + " B.kpi_year, B.kpi_month, B.kpi_progress, B.kpi_remark From view_kpi A Left Join"
        //    + " (Select kpi_progress.kpi_id, kpi_progress.kpi_year, kpi_progress.kpi_month, kpi_progress.kpi_progress, kpi_progress.kpi_remark"
        //    + " From kpi_progress Where kpi_progress.kpi_year = '" + opYear + "' And kpi_progress.kpi_month = " + opMonth + ") B On A.kpi_id = B.kpi_id" + strsql;

        //    //strsql = "Select a.kpi_id As kpi_id, a.kpm_id As kpm_id, a.kpm_description As kpm_description,a.manager_id As manager_id,"
        //    //    + " a.manager_name As manager_name, a.manager_description As manager_description,"
        //    //    + " a.director_id as director_id, a.director_name As director_name,"
        //    //    + " a.director_description As director_description, a.department As department, a.sub_department As sub_department,"
        //    //    + " a.efficiency_description As efficiency_description, a.kpi_prefix_id As kpi_prefix_id, a.kpi_prefix As kpi_prefix,"
        //    //    + " a.kpi_prefix_short As kpi_prefix_short, a.kpi_estimate As kpi_estimate, a.unit_id As unit_id, a.kpi_unit As kpi_unit,"
        //    //    + " a.kpi_unit_short As kpi_unit_short, a.kpi_estimate_year As kpi_estimate_year, B.kpi_year, B.kpi_month, B.kpi_progress, B.kpi_remark"
        //    //    + " From view_kpi_progress a Left Join"
        //    //    + " (Select kpi_progress.kpi_id, kpi_progress.kpi_year, kpi_progress.kpi_month, kpi_progress.kpi_progress, kpi_progress.kpi_remark"
        //    //    + " From kpi_progress Where kpi_progress.kpi_year = '" + opYear + "' And kpi_progress.kpi_month = " + opMonth + ") B On a.kpi_id = B.kpi_id " + strsql;
        //    return strsql;
        //}

    }
}
