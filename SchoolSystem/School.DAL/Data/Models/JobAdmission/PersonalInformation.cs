using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolSystem.School.DAL.Data.Models.Career
{
    public class PersonalInformation
    {
        [Key]
        public int Id { get; set; }
        public string FullNameArabic { get; set; }
        public string FullNameEnglish { get; set; }
        public string NationalIDNumber { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string MaritalStatus { get; set; }
        public string? MilitaryService { get; set; }
        public string? CityCenter { get; set; }
        public string DetailedAddress { get; set; }
        public string MobileNumber { get; set; }
        public string AdditionalMobileNumber { get; set; }

        //
        public string MajorQualification { get; set; }
        public string Specialization { get; set; }
        public int GraduationYear { get; set; }
        public string University { get; set; }

        //
        public string PreviousEmployer { get; set; }
        public string JobTitle { get; set; }
        public decimal MonthlySalary { get; set; }
        public string ReasonForLeaving { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int YearsOfExperience { get; set; }
        public string CV { get; set; }
        //


        //Navigation
        [ForeignKey("joddataid")]
        public int JobDataid { get; set; }
        public virtual JobData jobData { get; set; }


    }
}
