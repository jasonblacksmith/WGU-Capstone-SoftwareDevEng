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
        public Appointment appointment = new();
        public ObservableCollection<Appointment> appointments = new();

        //Creates and adds new Appointment record to DB
        public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
        {
            this.appointment = appointment;
            try
            {
                await SqLiteDataService.Db.InsertAsync(this.appointment);
                return this.appointment;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;            
            }
        }

        //Returns the desired Appointment record from the DB
        public async Task<Appointment> GetAppointmentAsync(int pk)
        {
            try
            {
                this.appointment = await SqLiteDataService.Db.GetAsync<Appointment>(pk);
                return this.appointment;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;            }
        }

        //Returns an ObservableCollection of all Appointments int the table
        public async Task<ObservableCollection<Appointment>> GetAppointmentsAsync()
        {
            try
            {
                List<Appointment> Appointments = new();
                Appointments = await SqLiteDataService.Db.Table<Appointment>().ToListAsync();
                foreach (Appointment a in Appointments)
                {
                    appointments.Add(a);
                }
                return appointments;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

        }

        //Removes or Deletes the desired Appointment record from the DB
        public async Task<bool> RemoveAppointmentAsync(Appointment appointment)
        {
            try
            {
                await SqLiteDataService.Db.DeleteAsync<Appointment>(appointment.AppointmentId);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        //Updates the desired Appointment in the Appointment table in the DB
        public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            try
            {
                await SqLiteDataService.Db.UpdateAsync(appointment);
                return appointment;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
