using Calendar.Shared;

namespace Calendar.Data
{
    public class DataController
    {
        public static DataController Instance { get; private set; }

        public List<JobInformation> Jobs { get; private set; }

        static DataController() {
            Instance = new DataController();
        }

        public DataController()
        {
            Jobs = new List<JobInformation>();
            Jobs.Add(new JobInformation() { Year = 2022, Month = 8, Number = 20, Text = "День рождения!" });
            Jobs.Add(new JobInformation() { Year = 2022, Month = 8, Number = 10, Text = "Позвонить Олегу", Hour = 16, Minute = 00 });
            Jobs.Add(new JobInformation() { Year = 2022, Month = 8, Number = 16, Text = "Тренировка", Hour = 8, Minute = 00 });
        }

        public void AddJob(JobInformation job)
        {
            Jobs.Add(job);
        }
        public void RemoveJob(JobInformation job)
        {
            Jobs.Remove(job);
        }
    }
}
