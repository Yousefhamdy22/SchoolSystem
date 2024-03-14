
using SchoolSystem.Helpers;
using System.ComponentModel.DataAnnotations;

namespace SchoolSystem.School.DAL.Data.Models.Students
{
    public class StudentAdmission
    {
        [Key]

        public int StudentAdmissionId { get; set; }
        public string FullNameArabic { get; set; } // Full Name in Arabic
        public string FullNameEnglish { get; set; } // Full Name in English
        public string Gender { get; set; }
        public string? StudentNationalId { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Religion { get; set; }
        public string City { get; set; }
        public string? DetailedAddress { get; set; }
        public string? MobileNumber { get; set; }
        public string? AdditionalMobileNumber { get; set; }
        public string? EducationalSystem { get; set; }
        public string SchoolStage { get; set; }
        //
        public string FatherFullNameArabic { get; set; }
        public string FatherFullNameEnglish { get; set; }
        public string FatherNationalId { get; set; }
        public string FatherQualification { get; set; }
        public string FatherJob { get; set; }
        public string FatherMobileNumber { get; set; }
        //
        public string MotherFullNameArabic { get; set; }
        public string MotherFullNameEnglish { get; set; }
        public string MotherNationalId { get; set; }
        public string MotherQualification { get; set; }
        public string MotherJob { get; set; }
        public string MotherMobileNumber { get; set; }

        //
        public bool HasRelativeStudentsInSchool { get; set; }
        public bool HasRelativeWorkingInSchool { get; set; }
        public bool HasChronicDiseases { get; set; }
        public string? PreviousSchoolAttended { get; set; }
        public string? PaymentOfLastTuitionFees { get; set; }
        public string? ReasonForChoosingSchool { get; set; }
        public string? SecondLanguage { get; set; }
        public string? LivingWith { get; set; }
        public bool UseSchoolBus { get; set; }
        public string? BusWaitingLocation { get; set; }

        public string AdmissionCode { get; set; } = RandomCodeGenerator.GenerateCode(8);




    }
}
