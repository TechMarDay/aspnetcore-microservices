using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Domains.Interfaces
{
    public interface IUserTracking
    {
        string CreatedBy { get; set; }

        string LastModifiedBy { get; set; }
    }
}
