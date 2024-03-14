using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SchoolSystem.DTOs;
using SchoolSystem.School.DAL.Data.Models.Career;
using SchoolSystem.School.DAL.Data.Models.Students;
using SchoolSystem.School.DAL.Data.Models.TeachersAdmission;
using SchoolSystem.School.DAL.GenaricRepo;


namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly IGenaricRepo<TeacherInfoAdmission> _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public TeachersController(IGenaricRepo<TeacherInfoAdmission> repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        #region GetAll

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var baseUrl = _configuration["BaseUrl:Local"] as string;
                var TeacherAdmission = await _repository.GetAll();

                foreach (var item in TeacherAdmission)
                {
                    item.CVFilePath = string.IsNullOrEmpty(item.CVFilePath) ? string.Empty : baseUrl + item.CVFilePath;
                }

                return Ok(new ResponseModel<TeacherInfoAdmission>
                {
                    Status = 200,
                    Success = true,
                    Message = "success",
                    Data = TeacherAdmission.ToList()
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
            //try
            //{
            //    var baseUrl = _configuration["BaseUrl : local"] as string;
            //    var teachersAdmission = await _repository.GetAll();

            //    foreach (var item in teachersAdmission)
            //    {
            //        item.CVFilePath = string.IsNullOrEmpty(item.CVFilePath) ? string.Empty : baseUrl + item.CVFilePath;
            //    }
            //    return Ok(new ResponseModel<TeacherInfoAdmission>
            //    {
            //        Status = 200,
            //        Success = true,
            //        Message = "success",
            //        Data = teachersAdmission.ToList()
            //    });

            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new ResponseModel<PersonalInformation>
            //    {
            //        Status = 400,
            //        Success = false,
            //        Message = ex.Message,
            //    });
            //}
        }
        #endregion

        #region GetById

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var teachers = await _repository.GetByIdAsync(id);
                var Result = new List<object>
                {
                    teachers
                };

                if (teachers == null)
                {
                    throw new Exception("Teacher Admission NotFound");
                }


                return Ok(teachers);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<object>
                {
                    Status = 400,
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }

        }
        #endregion

        #region Add Admission


        [HttpPost("Admission")]
        public async Task<IActionResult> AddAsync([FromForm] TeacherAdmissionDto teacher)
        {
            try
            {
                if (teacher == null)
                {
                    return BadRequest(new ResponseModel<object>
                    {
                        Success = false,
                        Message = "Admission object is null",
                        Status = 400
                    });
                }

                var mapped = _mapper.Map<TeacherInfoAdmission>(teacher);

                var response = await _repository.InsertAsync(mapped);
                await _repository.SaveChangesAsync();

                if (teacher.CVFile != null && teacher.CVFile.Length > 0)
                {
                    var extension = Path.GetExtension(teacher.CVFile.FileName).ToLowerInvariant(); // 
                    var filePath = Path.Combine("wwwroot", "uploads", "teachers", $"{mapped.Id}{extension}");

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await teacher.CVFile.CopyToAsync(stream);
                    }

                    var relativePath = $"/wwwroot/uploads/teachers/{mapped.Id}{extension}";
                    mapped.CVFilePath = relativePath;
                    await _repository.SaveChangesAsync();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<object>
                {
                    Status = 400,
                    Success = false,
                    Message = $"An error occurred while processing your request: {ex.Message}",
                    Data = null
                });
            }

        }

            #endregion

            #region DownloadCv

            [HttpGet("download-cv/{admissionId}")]
            public async Task<IActionResult> DownloadCV(int admissionId)
            {
                var admissionInfo = await _repository.GetByIdAsync(admissionId);
                if (admissionInfo == null || string.IsNullOrWhiteSpace(admissionInfo.CVFilePath))
                {
                    return NotFound("CV not found.");
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", admissionInfo.CVFilePath);
                if (!System.IO.File.Exists(path))
                {
                    return NotFound("File does not exist.");
                }

                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

                return File(memory, GetContentType(path), Path.GetFileName(path));
            }

            private string GetContentType(string path)
            {
                // This method should return the MIME type based on the file extension
                // Example implementation not shown for brevity
                return "application/octet-stream";
            }
            #endregion

            #region Update admiddion

            [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] TeacherAdmissionDto updatedTeacherDto)
        {
            try
            {
                if (updatedTeacherDto == null)
                {
                    throw new Exception("Update Teacher Null");
                }

                var existingTeacher = await _repository.GetByIdAsync(id);
               
                if (existingTeacher == null)
                {
                    
                    throw new Exception($"Teacher with ID {id} not found.");

                }
               // var updatedTeacher = _mapper.Map<TeacherInfoAdmission>(updatedTeacherDto);
                existingTeacher.NationalIdNumber = updatedTeacherDto.NationalIdNumber;
               
                
                var Response = await _repository.UpdateAsync(existingTeacher);
           //      await _repository.SaveChangesAsync();

                return Ok(Response);
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseModel<object>
                {
                    Status = 400,
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
               
            }
        }


        #endregion

        #region Delete

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var existingItem =await _repository.GetByIdAsync(id);
                if (existingItem == null)
                {
                   
                    throw new Exception($"Item with ID {id} not found.");

                }

                var Response =await _repository.DeleteAsync(id);
                 await _repository.SaveChangesAsync();

                return Ok(Response);
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseModel<object>
                {
                    Status = 400,
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }


        #endregion

    }
}