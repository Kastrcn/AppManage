using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppManage.Model
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public virtual ICollection<App> App { get; set; }
        public Project()
        {
            this.App = new HashSet<App>();
        }

    }
}
