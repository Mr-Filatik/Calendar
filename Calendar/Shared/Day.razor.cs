using Calendar.Data;

namespace Calendar.Shared
{
    public class DayInformation
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Number { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public List<JobInformation> Jobs { get; set; }

        public void AddJob(JobInformation job)
        {
            DataController.Instance.AddJob(job);
            Jobs.Add(job);
        }
        public void RemoveJob(JobInformation job)
        {
            DataController.Instance.RemoveJob(job);
            Jobs.Remove(job);
        }
    }
}
