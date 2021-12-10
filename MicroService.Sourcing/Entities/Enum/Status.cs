using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Sourcing.Entities.Enum
{
    public enum Status : byte
    {
        Active = 0,
        Closed = 1,
        Passive = 2
    }
}
