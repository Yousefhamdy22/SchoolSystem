
using Microsoft.AspNetCore.Cors;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.DTOs;
using SchoolSystem.School.DAL.Data.Models.Career;
using SchoolSystem.School.DAL.Data.Models.Students;
using SchoolSystem.School.DAL.GenaricRepo;
using Microsoft.AspNetCore.Authorization;
using ServiceStack.Text;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobAdmissionController : ControllerBase
    {
        private readonly IGenaricRepo<PersonalInformation> _repository;
        private readonly IGenaricRepo<JobData> _JobRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;

        public JobAdmissionController(IGenaricRepo<PersonalInformation> repository, IGenaricRepo<JobData> jobRepository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _JobRepository = jobRepository;
            _mapper = mapper;
            this.configuration = configuration;
        }

        #region GetAll
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var baseUrl = configuration["BaseUrl:Local"] as string;
                var PersonalAdmission = await _repository.GetAll();

                foreach (var item in PersonalAdmission)
                {
                    item.CV = string.IsNullOrEmpty(item.CV) ? string.Empty : baseUrl + item.CV;
                }

                return Ok(new ResponseModel<PersonalInformation>
                {
                    Status = 200,
                    Success = true,
                    Message = "success",
                    Data = PersonalAdmission.ToList()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<PersonalInformation>
                {
                    Status = 400,
                    Success = false,
                    Message = ex.Message,
                });
            }


        }
        #endregion

        #region GetById

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var student = await _repository.GetByIdAsync(id);

            if (student == null)
            {
                throw new Exception("Person Admission Not Found");
            }

            return Ok(student);
        }

        #endregion

        #region Add Admission

        [HttpPost("Admission")]
        public async Task<IActionResult> AddAsync([FromForm] PersonalAddmissionDto admissionDto)
        {
            try
            {
                if (admissionDto == null)
                {

                    throw new Exception("Person Null");
                }

                // Map StudentAdmissionDto to StudentAdmission
                var mapped = _mapper.Map<PersonalInformation>(admissionDto);

                var response = await _repository.InsertAsync(mapped);
                await _repository.SaveChangesAsync();

                if (admissionDto.CV != null && admissionDto.CV.Length > 0)
                {
                    var extension = Path.GetExtension(admissionDto.CV.FileName).ToLowerInvariant(); // 
                    var filePath = Path.Combine("wwwroot", "uploads", "jobs", $"{mapped.Id}{extension}");

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await admissionDto.CV.CopyToAsync(stream);
                    }

                    var relativePath = $"/wwwroot/uploads/jobs/{mapped.Id}{extension}";
                    mapped.CV = relativePath;
                    await _repository.SaveChangesAsync();
                }
                return Ok(response);
            }
            catch
            {
                // Log the exception details
                return BadRequest(new ResponseModel<StudentAdmission>
                {
                    Status = 400,
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Data = null
                });
            }

        }
        #endregion

        #region DownloadCv

        //[HttpGet("download-cv/{admissionId}")]
        //public async Task<IActionResult> DownloadCV(int admissionId)
        //{
        //    var admissionInfo = await _repository.GetByIdAsync(admissionId);
        //    if (admissionInfo == null || string.IsNullOrWhiteSpace(admissionInfo.CV))
        //    {
        //        return NotFound("CV not found.");
        //    }

        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", admissionInfo.CV);
        //    if (!System.IO.File.Exists(path))
        //    {
        //        return NotFound("File does not exist.");
        //    }

        //    var memory = new MemoryStream();
        //    using (var stream = new FileStream(path, FileMode.Open))
        //    {
        //        await stream.CopyToAsync(memory);
        //    }
        //    memory.Position = 0;

        //    return File(memory, GetContentType(path), Path.GetFileName(path));
        //}

        //private string GetContentType(string path)
        //{
        //    // This method should return the MIME type based on the file extension
        //    // Example implementation not shown for brevity
        //    return "application/octet-stream";
        //}

        #endregion

        #region UpdateAdmission

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] PersonalAddmissionDto updatajobad)
        {
            try
            {
                if (updatajobad == null)
                {
                    throw new Exception("Persone Admission Is Null");
                }

                var item = await _repository.GetByIdAsync(id); 

                if (item == null)
                {
                    throw new Exception("Updated Not Done.");
                }

                var existingUpdate = await _repository.GetByIdAsync(id);
                if (existingUpdate == null)
                {
                    throw new Exception("Not Found ");
                }
                //if(!string.IsNullOrEmpty(personal.CityCenter) && personal.CityCenter != item.CityCenter)
                //    item.CityCenter = personal.CityCenter;
                //if (!string.IsNullOrEmpty(personal.CityCenter) && personal.CityCenter != item.CityCenter)
                //    item.CityCenter = personal.CityCenter;
                //var updatedJodad = _mapper.Map<PersonalInformation>(updatajobad);//updatedJodad.Id = id;

                _mapper.Map(updatajobad, existingUpdate);
              await _repository.UpdateAsync(existingUpdate); 
               // await _repository.SaveChangesAsync();

                var Response = await _repository.UpdateAsync(item); 
             
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<PersonalInformation>
                {
                    Status =400,
                    Success = false,
                    Message = "An error occurred while updating the Person Admission.",
                    Data = null
                });
            }

        }


        #endregion


        #region Delete

        [HttpDelete("{id}")]
        public  async Task< IActionResult> Delete(int id)
        {
            try
            {

                if (id == null)
                {
                    throw new Exception("Person Admission Is Null");
                }

              await  _repository.DeleteAsync(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(new ResponseModel<object>
                {
                    Status = 400,
                    Success = false,
                    Message =ex.Message ,
                    Data = null
                });
            }
        }
        #endregion

       
    }
}
