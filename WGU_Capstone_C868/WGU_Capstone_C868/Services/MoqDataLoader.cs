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

                User user2 = new User();
                user2.UserId = 3;
                user2.UserName = "Test3";
                user2.Name = "Tester Testapalooza";
                user2.Password = "Test1234!";
                await userCalls.AddUserAsync(user2);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

            //One Upcoming Appointment
            Appointment coA = new()
            {
                AppointmentId = 1,
                ProceedureId = 1,
                LocationName = "UVRMC Imaging",
                AddressId = 1,
                Notes = "Must be fasting and no metal on person or clothing",
                Current = true,
                PhoneNumber = "123-456-7890",
                DateAndTime = DateTime.Now.AddDays(21),
                UserId = 1
            };
            await appointmentCalls.AddAppointmentAsync(coA);

            //Associated Address
            Address address = new()
            {
                AddressId = 1,
                StreetAddress = "1034 N 500 W",
                SuiteNumber = string.Empty,
                City = "Provo",
                ZipCode = "84604",
                State = "Utah",
                Country = "United States",
                Latitude = 40.24762438250751,
                Longitude = -111.6656509881319
            };
            await addressCalls.AddAddressAsync(address);

            //One Completed Appointment
            Appointment coB = new()
            {
                AppointmentId = 2,
                ProceedureId = 1,
                LocationName = "Rodizio Grill Img",
                AddressId = 2,
                Notes = "Must be fasting and no metal on person or clothing",
                Current = false,
                PhoneNumber = "(480) 813-5400",
                DateAndTime = DateTime.Now.AddMonths(-4),
                UserId = 2
            };
            await appointmentCalls.AddAppointmentAsync(coB);

            //Associated Address
            Address address1 = new()
            {
                AddressId = 2,
                StreetAddress = "1840 S Val Vista Dr,",
                SuiteNumber = string.Empty,
                City = "Mesa",
                ZipCode = "85204",
                State = "Arizona",
                Country = "United States",
                Latitude = 33.3822353,
                Longitude = -111.757013
            };
            await addressCalls.AddAddressAsync(address);

            //One Upcoming Appointment
            Appointment coC = new()
            {
                AppointmentId = 3,
                ProceedureId = 2,
                LocationName = "Nalu's South Shore Grill",
                AddressId = 3,
                Notes = "Come hungry and ready to dig in, Hawaii's #1 Poke!",
                Current = true,
                PhoneNumber = "(808) 891-8650",
                DateAndTime = DateTime.Now.AddMonths(4),
                UserId = 3
            };
            await appointmentCalls.AddAppointmentAsync(coC);

            //Associated Address
            Address address2 = new()
            {
                AddressId = 3,
                StreetAddress = "1280 S Kihei Rd",
                SuiteNumber = string.Empty,
                City = "Kihei",
                ZipCode = "96753",
                State = "Hawaii",
                Country = "United States",
                Latitude = 20.749231,
                Longitude = -156.456857
            };
            await addressCalls.AddAddressAsync(address2);

            //Associated Results
            Model.Result result = new()
            {
                ResultId = 1,
                AppointmentId = 2,
                UserId = 2,
                ProceedureId = 1,
                DoctorsNoteId = 1,
                OtherNotes = string.Empty
            };
            await resultCalls.AddResultAsync(result);

            //Associated Doctors Note
            DoctorsNote doctorsNote = new()
            {
                DoctorsNoteId = 1,
                VisitId = 1,
                Title = "MRI Follow Up Notes",
                Content = "Bacon ipsum dolor amet jerky tri-tip meatball corned beef. Sausage filet mignon corned beef turkey andouille, tail shank boudin turducken short loin ground round pork prosciutto. Hamburger biltong capicola tri-tip drumstick beef pancetta chuck turducken ham hock ribeye buffalo salami alcatra porchetta. Shoulder chislic sirloin, landjaeger andouille burgdoggen ground round spare ribs.",
                DoctorsName = "Matrim Cauthorn",
                WithResults = true,
                ResultsId = 1
            };
            await doctorsNoteCalls.AddDoctorsNoteAsync(doctorsNote);

            //Associated Visit
            Visit visit = new()
            {
                VisitId = 1,
                UserId = 2,
                VisitTypeId = 2,
                DateAndTime = DateTime.Now.AddMonths(-5)
            };
            await visitCalls.AddVisitAsync(visit);

            //Relapse Diary
            Relapse relapse = new()
            {
                RelapseId = 1,
                UserId = 2,
                Location = "Family Gathering",
                DateAndTime = DateTime.Now.AddMonths(-10),
                TriggerCollectionId = 1,
                SymptomCollectionId = 1
            };
            await relapseCalls.AddRelapseAsync(relapse);

            //Associated TriggerCollection
            TriggerCollection tC = new TriggerCollection
            {
                TriggerCollectionId = 1,
                UserId = 2
            };
            await tCC.AddTriggerCollectionAsync(tC);

            //Associated Triggers
            Model.Trigger trigger = new()
            {
                TriggerId = 1,
                TriggerCollectionId = 1,
                Title = "Family Gathering",
                Description = "When I am around family that I don't like at family gatherings my stress goes way up and it can trigger a relapse",
                IsNew = false
            };
            await tCalls.AddTriggerAsync(trigger);

            //Another one
            Model.Trigger trigger1 = new Model.Trigger
            {
                TriggerId = 2,
                TriggerCollectionId = 1,
                Title = "Work stress",
                Description = "When difficult at work my stress increases and it can trigger a relapse",
                IsNew = false
            };
            await tCalls.AddTriggerAsync(trigger1);

            //And one more for the show
            Model.Trigger trigger2 = new Model.Trigger
            {
                TriggerId = 3,
                TriggerCollectionId = 1,
                Title = "Clowns",
                Description = "Clowns freak me out and have caused a relapse recently",
                IsNew = true
            };
            await tCalls.AddTriggerAsync(trigger2);

            //Associated SymptomCollection
            SymptomCollection sC = new SymptomCollection
            {
                SymptomCollectionId = 1,
                UserId = 2
            };
            await sCC.AddSymptomCollectionAsync(sC);

            //Associated Symptoms
            Symptom symptom = new Symptom
            {
                SymptomId = 1,
                SymptomCollectionId = 1,
                Title = "Tingling in hands",
                Description = "Tingling and numbness in my fingers and hands",
                IsNew = false
            };
            await sCalls.AddSymptomAsync(symptom);

            //Another one
            Symptom symptom1 = new Symptom
            {
                SymptomId = 2,
                SymptomCollectionId = 1,
                Title = "Fatigue",
                Description = "Very tired or easily exhausted",
                IsNew = false
            };
            await sCalls.AddSymptomAsync(symptom1);

            //And one more for the show
            Symptom symptom2 = new Symptom
            {
                SymptomId = 3,
                SymptomCollectionId = 1,
                Title = "Right Leg Slow",
                Description = "My right leg feels heavy and is slow to react sometimes when I go to walk from sitting or laying down",
                IsNew = true
            };
            await sCalls.AddSymptomAsync(symptom2);
        }
    }
}