using ClimbStats.Models;
using ClimbStats.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClimbStats.ViewModels
{
    public class SportViewModel : BaseViewModel
    {
        private SQLiteAsyncConnection conn;
        public string StatusMessage { get; set; }

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
        public async Task AddSportClimb(int numAttempts, KeyValuePair<int,string> grade, bool isOutdoors)
        {
            int result = 0;

            var climb = new SportClimb
            {
                SendDate = DateTime.Today,
                NumAttempts = numAttempts,
                GradeText = grade.Value,
                GradeInt = grade.Key,
                IsOutdoors = isOutdoors
            };

            try
            {
                if (ClimbValidation(climb))
                {
                    result = await conn.InsertAsync(climb);

                    StatusMessage = string.Format("{0} record(s) added \n[SendDate: {1},\nNumAttempts: {2},\nGrade: {3},\nIsOutdoors: {4}]", result, climb.SendDate, climb.NumAttempts, climb.GradeText, climb.IsOutdoors);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", grade, ex.Message);
                Console.WriteLine(StatusMessage);
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
        public async Task EditSportClimb(int id, int numAttempts, KeyValuePair<int,string> grade, bool isOutdoors)
        {
            try
            {
                var climb = await conn.Table<SportClimb>().FirstOrDefaultAsync(x => x.Id == id);

                climb.GradeInt = grade.Key;
                climb.GradeText = grade.Value;
                climb.IsOutdoors = isOutdoors;
                climb.NumAttempts = numAttempts;

                if (ClimbValidation(climb))
                {
                    await conn.UpdateAsync(climb);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to update {0}. Error: {1}", grade.Value, ex.Message);
            }
        }

        //true if info is valid
        private bool ClimbValidation(SportClimb climb)
        {
            return true;
        }
    }
}