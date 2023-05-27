using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Maui.DataGrid;
using WGU_Capstone_C868.Model;
using WGU_Capstone_C868.Model.DataStructs;
using WGU_Capstone_C868.Services.Calls;

namespace WGU_Capstone_C868.ViewModel
{
    public partial class MetricsDataViewModel : LoginPageViewModel
    {
        public static User ThisUser = new User();

        RelapseCalls RelapseCalls = new RelapseCalls();
        TriggerCalls TriggerCalls = new TriggerCalls();
        SymptomCalls SymptomCalls = new SymptomCalls();
        AppointmentCalls AppointmentCalls = new AppointmentCalls();

        public ObservableCollection<Relapse> _diaryEntries = new ObservableCollection<Relapse>();
        public ObservableCollection<Relapse> DiaryEntries
        {
            get { return _diaryEntries; }
            set { SetProperty(ref _diaryEntries, value); }
        }

        public ObservableCollection<Model.Trigger> _triggersReport = new ObservableCollection<Model.Trigger>();
        public ObservableCollection<Model.Trigger> TriggersReport
        {
            get { return _triggersReport; }
            set { SetProperty(ref _triggersReport, value); }
        }

        public ObservableCollection<Symptom> _symptomsReport = new ObservableCollection<Symptom>();
        public ObservableCollection<Symptom> SymptomsReport
        {
            get { return _symptomsReport; }
            set { SetProperty(ref _symptomsReport, value); }
        }

        public ObservableCollection<Appointment> _appointmentReport = new ObservableCollection<Appointment>();
        public ObservableCollection<Appointment> AppointmentsReport
        {
            get { return _appointmentReport; }
            set { SetProperty(ref _appointmentReport, value); }
        }

        [ObservableProperty]
        public bool canSeeEntries = true;

        [ObservableProperty]
        public bool canSeeTriggers = false;

        [ObservableProperty]
        public bool canSeeSymptoms = false;

        [ObservableProperty]
        public bool canSeeAppointments = false;

        [RelayCommand]
        public async Task Reload()
        {
            PageTitle = "Metrics and Data";
            TheUser = ThisUser;
            await Init();
        }

        private async Task Init()
        {
            await LoadEntries();
        }

        [RelayCommand]
        private async Task LoadEntries()
        {
            try
            {
                DiaryEntries = await RelapseCalls.GetRelapsesAsync();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            CanSeeEntries = true;
            CanSeeTriggers = false;
            CanSeeSymptoms = false;
            CanSeeAppointments = false;
        }

        [RelayCommand]
        private async Task LoadTriggers()
        {
            try
            {
                TriggersReport = await TriggerCalls.GetTriggersAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            CanSeeEntries = false;
            CanSeeTriggers = true;
            CanSeeSymptoms = false;
            CanSeeAppointments = false;
        }

        [RelayCommand]
        private async Task LoadSymptoms()
        {
            try
            {
                SymptomsReport = await SymptomCalls.GetSymptomsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            CanSeeEntries = false;
            CanSeeTriggers = false;
            CanSeeSymptoms = true;
            CanSeeAppointments = false;
        }
        //TODO: Fix this one for appointments
        [RelayCommand]
        private async Task LoadAppointments()
        {
            try
            {
                _symptomsReport = await SymptomCalls.GetSymptomsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            CanSeeEntries = false;
            CanSeeTriggers = false;
            CanSeeSymptoms = false;
            CanSeeAppointments = true;
        }

        [RelayCommand]
        public async Task BackToLogin()
        {
            TheUser = null;
            ThisUser = null;

            // Load new page
            await Shell.Current.GoToAsync("//MainPage", true);
        }
    }
}


//data.Clear();
//data.Add(new RelapseStruct(d, "Elephant Attack at Zoo", 0, "Dumbo dun went full cray cray and it dun relapsed me sumfin gud fierce like!", 1, 1, 2));
//data.Add(new RelapseStruct(d1, "Singing in Public", 0, "Not cool y'all being oput on the spot an all!", 1, 1, 2));
//data.Add(new RelapseStruct(d2, "Wrestling in local League", 0, "I think it was the choke hold that did it... but I can't remember seeing as I passed out!", 1, 1, 2));
//data.Add(new RelapseStruct(d3, "Spanish Fork Niece and Nephews house", 0, "My Sister In-Law is a horder and not the most clean, so being here is no beuno!", 1, 1, 2));

//_diaryEntries.Clear();
//foreach (RelapseStruct rs in data)
//{
//    _diaryEntries.Add(rs.Relapse);
//}

//foreach (Relapse r in DiaryEntries)
//{
//    await RelapseCalls.AddRelapseAsync(r);
//}
