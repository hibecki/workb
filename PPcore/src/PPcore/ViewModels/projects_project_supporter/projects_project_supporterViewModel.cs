using System.ComponentModel.DataAnnotations;


namespace PPcore.ViewModels.projects_project_supporter
{
    public class projects_project_supporterViewModel
    {
        public Models.project project { get; set; }

        [Display(Name = "งบประมาณ")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        //[DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal? support_budget { get; set; }
    }
}
