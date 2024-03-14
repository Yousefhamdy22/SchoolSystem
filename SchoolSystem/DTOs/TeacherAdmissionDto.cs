namespace SchoolSystem.DTOs
{
    public class TeacherAdmissionDto
    {
       
        public string FullNameArabic { get; set; }
        public string FullNameEnglish { get; set; }
        public string NationalIdNumber { get; set; }
        public string NationalIdIssuanceBody { get; set; }
        public DateTime NationalIdIssuanceDate { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string City { get; set; }
        public string DetailedAddress { get; set; }
        public string MobileNumber { get; set; }
        public string? AdditionalMobileNumber { get; set; }
        public bool HasSocialInsurance { get; set; }
        public string? MaritalStatus { get; set; }
        public string? MilitaryOrPublicServiceSituation { get; set; }


        // Educationallevel
        public string MajorQualification { get; set; }
        public string Specialization { get; set; }
        public int GraduationYear { get; set; }
        public string UniversityOrInstituteName { get; set; }

        public string? AdditionalQualification { get; set; }
        public string? AdditionalSpecialization { get; set; }

        //Language
        public string? LanguageName { get; set; }
        public string? Speaking { get; set; }
        public string? Reading { get; set; }
        public string? Writing { get; set; }

        //Course
        public string? CourseName { get; set; }
        public string? WhereFrom { get; set; }

        //workexper
        public string EmployerName { get; set; }
        public string JobTitle { get; set; }
        public decimal MonthlySalary { get; set; }
        public string ReasonForLeaving { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        //
        public bool RelativesWorkAtSchool { get; set; }
        public bool HasChronicDiseases { get; set; }
        public string? ReasonForChoosingSchool { get; set; }
        public IFormFile CVFile { get; set; } //IFormFile
        public int JobId { get; set; } // FK 

    }
}
