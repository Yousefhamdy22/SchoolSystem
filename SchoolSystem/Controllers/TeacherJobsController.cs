using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.DTOs;
using SchoolSystem.School.DAL.Data.Models.Students;
using SchoolSystem.School.DAL.Data.Models.TeachersAdmission;
using SchoolSystem.School.DAL.GenaricRepo;

/*
namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherJobsController : ControllerBase
    {

        private readonly IGenaricRepo<DAJob> _JobRepository;
         private readonly IMapper _mapper;


        public TeacherJobsController(IGenaricRepo<DAJob> JobRepositor , IMapper mapper)
        {
            _JobRepository = JobRepositor;
            _mapper = mapper;
        }

        #region GetAll
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobs = await _JobRepository.GetAll();

            return Ok(new ResponseModel<DAJob>
            {
                Success = true,
                Message = "success",
                Data = jobs.ToList()
            });
        }
        #endregion


            #region AddJob

        [HttpPost("Job")]
        public async Task<IActionResult> AddJob([FromBody] DAjobDto jobDataDto)
        {
            try
            {
                if (jobDataDto == null)
                {
                    return BadRequest(new ResponseModel<string>
                    {
                        Success = false,
                        Message = "jobData object is null",
                        Data = null
                    });
                }
                var job = new DAJob
                {
                    Division = jobDataDto.Division,
                    JobArea = jobDataDto.JobArea,
                    experienceYears = jobDataDto.experienceYears

                };

            var Reponse = await _JobRepository.InsertAsync(job);
                return Ok(Reponse);
            }
            catch
            {
                // Log the exception details
                return BadRequest(new ResponseModel<string>
                {
                    Status = 400 ,
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Data = null 
                });
            }
        }

        #endregion

        #region UpdateTJob
        [HttpPut("job/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] DAjobDto TjobData)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    throw new Exception("Teacher Not Updated");
                }
                var existingjob = await _JobRepository.GetByIdAsync(id);
                if (existingjob == null)
                {
                    return NotFound(new ResponseModel<object>
                    {
                        Success = false,
                        Message = $"Student with ID {id} not found.",
                        Data = null
                    });
                }

                var updatedjob =  _mapper.Map<TeacherAdmissionDto>(TjobData);

                updatedjob.JobId = id;
                var Response = await _JobRepository.UpdateAsync(existingjob);
             //  await _JobRepository.SaveChangesAsync();
                
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


        //[HttpPut("job")]
        //public async Task<IActionResult> UpdateAsync(int id, [FromBody] DAJob jobdata)
        //{
        //    if (jobdata == null)
        //        return BadRequest("Student object is null");

        //    var item = await _JobRepository.GetByIdAsync(id);

        //    if (item == null)
        //        return NotFound();

        //    // item.NationalIDNumber = personal.NationalIDNumber;  //automaper 



        //    return Ok("Item Uodated Succ");
        //}

        #region Delete

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var item = await _JobRepository.GetByIdAsync(id);
                if (item == null)
                {
                    throw new Exception($"Job with ID {id} not found.");
                }

                var Response= await _JobRepository.DeleteAsync(id);
            //    await _JobRepository.SaveChangesAsync(); 

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

*/
