using System;

namespace WGU_Capstone_C868.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        public string title = "My MS Tracker";

        [ObservableProperty]
        public string pageTitle;

        [ObservableProperty]
        public string dateToday = DateTime.Today.ToString("MM/dd/yyyy");
    }
}
