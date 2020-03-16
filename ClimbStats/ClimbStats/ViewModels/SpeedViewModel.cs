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

        //Get Fastest Climb
        //TODO: Change from bubbleSort
        public async Task<double> GetFastest()
        {
            try
            {
                double temp;
                var climbs = await conn.Table<SpeedClimb>().ToListAsync();

                for (int i = 0; i <= climbs.Count - 2; i++)
                {
                    for (int j = 0; j <= climbs.Count - 2; j++)
                    {
                        if (climbs[j].SendTime > climbs[j + 1].SendTime)
                        {
                            temp = climbs[j + 1].SendTime;
                            climbs[j + 1].SendTime = climbs[j].SendTime;
                            climbs[j].SendTime = temp;
                        }
                    }
                }

                return climbs[0].SendTime;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new double();
        }

        //Get List of speed times
        public async Task<List<double>> GetAllTImes()
        {
            try
            {
                List<double> times = new List<double>();
                var climbs = await conn.Table<SpeedClimb>().ToListAsync();

                foreach(SpeedClimb c in climbs)
                {
                    times.Add(c.SendTime);
                }

                return times;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<double>();
        }

        //Add climb
        public async Task AddSpeedClimb(double time)
        {
            int result = 0;

            var climb = new SpeedClimb
            {
                SendDate = DateTime.Today,
                SendTime = time
            };

            try
            {
                if (ClimbValidation(climb))
                {
                    result = await conn.InsertAsync(climb);

                    StatusMessage = string.Format("{0} record(s) added \n[SendDate: {1},\nTime: {2}]", result, climb.SendDate, climb.SendTime);
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
        public async Task EditSpeedClimb(int id, double time)
        {
            try
            {
                var climb = await conn.Table<SpeedClimb>().FirstOrDefaultAsync(x => x.Id == id);
                climb.SendTime = time;

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