using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESKOBApi.Models.Notifications
{
    public class Subscription
    {
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public string Tag { get; set; }
    }
}
