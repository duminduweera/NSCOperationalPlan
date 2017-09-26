using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSCOperationalPlan
{
    class StrategyMeasures
    {
        private String _measureCode, _description,_source,_howMeasured,_year,manager_id,strategy_id,_comment;

        public string Comment
        {
            get
            {
                return _comment;
            }

            set
            {
                _comment = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }

        public string HowMeasured
        {
            get
            {
                return _howMeasured;
            }

            set
            {
                _howMeasured = value;
            }
        }

        public string Manager_id
        {
            get
            {
                return manager_id;
            }

            set
            {
                manager_id = value;
            }
        }

        public string MeasureCode
        {
            get
            {
                return _measureCode;
            }

            set
            {
                _measureCode = value;
            }
        }

        public string Source
        {
            get
            {
                return _source;
            }

            set
            {
                _source = value;
            }
        }

        public string Strategy_id
        {
            get
            {
                return strategy_id;
            }

            set
            {
                strategy_id = value;
            }
        }

        public string Year
        {
            get
            {
                return _year;
            }

            set
            {
                _year = value;
            }
        }


    }
}
