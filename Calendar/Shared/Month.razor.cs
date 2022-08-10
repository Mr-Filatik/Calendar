using Calendar.Data;

namespace Calendar.Shared
{
    public class MonthInformation
    {
        public static JobInformation? SelectJobInformation = null;
        public static DayInformation? OldDayInformation = null;
        public static DayInformation? NewDayInformation = null;
        public int Year { get; private set; }
        public int Month { get; private set; }
        public DayInformation[,] Weeks { get; private set; }
        public string MonthString => Month switch
        {
            1 => "Январь",
            2 => "Февраль",
            3 => "Март",
            4 => "Апрель",
            5 => "Май",
            6 => "Июнь",
            7 => "Июль",
            8 => "Август",
            9 => "Сентябрь",
            10 => "Октябрь",
            11 => "Ноябрь",
            12 => "Декабрь",
            _ => "Нет данных"
        };
        public MonthInformation(DateTime dateTime)
        {
            Year = dateTime.Year;
            Month = dateTime.Month;
            ChangeWeekCount();
        }
        public void ChangeWeekCount()
        {
            decimal weeksCount = Math.Ceiling((decimal)(DateTime.DaysInMonth(Year, Month) - new DateTime(Year, Month, 1).DayOfWeek switch
            {
                DayOfWeek.Monday => 7,
                DayOfWeek.Tuesday => 6,
                DayOfWeek.Wednesday => 5,
                DayOfWeek.Thursday => 4,
                DayOfWeek.Friday => 3,
                DayOfWeek.Saturday => 2,
                DayOfWeek.Sunday => 1,
                _ => 0
            }) / 7) + 1;
            Weeks = new DayInformation[(int)weeksCount, 7];
            int startDayOfWeek = (int)(new DateTime(Year, Month, 1).DayOfWeek - 1);
            if (startDayOfWeek < 0)
            {
                startDayOfWeek = 6;
            }
            for (int i = 0, number = 1; i < Weeks.GetLength(0) * 7; i++)
            {
                Weeks[i / 7, i % 7] = new DayInformation();
                Weeks[i / 7, i % 7].Year = Year;
                Weeks[i / 7, i % 7].DayOfWeek = (DayOfWeek)(i % 7 == 6 ? 0 : i % 7 + 1);
                if (i < startDayOfWeek)
                {
                    Weeks[i / 7, i % 7].Number = DateTime.DaysInMonth(Month > 1 ? Year : Year - 1, Month > 1 ? Month - 1 : 12) - startDayOfWeek + 1 + i;
                    Weeks[i / 7, i % 7].Month = (Month - 1) < 1 ? 12 : Month - 1;
                }
                if (i > DateTime.DaysInMonth(Year, Month) + startDayOfWeek - 1)
                {
                    Weeks[i / 7, i % 7].Number = i - (DateTime.DaysInMonth(Year, Month) + startDayOfWeek - 1);
                    Weeks[i / 7, i % 7].Month = (Month + 1) > 12 ? 1 : Month + 1;
                }
                if (i >= startDayOfWeek && i <= DateTime.DaysInMonth(Year, Month) + startDayOfWeek - 1)
                {
                    Weeks[i / 7, i % 7].Number = number;
                    number++;
                    Weeks[i / 7, i % 7].Month = Month;
                }
                Weeks[i / 7, i % 7].Jobs = new List<JobInformation>();
                foreach (var item in DataController.Instance.Jobs)
                {
                    if (item.Year == Weeks[i / 7, i % 7].Year && item.Month == Weeks[i / 7, i % 7].Month && item.Number == Weeks[i / 7, i % 7].Number)
                    {
                        Weeks[i / 7, i % 7].Jobs.Add(item);
                    }
                }
            }
        }
        public void ChangeMonth(bool isNext)
        {
            if (isNext)
            {
                if (Month < 12)
                {
                    Month++;
                    ChangeWeekCount();
                }
                else
                {
                    bool flag = ChangeYear(true);
                    if (flag)
                    {
                        Month = 1;
                        ChangeWeekCount();
                    }
                }
            }
            else
            {
                if (Month > 1)
                {
                    Month--;
                    ChangeWeekCount();
                }
                else
                {
                    bool flag = ChangeYear(false);
                    if (flag)
                    {
                        Month = 12;
                        ChangeWeekCount();
                    }
                }
            }
        }
        public bool ChangeYear(bool isNext)
        {
            if (isNext)
            {
                if (Year < 2030)
                {
                    Year++;
                    return true;
                }
            }
            else
            {
                if (Year > 2020)
                {
                    Year--;
                    return true;
                }
            }
            return false;
        }
        public void ChangeMonth()
        {
            DateTime dateTime = DateTime.Now;
            Year = dateTime.Year;
            Month = dateTime.Month;
            ChangeWeekCount();
        }
    }
}
