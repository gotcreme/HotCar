namespace HotCar.WebUI.Admin.Code.ExceptionWorking.Types
{
    public class UnexpectedErrorWorking : IWorkingExeption
    {
        public string WhatHappenedInfo
        {
            get { return "трапилась неочікувана помилка/unexpected error happened"; }
        }

        public string WhyHappenedInfo
        {
            get { return "причина невідома/reason is unknown"; }
        }

        public string SuggestionInfo
        {
            get { return "почекати декілька хвилин і спробувати знову, або зв'язатись з адміністратором сайту/wait a few minutes and try again or contact to administrator"; }
        }
    }
}