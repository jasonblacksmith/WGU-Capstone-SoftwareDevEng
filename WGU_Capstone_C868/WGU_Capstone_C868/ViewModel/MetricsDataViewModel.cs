using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGU_Capstone_C868.ViewModel
{
    public partial class MetricsDataViewModel : LoginPageViewModel
    {
        public static User ThisUser = new User();

        [RelayCommand]
        public async Task Reload()
        {
            PageTitle = "Metrics and Data";
            TheUser = ThisUser;
            await Init();
        }

        private async Task Init()
        {

        }
    }
}
