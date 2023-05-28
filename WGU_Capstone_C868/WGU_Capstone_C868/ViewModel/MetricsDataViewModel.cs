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
using Syncfusion.Maui.Data;

namespace WGU_Capstone_C868.ViewModel
{
    public partial class MetricsDataViewModel : LoginPageViewModel
    {
        public static User ThisUser = new User();
        public static SfDataGrid entryGrid;
        public bool IsResults = false;

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

        public ObservableCollection<Appointment> _appointmentsReport = new ObservableCollection<Appointment>();
        public ObservableCollection<Appointment> AppointmentsReport
        {
            get { return _appointmentsReport; }
            set { SetProperty(ref _appointmentsReport, value); }
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

        [RelayCommand]
        private async Task LoadAppointments()
        {
            try
            {
                _appointmentsReport = await AppointmentCalls.GetAppointmentsAsync();
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

        [ObservableProperty]
        string stringToCompare;

        [RelayCommand]
        public async Task FilterRecords()
        {
            string compare = StringToCompare;
            if (compare.Length >= 3)
            {
                FilterTheRecords(compare);
                if (!IsResults)
                {
                    await Shell.Current.DisplayAlert("No Results", "No records contain results that match!", "Okay");
                }
                return;
            }
            else
            {
                await Shell.Current.DisplayAlert("Too Short", "Search must be 3 characters or more long.", "Ok");
            }
        }

        public void FilterTheRecords(string stringToCompare)
        {
            //String searchs for contains
            //hits all strings
            //Each string value in each record of selected display type
            //Compiles all results
            //Then clears the ObservableCollection and reloads with results.

            if (CanSeeEntries)
            {
                ObservableCollection<Relapse> holderOfTruth = new ObservableCollection<Relapse>();
                holderOfTruth.Clear();
                foreach (Relapse r in DiaryEntries)
                {
                    if (r.Location.Contains(stringToCompare) || r.Notes.Contains(stringToCompare))
                    {
                        holderOfTruth.Add(r);
                        IsResults = true;
                    }
                }

                if(holderOfTruth.Count != 0)
                {
                    DiaryEntries.Clear();
                    DiaryEntries = holderOfTruth;
                }
            }
            else if (CanSeeTriggers)
            {
                ObservableCollection<Model.Trigger> holderOfTruth = new ObservableCollection<Model.Trigger>();
                holderOfTruth.Clear();

                foreach (Model.Trigger t in TriggersReport)
                {
                    if (t.Title.Contains(stringToCompare) || t.Description.Contains(stringToCompare))
                    {
                        holderOfTruth.Add(t);
                        IsResults = true;
                    }
                }

                if (holderOfTruth.Count != 0)
                {
                    TriggersReport.Clear();
                    TriggersReport = holderOfTruth;
                }
            }
            else if (CanSeeSymptoms)
            {
                ObservableCollection<Symptom> holderOfTruth = new ObservableCollection<Symptom>();
                holderOfTruth.Clear();

                foreach (Symptom s in SymptomsReport)
                {
                    if (s.Title.Contains(stringToCompare) || s.Description.Contains(stringToCompare))
                    {
                        holderOfTruth.Add(s);
                        IsResults = true;
                    }
                }

                if (holderOfTruth.Count != 0)
                {
                    SymptomsReport.Clear();
                    SymptomsReport = holderOfTruth;
                }
            }
            else if(CanSeeAppointments)
            {
                ObservableCollection<Appointment> holderOfTruth = new ObservableCollection<Appointment>();
                holderOfTruth.Clear();

                foreach (Appointment a in AppointmentsReport)
                {
                    if (a.LocationName.Contains(stringToCompare) || a.Notes.Contains(stringToCompare))
                    {
                        holderOfTruth.Add(a);
                        IsResults = true;
                    }
                }

                if (holderOfTruth.Count != 0)
                {
                    AppointmentsReport.Clear();
                    AppointmentsReport = holderOfTruth;
                }
            }
        }
    }
}
