using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web;
using WGU_Capstone_C868.Services.Calls;
using WGU_Capstone_C868.View;

namespace WGU_Capstone_C868.ViewModel
{
    public partial class DrVisitNotesViewModel : LoginPageViewModel
    {
        public static User ThisUser = new User();

        public DrVisitNotesViewModel()
        {
            Task task = Init();
        }

        public async Task Init()
        {

        }
    }
}
