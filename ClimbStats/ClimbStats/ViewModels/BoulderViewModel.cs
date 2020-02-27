using ClimbStats.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClimbStats.ViewModels
{
    public class BoulderViewModel : BaseViewModel
    {
        private SQLiteAsyncConnection conn;
        public string StatusMessage { get; set; }

        public BoulderViewModel(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Boulder>().Wait();

            Title = "Bouldering";
        }

        //Get all climbs
        public async Task<List<Boulder>> GetAllBoulders()
        {
            try
            {
                return await conn.Table<Boulder>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<Boulder>();
        }

        //Get one climb
        public async Task<Boulder> GetBoulder(int id)
        {
            try
            {
                return await conn.Table<Boulder>().FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new Boulder();
        }

        //Add climb
        public async Task AddBoulder(int numAttempts, string grade, bool isOutdoors)
        {
            int result = 0;

            var climb = new Boulder
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
        public async Task<int> DeleteBoulder(int id)
        {
            try
            {
                return await conn.DeleteAsync<Boulder>(id);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to delete data. {0}", ex.Message);
            }
            return -1;
        }

        //Edit Climb
        public async Task EditBoulder(int id, int numAttempts, string grade, bool isOutdoors)
        {
            try
            {
                var climb = await conn.Table<Boulder>().FirstOrDefaultAsync(x => x.Id == id);

                climb.Grade = grade;
                climb.IsOutdoors = isOutdoors;
                climb.NumAttempts = numAttempts;

                if (ClimbValidation(climb))
                {
                    await conn.UpdateAsync(climb);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to update {0}. Error: {1}", grade, ex.Message);
            }
        }

        //true if info is valid
        private bool ClimbValidation(Boulder climb)
        {
            return true;
        }
    }
}