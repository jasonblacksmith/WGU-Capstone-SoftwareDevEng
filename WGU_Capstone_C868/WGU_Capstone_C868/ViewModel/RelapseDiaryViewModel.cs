using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web;
using WGU_Capstone_C868.Services.Calls;
using WGU_Capstone_C868.View;

namespace WGU_Capstone_C868.ViewModel
{
    public partial class RelapseDiaryViewModel : LoginPageViewModel
    {
        public static User ThisUser = new User();

        RelapseCalls RelapseCalls = new();
        TriggerCollectionCalls TriggerCollectionCalls = new();
        TriggerCalls TriggerCalls = new();
        SymptomCollectionCalls SymptomCollectionCalls = new();
        SymptomCalls SymptomCalls = new();

        Relapse newRelapse = new();
        Model.Trigger newtrigger = new();
        Symptom newSymptom = new();

        ObservableCollection<Relapse> _relapseDiaryEntries = new ObservableCollection<Relapse>();
        public ObservableCollection<Relapse> RelapseDiaryEntries
        {
            get { return _relapseDiaryEntries; }
            set { SetProperty(ref _relapseDiaryEntries, value); }
        }

        ObservableCollection<Model.Trigger> _triggersForUser = new ObservableCollection<Model.Trigger>();
        public ObservableCollection<Model.Trigger> TriggersForUser
        {
            get { return _triggersForUser; }
            set { SetProperty(ref _triggersForUser, value); }
        }

        ObservableCollection<Symptom> _symptomsForUser = new ObservableCollection<Symptom>();
        public ObservableCollection<Symptom> SymptomsForUser
        {
            get { return _symptomsForUser; } 
            set { SetProperty(ref _symptomsForUser, value); }
        }

        [RelayCommand]
        public async Task Reload()
        {
            PageTitle = "Relapse Diary";
            TheUser = ThisUser;
            RelapseDiaryEntries.Clear();
            await Init();
        }

        public async Task Init()
        {
            await LoadRelapses(TheUser.UserId);
        }

        private async Task LoadRelapses(int UserId)
        {
            ObservableCollection<Relapse> _allRelapses = await RelapseCalls.GetRelapsesAsync();
            foreach(Relapse r in _allRelapses)
            {
                if (r.UserId == UserId)
                {
                    RelapseDiaryEntries.Add(r);
                }
            }

            foreach(Relapse r in RelapseDiaryEntries)
            {
                await LoadUsersTriggers(r);
                await LoadUsersSymptoms(r);
            }
        }

        private async Task<Model.Trigger> SaveNewTrigger(Model.Trigger trigger)
        {

            //TODO: Put Save Trigger Logic Here

            await TriggerCalls.AddTriggerAsync(trigger);
            return trigger;
        }

        private async Task<Symptom> SaveNewSymptom(Symptom symptom)
        {

            //TODO: Put Save Trigger Logic Here

            await SymptomCalls.AddSymptomAsync(symptom);
            return symptom;
        }

        [RelayCommand]
        private async Task LoadUsersTriggers(Relapse relapse)
        {
            ObservableCollection<Model.Trigger> _triggersAll = await TriggerCalls.GetTriggersAsync();
            foreach (Model.Trigger t in _triggersAll)
            {
                if(relapse.TriggerCollectionId == t.TriggerCollectionId)
                {
                    TriggersForUser.Add(t);
                }
            }
        }

        private async Task LoadUsersSymptoms(Relapse relapse)
        {
            ObservableCollection<Symptom> _symptomsAll = await SymptomCalls.GetSymptomsAsync();
            foreach (Symptom s in _symptomsAll)
            {
                if (relapse.SymptomCollectionId == s.SymptomCollectionId)
                {
                    SymptomsForUser.Add(s);
                }
            }
        }

        [RelayCommand]
        private async Task AddEntry()
        {

        }

        [RelayCommand]
        public async Task BackToLogin()
        {
            TheUser = null;
            ThisUser = null;

            // Load new page
            await Shell.Current.GoToAsync("//MainPage", false);
        }
    }
}
