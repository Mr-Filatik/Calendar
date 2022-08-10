namespace Calendar.Shared
{
    public class JobInformation
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Number { get; set; }
        public int? Hour { get; set; } = null;
        public int? Minute { get; set; } = null;
        public string Text { get; set; } = "";
    }
}
