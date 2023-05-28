#if DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Calls;


namespace WGU_Capstone_C868.Services.ServiceTests
{
    public class AppointmentServiceCallUnitTests
    {
        public static int passCount = 0;
        public static int failCount = 0;

        List<string> CasesThatPassed = new List<string>();
        List<string> CasesThatFailed = new List<string>();

        public AppointmentCalls appointmentCalls = new AppointmentCalls();

        Appointment appointmentA = new Appointment()
        {
            LocationName = "A",
            ProceedureId = 1,
            AddressId = 1,
            Notes = "Testing with appointmentA",
            PhoneNumber = "888-888-8888",
            Current = true,
            DateAndTime = DateTime.Today,
            UserId = 1
        };

        Appointment appointmentA_B = new Appointment();

        public async Task RunAll()
        {
            Debug.WriteLine("Test run started for AppointmentServiceCallUnitTests...");
            await CanAddAppointment(appointmentA);
            await CanGetAppointment(appointmentA);
            await CanUpdateAppointment(appointmentA_B, appointmentA);
            await CanDeleteAppointment(appointmentA_B);

            Debug.WriteLine($"Passing test Total: {passCount} Failing test Total: {failCount}");
            Debug.WriteLine("Passing Tests:");
            foreach(string s in CasesThatPassed)
            {
                Debug.WriteLine(s);
            } 
            Debug.WriteLine("Failing Tests:");
            foreach(string s in CasesThatFailed)
            {
                Debug.WriteLine(s);
            }
        }
        
        //Created simple internal Unit Tester
        async Task CanAddAppointment(Appointment appointmentA)
        {
            try
            {
                await appointmentCalls.AddAppointmentAsync(appointmentA);
                Debug.Assert(true);
                passCount++;
                CasesThatPassed.Add("CanAddAppointment - PASSED");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.Assert(false);
                failCount++;
                CasesThatFailed.Add("CanAddAppointment - FAILED");
                throw;
            }
        }

        async Task CanGetAppointment(Appointment appointmentA)
        {
            try
            {
                await appointmentCalls.GetAppointmentAsync(appointmentA.AppointmentId);
                Debug.Assert(true);
                passCount++;
                CasesThatPassed.Add("CanGetAppointment - PASSED");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.Assert(false);
                failCount++;
                CasesThatFailed.Add("CanGetAppointment - FAILED");
                throw;
            }
        }

        async Task CanUpdateAppointment(Appointment appointmentA_B, Appointment appointmentA)
        {
            try
            {
                appointmentA_B = appointmentA;
                appointmentA_B.Current = false;
                appointmentA_B.DateAndTime = DateTime.Now.AddMonths(-1);
                appointmentA_B.PhoneNumber = "999-999-9999";
                await appointmentCalls.UpdateAppointmentAsync(appointmentA_B);
                Debug.Assert(true);
                passCount++;
                CasesThatPassed.Add("CanUpdateAppointment - PASSED");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.Assert(false);
                failCount++;
                CasesThatFailed.Add("CanUpdateAppointment - FAILED");
                throw;
            }
        }

        async Task CanDeleteAppointment(Appointment appointmentA_B)
        {
            try
            {
                await appointmentCalls.RemoveAppointmentAsync(appointmentA_B);
                Debug.Assert(true);
                passCount++;
                CasesThatPassed.Add("CanRemoveAppointment - PASSED");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.Assert(false);
                failCount++;
                CasesThatFailed.Add("CanRemoveAppointment - FAILED");
                throw;
            }
        }
    }
}
#endif