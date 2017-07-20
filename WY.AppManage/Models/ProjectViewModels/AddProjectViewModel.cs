using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WY.AppManage.Models.ProjectViewModels
{
    public class AddProjectViewModel
    {
        [Required]
        public string Name { get; set; }
    
    }
}
