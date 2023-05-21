using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Model;
using WGU_Capstone_C868.Services.Calls;
using WGU_Capstone_C868.View;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WGU_Capstone_C868.ViewModel
{
    internal class MRIViewModel : LoginPageViewModel
    {
        //TODO: Look into removing the Critical Objects file.
        public User ThisUser = new User();

        private static AppointmentCalls AppointmentCalls = new AppointmentCalls();
        private static AppointmentStateCalls AppointmentStateCalls = new AppointmentStateCalls();
        private static ProceedureCalls ProceedureCalls = new ProceedureCalls();
        private static AddressCalls AddressCalls = new AddressCalls();
        private static ResultCalls ResultCalls = new ResultCalls();
        private static FileCollectionCalls FileCollectionCalls = new FileCollectionCalls();
        private static FileCalls FileCalls = new FileCalls();

        public MRIViewModel() 
        {
            
        }
    }
}
