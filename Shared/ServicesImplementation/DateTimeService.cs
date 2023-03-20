using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ServicesImplementation
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime DateTimeNowUtc => DateTime.Now;
    }
}
