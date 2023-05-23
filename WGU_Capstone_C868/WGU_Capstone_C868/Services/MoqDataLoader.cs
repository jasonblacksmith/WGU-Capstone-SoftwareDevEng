using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Calls;

namespace WGU_Capstone_C868.Services
{
    public class MoqDataLoader
    {
        CriticalObjects CriticalObjects = new CriticalObjects();

        private UserCalls userCalls = new UserCalls();
        private AppointmentCalls appointmentCalls = new AppointmentCalls();
        private AddressCalls addressCalls = new AddressCalls();
        private ResultCalls resultCalls = new ResultCalls();
        private DoctorsNoteCalls doctorsNoteCalls = new DoctorsNoteCalls();
        private VisitCalls visitCalls = new VisitCalls();
        private RelapseCalls relapseCalls = new RelapseCalls();
        private TriggerCollectionCalls tCC = new TriggerCollectionCalls();
        private SymptomCollectionCalls sCC = new SymptomCollectionCalls();
        private TriggerCalls tCalls = new TriggerCalls();
        private SymptomCalls sCalls = new SymptomCalls();

        public async void Init()
        {
            try
            {
                //User One
                User user = new User();
                user.UserId = 1;
                user.UserName = "Test1";
                user.Name = "Test Testerton";
                user.Password = "Test1234!";
                await userCalls.AddUserAsync(user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

            try
            {
                //User Two
                User user1 = new User();
                user1.UserId = 2;
                user1.UserName = "jasonrwhalen";
                user1.Name = "Jason Tester";
                user1.Password = "Telec@st3rHH";
                await userCalls.AddUserAsync(user1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }


            //One Upcoming Appointment
            Appointment coA = CriticalObjects.AppointmentData;
            coA.AppointmentId = 1;
            coA.ProceedureId = 1;
            coA.LocationName = "UVRMC Imaging";
            coA.AddressId = 1;
            coA.Notes = "Must be fasting and no metal on porson or clothing";
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
            coB.Notes = "Must be fasting and no metal on person or clothing";
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
            Model.Trigger trigger1 = new Model.Trigger();
            Model.Trigger trigger2 = new Model.Trigger();
            trigger.TriggerId = 1;
            trigger.TriggerCollectionId = 1;
            trigger.Title = "Family Gathering";
            trigger.Description = "When I am around family that I don't like at family gatherings my stress goes way up and it can trigger a relapse";
            trigger.IsNew = false;
            await tCalls.AddTriggerAsync(trigger);
            //Another one
            trigger1.TriggerId = 2;
            trigger1.TriggerCollectionId = 1;
            trigger1.Title = "Work stress";
            trigger1.Description = "When difficult at work my stress increases and it can trigger a relapse";
            trigger1.IsNew = false;
            await tCalls.AddTriggerAsync(trigger1);
            //And one more for the show
            trigger2.TriggerId = 3;
            trigger2.TriggerCollectionId = 1;
            trigger2.Title = "Clowns";
            trigger2.Description = "Clowns freak me out and have caused a relapse recently";
            trigger2.IsNew = true;
            await tCalls.AddTriggerAsync(trigger2);
            //Associated SymptomCollection
            SymptomCollection sC = new SymptomCollection();
            sC.SymptomCollectionId = 1;
            sC.UserId = 2;
            await sCC.AddSymptomCollectionAsync(sC);
            //Associated Symptoms
            Symptom symptom = new Symptom();
            Symptom symptom1 = new Symptom();
            Symptom symptom2 = new Symptom();
            symptom.SymptomId = 1;
            symptom.SymptomCollectionId = 1;
            symptom.Title = "Tingling in hands";
            symptom.Description = "Tingling and numbness in my fingers and hands";
            symptom.IsNew = false;
            await sCalls.AddSymptomAsync(symptom);
            //Another one
            symptom1.SymptomId = 2;
            symptom1.SymptomCollectionId = 1;
            symptom1.Title = "Fatigue";
            symptom1.Description = "Very tired or easily exhausted";
            symptom1.IsNew = false;
            await sCalls.AddSymptomAsync(symptom1);
            //And one more for the show
            symptom2.SymptomId = 3;
            symptom2.SymptomCollectionId = 1;
            symptom2.Title = "Right Leg Slow";
            symptom2.Description = "My right leg feels heavy and is slow to react sometimes when I go to walk from sitting or laying down";
            symptom2.IsNew = true;
            await sCalls.AddSymptomAsync(symptom2);
        }
    }
}