using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Handlers.Items;
using Microsoft.Maui.Graphics;
using System.Collections;
using System.Collections.Immutable;
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
        public static string ThisDaysSinceMessage = "";

        RelapseCalls RelapseCalls = new();
        TriggerCalls TriggerCalls = new();
        SymptomCalls SymptomCalls = new();

        Relapse newRelapse = new();
        Model.Trigger newtrigger = new();
        Symptom newSymptom = new();

        int newTriggerCollectionId;
        int newSymptomCollectionId;

        bool IsEdit;

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

        [ObservableProperty]
        string selectedItem;

        [ObservableProperty]
        private DateTime dateTimeNow = DateTime.Now;

        [ObservableProperty]
        public string daysSinceMessage;

        [ObservableProperty]
        public bool firstTimeUser;

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
            if(DaysSinceMessage != null) 
            {
                DaysSinceMessage = ThisDaysSinceMessage;
            }
            else
            {
                ThisDaysSinceMessage = "Add your first entry!";
            }
            FirstTimeUser = false;
            await Init();
        }

        public async Task Init()
        {
            //await NotesSelected();
            //First Time Load Check (EmptyState?);
            await EmptyState();
            if (!FirstTimeUser)
            {
                await NotesSelected();
            }
            else
            {
                await FirstTimeGuided();
            }
        }

        public async Task EmptyState()
        {
            ObservableCollection<Relapse> listA = new ObservableCollection<Relapse>();
            try
            {
                listA = await RelapseCalls.GetRelapsesAsync();
                if(listA == null)
                {
                    FirstTimeUser= true;
                    return;
                }
                else
                {
                    foreach (Relapse r in listA)
                    {
                        if (r.UserId == TheUser.UserId)
                        {
                            FirstTimeUser = true;
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public async Task FirstTimeGuided()
        {
            IsNotes = true;
            IsTriggers = false;
            IsSymptoms = false;

            IsNotesButton = false;
            IsTriggersButton = true;
            IsSymptomsButton = true;

            RelapseDiaryEntries.Clear();

            SelectedRelapse = null;
            newTriggerCollectionId = 1;
            newSymptomCollectionId = 1;
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

            SelectedItem = "New Entry";
            try
            {
                allRelapses = await RelapseCalls.GetRelapsesAsync();
                allRelapses.OrderBy(x => x.DateAndTime).ToList();
                foreach (Relapse r in allRelapses)
                {
                    if (r.UserId == UserId)
                    {
                        RelapseDiaryEntries.Add(r);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            //Set Edit Status
            if (SelectedRelapse.UserId > 0)
            {
                IsEdit = true;
            }

            SelectedRelapse = RelapseDiaryEntries.FirstOrDefault();//Deffault on load
            newTriggerCollectionId = SelectedRelapse.TriggerCollectionId;
            newSymptomCollectionId = SelectedRelapse.SymptomCollectionId;
        } 

        [RelayCommand]
        private async Task TriggersSelected()
        {
            ObservableCollection<Model.Trigger> _triggersAll = new();

            TriggersForUser.Clear();
            _triggersAll.Clear();

            IsNotes = false;
            IsTriggers = true;
            IsSymptoms = false;

            IsNotesButton = true;
            IsTriggersButton = false;
            IsSymptomsButton = true;

            SelectedItem = "New Trigger";

            _triggersAll = await TriggerCalls.GetTriggersAsync();

            foreach (Model.Trigger t in _triggersAll)
            {
                if(newTriggerCollectionId == t.TriggerCollectionId)
                {
                    TriggersForUser.Add(t);
                }
            }

            //Set Edit Status
            if (SelectedTrigger.TriggerId > 0)
            {
                IsEdit = true;
            }
            SelectedTrigger = TriggersForUser.FirstOrDefault();
        }
        
        [RelayCommand]
        private async Task SymptomsSelected()
        {
            ObservableCollection<Symptom> _symptomsAll = new();

            SymptomsForUser.Clear();
            _symptomsAll.Clear();

            IsNotes = false;
            IsTriggers = false;
            IsSymptoms = true;

            IsNotesButton = true;
            IsTriggersButton = true;
            IsSymptomsButton = false;

            SelectedItem = "New Symptom";

            _symptomsAll = await SymptomCalls.GetSymptomsAsync();
            foreach (Symptom s in _symptomsAll)
            {
                if (newSymptomCollectionId == s.SymptomCollectionId)
                {
                    SymptomsForUser.Add(s);
                }
            }

            //Set Edit Status
            if (SelectedSymptom.SymptomId > 0)
            {
                IsEdit = true;
            }
            SelectedSymptom = SymptomsForUser.FirstOrDefault();
        }

        [RelayCommand]
        private void RelapseSelected(Relapse relapse)
        {
            SelectedRelapse = relapse;
        }

        [RelayCommand]
        private void TriggerSelected(Model.Trigger trigger)
        {
            SelectedTrigger = trigger;
        }

        [RelayCommand]
        private void SymptomSelected(Symptom symptom)
        {
            SelectedSymptom = symptom;
        }

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("//Dashboard", true);
        }

        [RelayCommand]
        private async Task DeleteEntry()
        {
            if (IsNotes)
            {
                Relapse relapse = SelectedRelapse;
                await RelapseCalls.RemoveRelapseAsync(relapse);//Removes the currently selected Relapse Record
                await NotesSelected();//Reloads the current dataview
                SelectedRelapse = RelapseDiaryEntries.FirstOrDefault();
            }

            if (IsTriggers)
            {
                Model.Trigger trigger = SelectedTrigger;
                await TriggerCalls.RemoveTriggerAsync(trigger);//Removes the currently selected Trigger Record
                await TriggersSelected();//Reloads the current dataview
                SelectedTrigger = TriggersForUser.FirstOrDefault();
            }

            if (IsSymptoms)
            {
                Symptom symptom = SelectedSymptom;
                await SymptomCalls.RemoveSymptomAsync(symptom);//Removes the currently selected Symptom Record
                await SymptomsSelected();//Reloads the current dataview
                SelectedSymptom = SymptomsForUser.FirstOrDefault();//Deffault on load
            }
                              
        }

        [RelayCommand]
        private async Task SaveEntry()
        {
            if (IsEdit)
            {
                if (IsNotes)
                {
                    if (ValidateRelapse())
                    {
                        Relapse relapse = SelectedRelapse;
                        await RelapseCalls.UpdateRelapseAsync(relapse);//Updates the currently selected Relapse record
                        await NotesSelected();//Reloads the current dataview
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Missing Inputs?", "Please make sure that there is a location name, date, and notes filled out or selected.", "Ok");
                    }

                }

                if (IsTriggers)
                {
                    if (ValidateTrigger())
                    {
                        Model.Trigger trigger = SelectedTrigger;
                        await TriggerCalls.UpdateTriggerAsync(trigger);//Updates the currently selected Trigger record
                        await TriggersSelected();//Reloads the current dataview
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Missing Inputs?", "Please make sure that there is a title, and description filled out.", "Ok");
                    }
                }

                if (IsSymptoms)
                {
                    if (ValidateSymptom())
                    {
                        Symptom symptom = SelectedSymptom;
                        await SymptomCalls.UpdateSymptomAsync(symptom);//Updates the currently selected Symptom record
                        await SymptomsSelected();//Reloads the current dataview
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Missing Inputs?", "Please make sure that there is a title, and description filled out.", "Ok");
                    }
                }
            }
            else
            {
                if (IsNotes)
                {
                    if (ValidateRelapse())
                    {
                        Relapse relapse = SelectedRelapse;
                        await RelapseCalls.AddRelapseAsync(relapse);//Adds the new Relapse record
                        await NotesSelected();//Reloads the current dataview
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Missing Inputs?", "Please make sure that there is a location name, date, and notes filled out or selected.", "Ok");
                    }
                }

                if (IsTriggers)
                {
                    if (ValidateTrigger())
                    {
                        Model.Trigger trigger = SelectedTrigger;
                        await TriggerCalls.AddTriggerAsync(trigger);//Adds the new Trigger record
                        await TriggersSelected();//Reloads the current dataview
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Missing Inputs?", "Please make sure that there is a title, and description filled out.", "Ok");
                    }
                }

                if (IsSymptoms)
                {
                    if (ValidateSymptom())
                    {
                        Symptom symptom = SelectedSymptom;
                        await SymptomCalls.AddSymptomAsync(symptom);//Adds the new Symptom record
                        await SymptomsSelected();//Reloads the current dataview
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Missing Inputs?", "Please make sure that there is a title, and description filled out.", "Ok");
                    }
                }
            }
        }

        [RelayCommand]
        private void AddANew()
        {
            IsEdit = false;
            if (IsNotes)
            {
                Relapse _newRelapse = new()
                {
                    RelapseId = 0,
                    UserId = SelectedRelapse.UserId,
                    Location = null,
                    DateAndTime = DateTime.Now,
                    Notes = null,
                    TriggerCollectionId = SelectedRelapse.TriggerCollectionId,
                    SymptomCollectionId = SelectedRelapse.SymptomCollectionId
                };
                SelectedRelapse = _newRelapse;
            }

            if (IsTriggers)
            {
                Model.Trigger _newTrigger = new()
                {
                    TriggerId = 0,
                    TriggerCollectionId= SelectedTrigger.TriggerCollectionId,
                    Title = null,
                    Description = null,
                    IsNew = true
                };
                SelectedTrigger = _newTrigger;
            }

            if (IsSymptoms)
            {
                Symptom _newSymptom = new()
                {
                    SymptomId = 0,
                    SymptomCollectionId = SelectedSymptom.SymptomCollectionId,
                    Title = null,
                    Description = null,
                    IsNew = true
                };
                SelectedSymptom = _newSymptom;
            }
        }

        [RelayCommand]
        public async Task BackToLogin()
        {
            TheUser = null;
            ThisUser = null;

            // Load new page
            await Shell.Current.GoToAsync("//MainPage", false);
        }

        public bool ValidateRelapse() //Bind on Unfocused event
        {
            if(SelectedRelapse != null && SelectedRelapse.Location != null && SelectedRelapse.Notes != null)
            {
                SelectedRelapse.Location.Trim();
                SelectedRelapse.Notes.Trim();
                if(SelectedRelapse.Location != null 
                    && SelectedRelapse.Location != "" 
                    && SelectedRelapse.Notes != null 
                    && SelectedRelapse.Notes != "")
                {
                    return true;
                }
            }
            return false;
        }

        public bool ValidateTrigger()
        {
            if(SelectedTrigger != null && SelectedTrigger.Title != null && SelectedTrigger.Description != null)
            {
                SelectedTrigger.Title.Trim();
                SelectedTrigger.Description.Trim();

                if (SelectedTrigger.Title != null 
                    && SelectedTrigger.Title != "" 
                    && SelectedTrigger.Description != null 
                    && SelectedTrigger.Description != "")
                {
                    return true;
                }
            }
            return false;
        }

        public bool ValidateSymptom()
        {
            if (SelectedSymptom != null && SelectedSymptom.Title != null && SelectedSymptom.Description != null)
            {
                SelectedSymptom.Title.Trim();
                SelectedSymptom.Description.Trim();

                if (SelectedSymptom.Title != null
                    && SelectedSymptom.Title != ""
                    && SelectedSymptom.Description != null
                    && SelectedSymptom.Description != "")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
