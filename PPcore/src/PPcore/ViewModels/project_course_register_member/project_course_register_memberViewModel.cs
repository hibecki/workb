using System.ComponentModel.DataAnnotations;


namespace PPcore.ViewModels.project_course_register_member
{
    public class project_course_register_memberViewModel
    {
        public Models.member member { get; set; }
        [Display(Name = "ผลการอบรม")]
        public int course_grade { get; set; }
    }
}
