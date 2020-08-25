using ClimbStats.Models;
using SQLite;
using System;
using System.Collections.Generic;
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

        //Get Climb Grades as int for graph
        public async Task<List<int>> GetAllClimbInt()
        {
            List<int> climbGrades = new List<int>();
            try
            {
                var temp = await conn.Table<Boulder>().ToListAsync();

                foreach (Boulder c in temp)
                {
                    climbGrades.Add(c.GradeInt);
                }

                return climbGrades;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<int>();
        }

        //Get Climb Grades as string for graph
        public async Task<List<string>> GetAllClimbText()
        {
            List<string> climbGrades = new List<string>();
            try
            {
                var temp = await conn.Table<Boulder>().ToListAsync();

                foreach (Boulder c in temp)
                {
                    climbGrades.Add(c.GradeText);
                }

                return climbGrades;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<string>();
        }

        //Get average number of attempts per climbing grade
        //TODO: MAKE MORE EFFICIENT!!!
        public async Task<List<double>> GetAvgNumAttempts()
        {
            List<double> averages = new List<double>();

            List<string> distinctClimbs = new List<string>();
            Dictionary<string, int> totals = new Dictionary<string, int>();
            Dictionary<string, int> counts = new Dictionary<string, int>();

            try
            {
                var temp = await conn.Table<Boulder>().ToListAsync();

                foreach (Boulder c in temp)
                {
                    if (distinctClimbs.Contains(c.GradeText))
                    {
                        totals[c.GradeText] = totals[c.GradeText] += c.NumAttempts;
                        counts[c.GradeText]++;
                    }
                    else
                    {
                        distinctClimbs.Add(c.GradeText);
                        totals.Add(c.GradeText, c.NumAttempts);
                        counts.Add(c.GradeText, 1);
                    }
                }

                foreach (var t in totals)
                {
                    var total = t.Value;
                    var count = counts[t.Key];
                    averages.Add((double)total / (double)count);
                }

                return averages;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<double>();
        }

        //Get Climb Grades as int for graph
        public async Task<List<string>> GetGradesClimbed()
        {
            List<string> DistinctGrades = new List<string>();
            HashSet<string> gradesClimbed = new HashSet<string>();
            try
            {
                var temp = await conn.Table<Boulder>().ToListAsync();

                foreach (Boulder c in temp)
                {
                    gradesClimbed.Add(c.GradeText);
                }

                foreach (string g in gradesClimbed)
                {
                    DistinctGrades.Add(g);
                }

                return DistinctGrades;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<string>();
        }

        //Get number of climbs sent at each grade
        //make more effiecent
        public async Task<List<int>> GetGradeCount()
        {

            List<int> numClimbed = new List<int>();

            List<string> distinctClimbs = new List<string>();
            Dictionary<string, int> counts = new Dictionary<string, int>();
            try
            {
                var temp = await conn.Table<Boulder>().ToListAsync();

                foreach (Boulder c in temp)
                {
                    if (distinctClimbs.Contains(c.GradeText))
                    {
                        counts[c.GradeText]++;
                    }
                    else
                    {
                        distinctClimbs.Add(c.GradeText);
                        counts.Add(c.GradeText, 1);
                    }
                }

                foreach (var c in counts)
                {
                    numClimbed.Add(c.Value);
                }

                return numClimbed;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<int>();
        }

        //Add climb
        public async Task AddBoulder(int numAttempts, KeyValuePair<int, string> grade, bool isOutdoors)
        {
            var climb = new Boulder
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
                    var result = await conn.InsertAsync(climb);

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
        public async Task EditBoulder(int id, int numAttempts, KeyValuePair<int, string> grade, bool isOutdoors)
        {
            try
            {
                var climb = await conn.Table<Boulder>().FirstOrDefaultAsync(x => x.Id == id);

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