using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGU_Capstone_C868
{
    public class CriticalObjects
    {
#pragma warning disable IDE0090 // Use 'new(...)'
        public Address AddressData = new Address();
#pragma warning restore IDE0090 // Use 'new(...)'
        public Appointment AppointmentData = new Appointment();
        public DoctorsNote DoctorsNoteData = new DoctorsNote();
        public Proceedure ProceedureData = new Proceedure();
        public Question QuestionData = new Question();
        public Relapse RelapseData = new Relapse();
        public Result ThisResult = new Result();
        public Symptom SymptomData = new Symptom();
        public SymptomCollection SymptomCollectionData = new SymptomCollection();
        public Model.Trigger TriggerData = new Model.Trigger();
        public TriggerCollection TriggerCollectionData = new TriggerCollection();
        public User UserData = new User();
        public Visit VisitData = new Visit();
        public VisitType VisitTypeData = new VisitType();

    }
}
