using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolSystem.DTOs;
using SchoolSystem.School.DAL.Data.Models.Students;
using SchoolSystem.School.DAL.GenaricRepo;
using System.Text.Json.Serialization;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IGenaricRepo<StudentAdmission> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentsController> _logger;
        public StudentsController(IGenaricRepo<StudentAdmission> repository, IMapper mapper,ILogger<StudentsController>logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
            _logger = logger;
        }

        #region GetAll

        [HttpGet]
        public async Task< IActionResult> GetAll()
        {
            var students = await _repository.GetAll();
            return Ok(new ResponseModel<StudentAdmission>
            {
                Status = 200,
                Success = true,
                Message = "success",
                Data = students.ToList()
            });
        }

        #endregion

        #region GetById

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var student = await _repository.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound(new ResponseModel<object>
                {
                    Success = false,
                    Message = "StudentAdmissionDto object is Not Found",
                    Status = 404 ,
                    Data = null
                });
            }

            return Ok(student);
        }
        #endregion


        #region Add Admission

        [HttpPost("Admission")]
        public async Task<IActionResult> AddAsync([FromBody] StudentAdmissionDto admissionDto)
        {
            try
            {
                if (admissionDto == null)
                {
                    
                    throw new Exception("Student Null");
                }

                // Map StudentAdmissionDto to StudentAdmission
                var admission = _mapper.Map<StudentAdmission>(admissionDto);

                var result =await _repository.InsertAsync(admission);
                await _repository.SaveChangesAsync();

                return Ok(result);

                


            }
            catch
            {
                // Log the exception details
                return BadRequest(new ResponseModel<StudentAdmission>
                {
                    Status = 400 ,
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Data = null 
                });
            }

        }

        #endregion

        // ? update

        #region Update Admission

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentAdmissionDto updatedStudentDto)
        {
            try
            {
                if (!ModelState.IsValid)
               {
                  
                    throw new Exception("Student Not Updated");
                }
                var existingStudent = await _repository.GetByIdAsync(id);
                if (existingStudent == null)
                {
                    return NotFound(new ResponseModel<object>
                    {
                        Success = false,
                        Message = $"Student with ID {id} not found.",
                       Data = null
                    });
                }
                
                var updatedStudent = _mapper.Map<StudentAdmission>(updatedStudentDto);

                updatedStudent.StudentAdmissionId = id;

                var Response =await _repository.UpdateAsync(existingStudent);
                 await _repository.SaveChangesAsync();

                return Ok(Response); 
            }
            catch
            {
                
                return StatusCode(500, new ResponseModel<object>
                {
                    Success = false,
                    Message = "Internal server error",
                    Data = null 
                });
            }
        }
        #endregion  // ?

        #region Delete

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
               
                var Response = await _repository.DeleteAsync(id);
               // await _repository.SaveChangesAsync();

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

