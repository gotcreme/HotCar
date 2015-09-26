namespace HotCar.WebUI.Admin.Code.ExceptionWorking.Types
{
    public interface IWorkingExeption
    {
        string WhatHappenedInfo { get; }
        string WhyHappenedInfo { get; }
        string SuggestionInfo { get; }
    }
}
