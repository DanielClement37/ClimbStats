using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClimbStats.Models;
using SQLite;

namespace ClimbStats.ViewModels
{
    public class SportViewModel : BaseViewModel
    {
        private SQLiteAsyncConnection conn;
        public string StatusMessage { get; set; }

        public SportViewModel()
        {
            Title = "Sport Climbing";
        }

        public SportViewModel(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<SportClimb>().Wait();
            Title = "Sport Climbing";
        }

        //Get all climbs
        public async Task<List<SportClimb>> GetAllSportClimbs()
        {
            try
            {
                return await conn.Table<SportClimb>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<SportClimb>();
        }

        //Get one climb
        public async Task<SportClimb> GetSportClimb(int id)
        {
            try
            {
                return await conn.Table<SportClimb>().FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new SportClimb();
        }

        //Add climb
        public async Task AddSportClimb(SportClimb climb)
        {
            int result = 0;
            DateTime sendDate = DateTime.Today;
            try
            {
                if (ClimbValidation(climb))
                {
                    result = await conn.InsertAsync(new SportClimb
                    {
                        SendDate = sendDate,
                        NumAttempts = climb.NumAttempts,
                        Grade = climb.Grade,
                        IsOutdoors = climb.IsOutdoors
                    });

                    StatusMessage = string.Format("{0} record(s) added \n[SendDate: {1},\nNumAttempts: {2},\nGrade: {3},\nIsOutdoors: {4}]", result, sendDate, climb.NumAttempts, climb.Grade, climb.IsOutdoors);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", climb.Grade, ex.Message);
            }
        }

        //Delete one climb
        public async Task<int> DeleteClimb(int id)
        {
            try
            {
                return await conn.DeleteAsync<SportClimb>(id);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to delete data. {0}", ex.Message);
            }
            return -1;
        }

        //Edit Climb
        public async Task EditSportClimb(SportClimb climb)
        {
            try
            {
                if (ClimbValidation(climb))
                {
                    await conn.UpdateAsync(climb);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to update {0}. Error: {1}", climb.Grade, ex.Message);
            }
        }

        //true if info is valid
        private bool ClimbValidation(SportClimb climb)
        {
            return true;
        }
    }
}