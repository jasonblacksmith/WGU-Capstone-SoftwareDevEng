using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Interfaces;

namespace WGU_Capstone_C868.Services.Calls
{
    //TODO: Clean Up Calls... ALL OF THEM
    public class AppointmentStateCalls : IAppointmentStateCalls
    {
        public AppointmentState appointmentState;
        public ObservableCollection<AppointmentState> appointmentStates = new();

        //Returns the desired AppointmentState record from the DB
        public async Task<AppointmentState> GetAppointmentStateAsync(int pk)
        {
            try
            {
                AppointmentState GetAppointmentState = await SqLiteDataService.Db.GetAsync<AppointmentState>(pk);
                return GetAppointmentState;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns an ObservableCollection of all AppointmentStates int the table
        public async Task<ObservableCollection<AppointmentState>> GetAppointmentStatesAsync()
        {
            List<AppointmentState> AppointmentStates = await SqLiteDataService.Db.Table<AppointmentState>().ToListAsync();
            foreach (AppointmentState AppointmentState in AppointmentStates)
            {
                appointmentStates.Add(AppointmentState);
            }
            return appointmentStates;
        }
    }
}
