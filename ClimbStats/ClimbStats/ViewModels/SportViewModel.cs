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

        //private List<string> _grades;
        //_grades = new List<string>(){
        //        "5.5" ,"5.6" ,"5.7" ,"5.8" ,"5.9" ,
        //        "5.10a" , "5.10b" ,"5.10c" ,"5.10d" ,
        //         "5.11a" ,"5.11b" ,"5.11c" ,"5.11d" ,
        //         "5.12a" ,"5.12b" ,"5.12c" ,"5.12d" ,
        //         "5.13a" ,"5.13b" ,"5.13c" ,"5.13d" ,
        //         "5.14a" ,"5.14b" ,"5.14c" ,"5.14d" ,
        //         "5.15a" ,"5.15b" ,"5.15c" ,"5.15d"
        //    };


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
        public async Task AddSportClimb(int numAttempts, string grade, bool isOutdoors)
        {
            int result = 0;

            var climb = new SportClimb
            {
                SendDate = DateTime.Today,
                NumAttempts = numAttempts,
                Grade = grade,
                IsOutdoors = isOutdoors
            };

            try
            {
                if (ClimbValidation(climb))
                {
                    result = await conn.InsertAsync(climb);

                    StatusMessage = string.Format("{0} record(s) added \n[SendDate: {1},\nNumAttempts: {2},\nGrade: {3},\nIsOutdoors: {4}]", result, climb.SendDate, climb.NumAttempts, climb.Grade, climb.IsOutdoors);
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