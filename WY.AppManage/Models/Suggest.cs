using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WY.AppManage.Models
{
    public class Suggest
    {
        public int Id { get; set; }
        public string PhoneModel { get; set; }

        public string Location { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }


    }
}
