using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSCOperationalPlan
{
    public class CustomException : Exception
    {
        public CustomException()
        {
        }
        public CustomException(string message)
            : base(message)
        {
            //switch (message)
            //{
            //    case "001":
            //        message = "Finance has not updated YTD details";
            //        break;
            //}
        }

        public CustomException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
