using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.DTOs;
using SchoolSystem.School.DAL.Data.Models.Career;
using SchoolSystem.School.DAL.GenaricRepo;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class JobsController : ControllerBase
    {
        // private readonly IGenaricRepo<JobData> _repository;
        private readonly IGenaricRepo<JobData> _JobRepository;
        private readonly IMapper _mapper;


        public JobsController(IGenaricRepo<JobData> JobRepositor , IMapper mapper )
        {
            _JobRepository = JobRepositor;
            _mapper = mapper;
        }


        #region GetAll
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobs = await _JobRepository.GetAll();
            return Ok(new ResponseModel<JobData>
            {
                Status = 200,
                Success = true,
                Message = "success",
                Data = jobs.ToList()
            });
        }
        #endregion

        #region Add Jobs

        [HttpPost]
        public async Task<IActionResult> AddJob([FromBody] JobDto jobData)
        {
            try
            {
                if (jobData == null)
                {
                    throw new Exception("Job data object is null");
                }

                var job = new JobData
                {
                    Declaration = jobData.Declaration == 1,
                    JobArea = jobData.JobArea,
                    SocialInsurance = jobData.SocialInsurance == 1
                };
                var Response =await _JobRepository.InsertAsync(job);
                await _JobRepository.SaveChangesAsync();

                return Ok(Response);
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseModel<object>
                {
                    Status= 400,
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        #endregion

        #region Update Jobs


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] JobDto jobData)
        {
            try
            {
                if (jobData == null)
                {
                    throw new Exception("JobData is Null");

                }

                var item =await _JobRepository.GetByIdAsync(id);

                if (item == null)
                {
                    return NotFound(new ResponseModel<object>
                    {
                        Success = false,
                        Message = $"Job with ID {id} not found.",
                        Data = null
                    });
                }

                item.JobArea = jobData.JobArea;
                item.Declaration = jobData.Declaration == 1;
                item.SocialInsurance = jobData.SocialInsurance == 1;
               

               var Response = await _JobRepository.UpdateAsync(item);
             //   await _JobRepository.SaveChangesAsync();

                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<object>
                {
                    Status = 400,
                    Success = false,
                    Message = "An error occurred while updating the job.",
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
              

                var Response = await _JobRepository.DeleteAsync(id);
            //  await _JobRepository.SaveChangesAsync();

                return Ok(Response);
            }
            catch (Exception ex)
            {
               
                return BadRequest(new ResponseModel<object>
                {
                    Status=400,
                    Success = false,
                    Message = ex.Message,
                   Data = null 
                });
            }
        }


        #endregion


    }
}
