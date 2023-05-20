using System;

namespace WGU_Capstone_C868.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        internal string title = "My MS Tracker";

        [ObservableProperty]
        internal string pageTitle;

        [ObservableProperty]
        public string dateToday = DateTime.Today.ToString("MM/dd/yyyy");

        //[ObservableProperty]
        //[NotifyPropertyChangedFor(nameof(IsNotBusy))]
        //bool isBusy;

        //public bool IsNotBusy => !IsBusy;
    }
}

//TODO: Reports to consider; How many appointments created in a 3month, 6month period of time to determine usefullness of application??? Other reports along the same lines?