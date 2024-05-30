using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDP224A1.Models
{
    public class ConcertBaseViewModel : ConcertAddViewModel
    {
        [Key]
        public int ConcertId { get; set; }

        public string DaysToGo
        {
            get
            {
                var dtNow = DateTime.Now.Date;
                
                if (ConcertDate < dtNow)
                {
                    return "No longer available";
                }
                else
                {
                    var days = Math.Floor((ConcertDate - dtNow).TotalDays);
                    
                    if (days == 1.0)
                    {
                        return "Tomorrow";
                    }
                    else
                    {
                        return $"{days:n0} days to go";
                    }
                }
            }
        }
    }
}