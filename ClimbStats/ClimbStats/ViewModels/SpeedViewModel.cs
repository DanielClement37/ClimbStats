using ClimbStats.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClimbStats.ViewModels
{
    public class SpeedViewModel : BaseViewModel
    {
        private SQLiteAsyncConnection conn;
        public string StatusMessage { get; set; }

        public SpeedViewModel(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<SpeedClimb>().Wait();

            Title = "Speed Climbing";
        }

        //Get all climbs
        public async Task<List<SpeedClimb>> GetAllSpeedClimbs()
        {
            try
            {
                return await conn.Table<SpeedClimb>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<SpeedClimb>();
        }

        //Get one climb
        public async Task<SpeedClimb> GetSpeedClimb(int id)
        {
            try
            {
                return await conn.Table<SpeedClimb>().FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new SpeedClimb();
        }

        //Add climb
        public async Task AddSpeedClimb(double? time, bool topped)
        {
            int result = 0;

            var climb = new SpeedClimb
            {
                SendDate = DateTime.Today,
                SendTime = time,
                Topped = topped
            };

            try
            {
                if (ClimbValidation(climb))
                {
                    result = await conn.InsertAsync(climb);

                    StatusMessage = string.Format("{0} record(s) added \n[SendDate: {1},\nSent: {2}\nTime: {3}]", result, climb.SendDate, climb.Topped, climb.SendTime);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", time, ex.Message);
                Console.WriteLine(StatusMessage);
            }
        }

        //Delete one climb
        public async Task<int> DeleteClimb(int id)
        {
            try
            {
                return await conn.DeleteAsync<SpeedClimb>(id);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to delete data. {0}", ex.Message);
            }
            return -1;
        }

        //Edit Climb
        public async Task EditSpeedClimb(int id, double? time, bool topped)
        {
            try
            {
                var climb = await conn.Table<SpeedClimb>().FirstOrDefaultAsync(x => x.Id == id);
                climb.SendTime = time;
                climb.Topped = topped;

                if (ClimbValidation(climb))
                {
                    await conn.UpdateAsync(climb);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to update {0}. Error: {1}", time, ex.Message);
            }
        }

        //true if info is valid
        private bool ClimbValidation(SpeedClimb climb)
        {
            return true;
        }
    }
}