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
    public partial class ImgOrLabViewModel : LoginPageViewModel
    {
        //TODO: Look into removing the Critical Objects file.
        public static User ThisUser = new User();
        public static bool Edit = false;
        public Appointment CurrentAppointment = new Appointment();

        private static AppointmentCalls AppointmentCalls = new AppointmentCalls();
        private static ProceedureCalls ProceedureCalls = new ProceedureCalls();
        private static AddressCalls AddressCalls = new AddressCalls();
        private static ResultCalls ResultCalls = new ResultCalls();
        private static FileCollectionCalls FileCollectionCalls = new FileCollectionCalls();
        private static FileCalls FileCalls = new FileCalls();

        [ObservableProperty]
        private string selectedProceedure;

        [ObservableProperty]
        private string locationName;

        [ObservableProperty]
        private string phoneNum;

        [ObservableProperty]
        private string streetAddress;

        [ObservableProperty]
        private string suiteOrBuilding;

        [ObservableProperty]
        private string city;

        [ObservableProperty]
        private string zipCode;

        [ObservableProperty]
        private string country;

        [ObservableProperty]
        private string latitute;

        [ObservableProperty]
        private string longitude;

        [ObservableProperty]
        private DateTime dateAppointment;

        [ObservableProperty]
        private TimeSpan timeAppointment;

        [ObservableProperty]
        private string noteAppointment;

        [ObservableProperty]
        private bool isEdit = false;

        [ObservableProperty]
        private string updateOrSave = "Save";

        public ImgOrLabViewModel() 
        {
            pageTitle = "Imaging and Labs";
            theUser = ThisUser;
            EditMode(Edit);

        }

        [RelayCommand]
        public void Delete()
        {

        }

        public void EditMode(bool edit)
        {
            if(edit)
            {
                IsEdit = true;
                UpdateOrSave = "Update";
            }
            else
            {
                IsEdit = false;
                UpdateOrSave = "Save";
            }
        }

        public void OnSelectedProceedureChanged()
        {

        }

        [RelayCommand]
        public void Save()
        {
            if(IsEdit)
            {

            }
            else
            {

            }
        }
    }
}
