using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Handlers.Items;
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
        TriggerCalls TriggerCalls = new();
        SymptomCalls SymptomCalls = new();

        Relapse newRelapse = new();
        Model.Trigger newtrigger = new();
        Symptom newSymptom = new();

        [ObservableProperty]
        Relapse selectedRelapse = new();

        [ObservableProperty]
        Model.Trigger selectedTrigger = new();

        [ObservableProperty]
        Symptom selectedSymptom = new();

        [ObservableProperty]
        string entryNote;

        [ObservableProperty]
        bool isNotes;

        [ObservableProperty]
        bool isNotesButton;

        [ObservableProperty]
        bool isTriggers;

        [ObservableProperty]
        bool isTriggersButton;

        [ObservableProperty]
        bool isSymptoms;

        [ObservableProperty]
        bool isSymptomsButton;

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
            await Init();
            SelectedRelapse = RelapseDiaryEntries.FirstOrDefault();
        }

        public async Task Init()
        {
            await NotesSelected();
        }

        [RelayCommand]
        private async Task NotesSelected()
        {
            ObservableCollection<Relapse> allRelapses = new();
            int UserId = TheUser.UserId;

            RelapseDiaryEntries.Clear();
            allRelapses.Clear();

            IsNotes = true;
            IsTriggers = false;
            IsSymptoms = false;

            IsNotesButton = false;
            IsTriggersButton = true;
            IsSymptomsButton = true;

            allRelapses = await RelapseCalls.GetRelapsesAsync();

            foreach (Relapse r in allRelapses)
            {
                if (r.UserId == UserId)
                {
                    RelapseDiaryEntries.Add(r);
                }
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
        private async Task TriggersSelected()
        {
            ObservableCollection<Model.Trigger> _triggersAll = new();
            Relapse relapse = SelectedRelapse;

            TriggersForUser.Clear();
            _triggersAll.Clear();

            IsNotes = false;
            IsTriggers = true;
            IsSymptoms = false;

            IsNotesButton = true;
            IsTriggersButton = false;
            IsSymptomsButton = true;

            _triggersAll = await TriggerCalls.GetTriggersAsync();

            foreach (Model.Trigger t in _triggersAll)
            {
                if(relapse.TriggerCollectionId == t.TriggerCollectionId)
                {
                    TriggersForUser.Add(t);
                }
            }
        }

        [RelayCommand]
        private async Task SymptomsSelected()
        {
            ObservableCollection<Symptom> _symptomsAll = new();
            Relapse relapse = SelectedRelapse;

            SymptomsForUser.Clear();
            _symptomsAll.Clear();

            IsNotes = false;
            IsTriggers = false;
            IsSymptoms = true;

            IsNotesButton = true;
            IsTriggersButton = true;
            IsSymptomsButton = false;

            _symptomsAll = await SymptomCalls.GetSymptomsAsync();
            foreach (Symptom s in _symptomsAll)
            {
                if (relapse.SymptomCollectionId == s.SymptomCollectionId)
                {
                    SymptomsForUser.Add(s);
                }
            }
        }

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("Dashboard", true);
        }

        [RelayCommand]
        private async Task AddEntry()
        {
            //Open Add Or Edit Page
        }

        [RelayCommand]
        private async Task EditEntry()
        {
            Relapse relapse = SelectedRelapse;
            await RelapseCalls.UpdateRelapseAsync(relapse);
        }

        [RelayCommand]
        private async Task DeleteEntry()
        {
            Relapse relapse = SelectedRelapse;
            await RelapseCalls.RemoveRelapseAsync(relapse);                                   
        }

        [RelayCommand]
        private async Task SaveEntry()
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
