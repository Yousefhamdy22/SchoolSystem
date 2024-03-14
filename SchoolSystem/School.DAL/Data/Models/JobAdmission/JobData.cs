using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.School.DAL.Data.Models.Career
{
    public class JobData
    {
        [Key]
        public int Id { get; set; }
        public string JobArea { get; set; }
        public bool SocialInsurance { get; set; }
        public bool Declaration { get; set; }

        //nav
        public ICollection<PersonalInformation> personalInformation { get; set; }



    }
}
