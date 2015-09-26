namespace HotCar.WebUI.Admin.Code.ExceptionWorking.Types
{
    public class SqlExceptionWorking : IWorkingExeption
    {
        public string WhatHappenedInfo
        {
            get { return "Помилка з'єднання з базою даних/Database connection error"; }
        }

        public string WhyHappenedInfo
        {
            get { return "база даних не доступна/database is not accessible"; }
        }

        public string SuggestionInfo
        {
            get { return "почекати декілька хвилин і спробувати ще раз/wait a few minutes and try again"; }
        }
    }
}