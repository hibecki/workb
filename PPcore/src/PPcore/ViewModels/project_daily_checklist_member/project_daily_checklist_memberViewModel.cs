using System.ComponentModel.DataAnnotations;


namespace PPcore.ViewModels.project_daily_checklist_member
{
    public class project_daily_checklist_memberViewModel
    {
        public Models.member member { get; set; }
        [Display(Name = "เช็คชื่อเข้าอบรม")]
        public string attended { get; set; }
        public int course_day { get; set; }
    }
}
