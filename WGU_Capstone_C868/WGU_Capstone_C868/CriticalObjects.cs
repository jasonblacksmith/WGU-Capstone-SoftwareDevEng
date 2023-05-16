using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGU_Capstone_C868
{
    public static class CriticalObjects
    {
        public static Address AddressData = new Address();
        public static Appointment AppointmentData = new Appointment();
        public static AppointmentState AppointmentStateData = new AppointmentState();
        public static DoctorsNote DoctorsNoteData = new DoctorsNote();
        public static Model.File FileData = new Model.File();
        public static FileCollection FileCollectionData = new FileCollection();
        public static Proceedure ProceedureData = new Proceedure();
        public static Question QuestionData = new Question();
        public static Relapse RelapseData = new Relapse();
        public static Result ThisResult = new Result();
        public static Symptom SymptomData = new Symptom();
        public static SymptomCollection SymptomCollectionData = new SymptomCollection();
        public static Model.Trigger TriggerData = new Model.Trigger();
        public static TriggerCollection TriggerCollectionData = new TriggerCollection();
        public static User UserData = new User();
        public static Visit VisitData = new Visit();
        public static VisitType VisitTypeData = new VisitType();

        //public CriticalObjects(Address addressData, Appointment appointmentData, AppointmentState appointmentStateData, DoctorsNote doctorsNoteData, Model.File fileData, FileCollection fileCollectionData, Proceedure proceedureData, Question questionData, Relapse relapseData, Result thisResult, Symptom symptomData, SymptomCollection symptomCollectionData, Model.Trigger triggerData, TriggerCollection triggerCollectionData, User userData, Visit visitData, VisitType visitTypeData)
        //{
        //    AddressData = addressData;
        //    AppointmentData = appointmentData;
        //    AppointmentStateData = appointmentStateData;
        //    DoctorsNoteData = doctorsNoteData;
        //    FileData = fileData;
        //    FileCollectionData = fileCollectionData;
        //    ProceedureData = proceedureData;
        //    QuestionData = questionData;
        //    RelapseData = relapseData;
        //    ThisResult = thisResult;
        //    SymptomData = symptomData;
        //    SymptomCollectionData = symptomCollectionData;
        //    TriggerData = triggerData;
        //    TriggerCollectionData = triggerCollectionData;
        //    UserData = userData;
        //    VisitData = visitData;
        //    VisitTypeData = visitTypeData;
        //}
    }
}
