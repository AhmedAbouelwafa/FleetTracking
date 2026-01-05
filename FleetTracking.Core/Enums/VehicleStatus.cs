using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetTracking.Core.Enums
{
    public enum VehicleStatus
    {
        Moving = 1,   // السرعة > 5 km/h
        Stopped = 2,  // السرعة = 0 لأقل من 5 دقائق
        Idle = 3,     // السرعة = 0 لأكتر من 5 دقائق
        Offline = 4   // مفيش تحديث من أكتر من 10 دقائق
    }
}
