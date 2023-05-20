using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Calls;

namespace WGU_Capstone_C868.Services
{
    public static class MoqDataLoader
    {
        private static UserCalls userCalls = new UserCalls();
        private static AppointmentCalls appointmentCalls = new AppointmentCalls();
        private static AddressCalls addressCalls = new AddressCalls();
        private static ResultCalls resultCalls = new ResultCalls();
        private static DoctorsNoteCalls doctorsNoteCalls = new DoctorsNoteCalls();
        private static VisitCalls visitCalls = new VisitCalls();
        private static RelapseCalls relapseCalls = new RelapseCalls();
        private static TriggerCollectionCalls tCC = new TriggerCollectionCalls();
        private static SymptomCollectionCalls sCC = new SymptomCollectionCalls();
        private static TriggerCalls tCalls = new TriggerCalls();
        private static SymptomCalls sCalls = new SymptomCalls();

        public static async Task Init()
        {
            //User One
            User user = new User();
            user.UserId = 1;
            user.UserName = "Test1";
            user.Name = "Test Testerton";
            user.Password = "Test1234!";
            await userCalls.AddUserAsync(user);
            //User Two
            user.UserId = 2;
            user.UserName = "jasonrwhalen";
            user.Name = "Jason Tester";
            user.Password = "Telec@st3rHH";
            await userCalls.AddUserAsync(user);

            //One Upcoming Appointment
            Appointment coA = CriticalObjects.AppointmentData;
            coA.AppointmentId = 1;
            coA.ProceedureId = 1;
            coA.LocationName = "UVRMC Imaging";
            coA.AddressId = 1;
            coA.Notes = "Must be fasting and no metal on porson or clothing";
            coA.StateId = 1;
            coA.Current = true;
            coA.PhoneNumber = "123-456-7890";
            coA.DateAndTime = DateTime.Now.AddDays(21);
            coA.UserId = 1;
            await appointmentCalls.AddAppointmentAsync(coA);
            //Associated Address
            Address address = CriticalObjects.AddressData;
            address.AddressId = 1;
            address.StreetAddress = "1034 N 500 W";
            address.SuiteNumber = string.Empty;
            address.City = "Provo";
            address.ZipCode = "84604";
            address.Country = "United States";
            address.Latitude = 40.24762438250751;
            address.Longitude = -111.6656509881319;
            await addressCalls.AddAddressAsync(address);

            //One Completed Appointment
            Appointment coB = CriticalObjects.AppointmentData;
            coB.AppointmentId = 2;
            coB.ProceedureId = 1;
            coB.LocationName = "UVRMC Imaging";
            coB.AddressId = 1;
            coB.Notes = "Must be fasting and no metal on porson or clothing";
            coB.StateId = 3;
            coB.Current = false;
            coB.PhoneNumber = "098-765-4321";
            coB.DateAndTime = DateTime.Now.AddMonths(-6);
            coB.UserId = 2;
            await appointmentCalls.AddAppointmentAsync(coB);
            //Associated Results
            Model.Result result = CriticalObjects.ThisResult;
            result.ResultId = 1;
            result.AppointmentId = 2;
            result.UserId = 2;
            result.ProceedureId = 1;
            result.FileCollectionId = 1;
            result.DoctorsNoteId = 1;
            result.OtherNotes = string.Empty;
            await resultCalls.AddResultAsync(result);
            //Associated Doctors Note
            DoctorsNote doctorsNote = new DoctorsNote();
            doctorsNote.DoctorsNoteId = 1;
            doctorsNote.VisitId = 1;
            doctorsNote.Title = "MRI Follow Up Notes";
            doctorsNote.Content = "Bacon ipsum dolor amet jerky tri-tip meatball corned beef. Sausage filet mignon corned beef turkey andouille, tail shank boudin turducken short loin ground round pork prosciutto. Hamburger biltong capicola tri-tip drumstick beef pancetta chuck turducken ham hock ribeye buffalo salami alcatra porchetta. Shoulder chislic sirloin, landjaeger andouille burgdoggen ground round spare ribs.";
            doctorsNote.DoctorsName = "Matrim Cauthorn";
            doctorsNote.WithResults = true;
            doctorsNote.ResultsId = 1;
            await doctorsNoteCalls.AddDoctorsNoteAsync(doctorsNote);
            //Associated Visit
            Visit visit = new Visit();
            visit.VisitId = 1;
            visit.UserId = 2;
            visit.VisitTypeId = 2;
            visit.DateAndTime = DateTime.Now.AddMonths(-5);
            await visitCalls.AddVisitAsync(visit);

            //Relapse Diary
            Relapse relapse = new Relapse();
            relapse.RelapseId = 1;
            relapse.UserId = 2;
            relapse.Location = "Family Gathering";
            relapse.DateAndTime = DateTime.Now.AddMonths(-10);
            relapse.TriggersCollectionId = 1;
            relapse.SymptomCollectionId = 1;
            await relapseCalls.AddRelapseAsync(relapse);
            //Associated TriggerCollection
            TriggerCollection tC = new TriggerCollection();
            tC.TriggerCollectionId = 1;
            tC.UserId = 2;
            await tCC.AddTriggerCollectionAsync(tC);
            //Associated Triggers
            Model.Trigger trigger = new Model.Trigger();
            trigger.TriggerId = 1;
            trigger.TriggerCollectionId = 1;
            trigger.Title = "Family Gathering";
            trigger.Description = "When I am around family that I don't like at family gatherings my stress goes way up and it can trigger a relapse";
            trigger.IsNew = false;
            await tCalls.AddTriggerAsync(trigger);
            //Another one
            trigger.TriggerId = 2;
            trigger.TriggerCollectionId = 1;
            trigger.Title = "Work stress";
            trigger.Description = "When difficult at work my stress increases and it can trigger a relapse";
            trigger.IsNew = false;
            await tCalls.AddTriggerAsync(trigger);
            //And one more for the show
            trigger.TriggerId = 3;
            trigger.TriggerCollectionId = 1;
            trigger.Title = "Clowns";
            trigger.Description = "Clowns freak me out and have caused a relapse recently";
            trigger.IsNew = true;
            await tCalls.AddTriggerAsync(trigger);
            //Associated SymptomCollection
            SymptomCollection sC = new SymptomCollection();
            sC.SymptomCollectionId = 1;
            sC.UserId = 2;
            await sCC.AddSymptomCollectionAsync(sC);
            //Associated Symptoms
            Symptom symptom = new Symptom();
            symptom.SymptomId = 1;
            symptom.SymptomCollectionId = 1;
            symptom.Title = "Tingling in hands";
            symptom.Description = "Tingling and numbness in my fingers and hands";
            symptom.IsNew = false;
            await sCalls.AddSymptomAsync(symptom);
            //Another one
            symptom.SymptomId = 2;
            symptom.SymptomCollectionId = 1;
            symptom.Title = "Fatigue";
            symptom.Description = "Very tired or easily exhausted";
            symptom.IsNew = false;
            await sCalls.AddSymptomAsync(symptom);
            //And one more for the show
            symptom.SymptomId = 3;
            symptom.SymptomCollectionId = 1;
            symptom.Title = "Right Leg Slow";
            symptom.Description = "My right leg feels heavy and is slow to react sometimes when I go to walk from sitting or laying down";
            symptom.IsNew = true;
            await sCalls.AddSymptomAsync(symptom);
        }
    }
}