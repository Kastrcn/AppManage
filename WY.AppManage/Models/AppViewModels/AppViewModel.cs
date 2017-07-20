using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WY.AppManage.Models
{
    public class AppViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string FileUrl { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
