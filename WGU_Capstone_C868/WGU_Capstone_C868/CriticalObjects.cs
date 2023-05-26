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

        public Proceedure ProceedureData = new Proceedure();

        public Relapse RelapseData = new Relapse();

        public Symptom SymptomData = new Symptom();

        public Model.Trigger TriggerData = new Model.Trigger();

        public User UserData = new User();
    }
}
