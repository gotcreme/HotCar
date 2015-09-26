using System;
using System.Data.SqlClient;
using HotCar.WebUI.Admin.Code.ExceptionWorking.Types;

namespace HotCar.WebUI.Admin.Code.ExceptionWorking
{
    public class ExceptionInfo
    {
        public IWorkingExeption GetInfo {get; private set; }

        public ExceptionInfo(Exception exception)
        {
            this.GetInfo = this.GetHandlerExeption(exception);
        }

        private IWorkingExeption GetHandlerExeption(Exception exception)
        {
            if (exception is SqlException)
            {
                return new SqlExceptionWorking();
            }
           
            return new UnexpectedErrorWorking();
        }
    }

}