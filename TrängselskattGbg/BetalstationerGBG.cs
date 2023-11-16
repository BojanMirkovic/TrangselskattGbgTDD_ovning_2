
using System;

namespace TrängselskattGbg
{
    public class BetalstationerGBG
    {
        //Set the max possible amount per day
        private readonly int maxTotalBelopp = 60;
        //totalBelopp is holding total amount to be payed, initialize to 0
        int totalBelopp = 0;

        //List of all amounts per one day
        List<int> totalBelopps = new List<int>();

        
        public int RäknaTotalBelopp(string carCheckedInDateTime)
        {
            

            // Split the string by comma to get individual date-time strings and save them in arrey
            string[] individualTimes = carCheckedInDateTime.Split(',');

           

            // Define the range for the event
            TimeSpan startTimeInterval1 = new TimeSpan(06, 00, 0);
            TimeSpan startTimeInterval2 = new TimeSpan(06, 30, 0);
            TimeSpan startTimeInterval3 = new TimeSpan(07, 00, 0);
            TimeSpan startTimeInterval4 = new TimeSpan(08, 00, 0);
            TimeSpan startTimeInterval5 = new TimeSpan(08, 30, 0);
            TimeSpan startTimeInterval6 = new TimeSpan(15, 00, 0);
            TimeSpan startTimeInterval7 = new TimeSpan(15, 30, 0);
            TimeSpan startTimeInterval8 = new TimeSpan(17, 00, 0);
            TimeSpan startTimeInterval9 = new TimeSpan(18, 00, 0);
            TimeSpan startTimeInterval10 = new TimeSpan(18, 30, 0);


           
           //Loop thrue arrey elements and extract necessary information like time of day,month,day of week
           for (int i = 0; i < individualTimes.Length; i++)
           {
                    // Parse the string into a DateTime object
                    DateTime dateTime = DateTime.ParseExact(individualTimes[i].Trim(), "yyyy-MM-dd HH:mm", null);
              
                    // Extract the time part as TimeSpan
                    TimeSpan carCheckedInHour = dateTime.TimeOfDay;

                    //Extract the date from dateTime
                    int carCheckedInDate = dateTime.Month;

                   //Extract day of week from dateTime
                   string carCheckedInDay = dateTime.DayOfWeek.ToString();

                 

                //Check for time of registration and set amountn for that paricular time interval
                    if ((carCheckedInHour >= startTimeInterval1 && carCheckedInHour < startTimeInterval2) ||
                        (carCheckedInHour >= startTimeInterval5 && carCheckedInHour < startTimeInterval6) ||
                        (carCheckedInHour >= startTimeInterval9 && carCheckedInHour < startTimeInterval10))
                    { totalBelopps.Add(8);}

                    else if ((carCheckedInHour >= startTimeInterval2 && carCheckedInHour < startTimeInterval3) ||
                        (carCheckedInHour >= startTimeInterval4 && carCheckedInHour < startTimeInterval5) ||
                        (carCheckedInHour >= startTimeInterval6 && carCheckedInHour < startTimeInterval7) ||
                        (carCheckedInHour >= startTimeInterval8 && carCheckedInHour < startTimeInterval9))
                    { totalBelopps.Add(13);  }

                    else if ((carCheckedInHour >= startTimeInterval3 && carCheckedInHour < startTimeInterval4) ||
                        (carCheckedInHour >= startTimeInterval7 && carCheckedInHour < startTimeInterval8))
                    { totalBelopps.Add(18);}

                    else { totalBelopps.Add(0); }

                //Recalculate all amounts and save data in totalBelopp
                totalBelopp = totalBelopps.Sum();

               
                //Set out of charge Saturday,Sunday and July
                if (carCheckedInDate==07 || carCheckedInDay == "Saturday" || carCheckedInDay == "Sunday" )
                    
                { totalBelopp = 0; }

               
           }

            // We are seting max time interval TimeSpan representing 60 minutes
            TimeSpan maxTimeDifference = TimeSpan.FromMinutes(60); 

            //Extract first and last CheckIn for that day
            DateTime firstCheckIn = DateTime.ParseExact(individualTimes.First().Trim(), "yyyy-MM-dd HH:mm", null);
            DateTime lastCheckIn = DateTime.ParseExact(individualTimes.Last().Trim(), "yyyy-MM-dd HH:mm", null);

            // Calculate the time difference between the first and last check-ins
            TimeSpan timeDifference = lastCheckIn - firstCheckIn;

            //  make sure that we have more than one registration
            if (individualTimes.Length>1)
            {
                //Check if the time difference is 60 minutes or less
                if (timeDifference <= maxTimeDifference)
                {
                    totalBelopp = totalBelopps.Max();

                    Console.WriteLine("The time difference between the first and last check-ins is 60 minutes or less.");
                }
                else
                {
                    /*If we have 3 entries in 60 minutes and one more after one hour, program is caculating as 4 separate entries
                    instead as: first3 = maxBelopp + entry4=belopp */
                    Console.WriteLine("The time difference between the first and last check-ins is greater than 60 minutes.");
                }
            }
            
            //Set max charge per day to 60kr
            if (totalBelopp > maxTotalBelopp) { totalBelopp = maxTotalBelopp; }


            //Clear the list,in case that in future we want to calculate totalBelop for all month/year
            totalBelopps.Clear();

            return totalBelopp;


        }

       
    }
}
