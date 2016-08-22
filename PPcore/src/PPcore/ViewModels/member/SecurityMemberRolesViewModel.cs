using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PPcore.ViewModels.member
{
    public class SecurityMemberRolesViewModel
    {
        [Display(Name = "ชื่อผู้ใช้")]
        public string mem_username { get; set; }
        public Guid memberId { get; set; }

        [Display(Name = "วันที่สร้าง")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "สร้างโดย")]
        public string CreatedByUserName { get; set; }
        public Guid CreatedBy { get; set; }

        [Display(Name = "วันที่แก้ไข")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime EditedDate { get; set; }

        [Display(Name = "แก้ไขโดย")]
        public string EditedByUserName { get; set; }
        public Guid EditedBy { get; set; }
    }
}
