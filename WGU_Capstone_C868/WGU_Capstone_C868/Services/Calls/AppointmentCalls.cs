using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Interfaces;

namespace WGU_Capstone_C868.Services.Calls
{
    public class AppointmentCalls : IAppointmentCalls
    {
        public ObservableCollection<Appointment> appointments = new();

        //Creates and adds new Appointment record to DB
        public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
        {
            Appointment AddAppointment = new();
            AddAppointment = appointment;
            try
            {
                await SqLiteDataService.Db.InsertAsync(AddAppointment);
                return appointment;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns the desired Appointment record from the DB
        public async Task<Appointment> GetAppointmentAsync(int pk)
        {
            Appointment appointment = new();
            try
            {
                appointment = await SqLiteDataService.Db.GetAsync<Appointment>(pk);
                return appointment;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns an ObservableCollection of all Appointments int the table
        public async Task<ObservableCollection<Appointment>> GetAppointmentsAsync()
        {
            List<Appointment> Appointments = new();
            Appointments = await SqLiteDataService.Db.Table<Appointment>().ToListAsync();
            foreach (Appointment Appointment in Appointments)
            {
                appointments.Add(Appointment);
            }
            return appointments;
        }

        //Removes or Deletes the desired Appointment record from the DB
        public async Task<bool> RemoveAppointmentAsync(Appointment appointment)
        {
            try
            {
                await SqLiteDataService.Db.DeleteAsync<Appointment>(appointment);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Updates the desired Appointment in the Appointment table in the DB
        public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            Appointment UpdateAppointment = new();
            UpdateAppointment = appointment;
            try
            {
                await SqLiteDataService.Db.UpdateAsync(UpdateAppointment);
                return UpdateAppointment;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
