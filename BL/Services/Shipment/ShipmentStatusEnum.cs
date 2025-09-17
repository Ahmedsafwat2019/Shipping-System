﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public enum ShipmentStatusEnum
    {
        Deleted=0,
        Created=1,
        Approved=2,
        Reject =7,
        ReadyForShip=3,
        Shipped=4,
        Delivered=5,
        Returned=6
    }
}
