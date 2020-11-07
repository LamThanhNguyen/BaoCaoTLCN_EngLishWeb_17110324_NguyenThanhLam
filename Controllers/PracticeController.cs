using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_HOCTIENGANH.Entities;
using WEB_HOCTIENGANH.Extensions;
using WEB_HOCTIENGANH.Helpers;
using WEB_HOCTIENGANH.Interfaces;

namespace WEB_HOCTIENGANH.Controllers
{
    public class PracticeController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public PracticeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("create")]
        public async Task<ActionResult<Practice>> CreatePractice(Practice practice)
        {
            if (await _unitOfWork.PracticeRepository.PracticeExists(practice.Question))
            {
                return BadRequest("Đã tồn tại.");
            }

            practice.PracticeName = practice.PracticeName.ToLower();

            _unitOfWork.PracticeRepository.AddPracticeAsync(practice);

            if (await _unitOfWork.Complete())
            {
                return Ok();
            }

            return BadRequest("Failed to add.");
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpGet("getpracbypracname")]
        public async Task<ActionResult<IEnumerable<Practice>>> GetPracticesByPracName([FromQuery]PracticesParams practicesParams)
        {
            var practices = await _unitOfWork.PracticeRepository.GetPracticesByPracName(practicesParams);

            Response.AddPaginationHeader(practices.CurrentPage,
                practices.PageSize, practices.TotalCount, practices.TotalPages);

            return Ok(practices);
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpGet("getpracticesname")]
        public async Task<ActionResult<IEnumerable<string>>> GetPracticesName()
        {
            var practicesName = await _unitOfWork.PracticeRepository.GetPracticesName();

            return Ok(practicesName);
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpGet("getpracticebyid/{Id}")]
        public async Task<ActionResult<Practice>> GetPracticeById(int Id)
        {
            var practice = await _unitOfWork.PracticeRepository.GetPracticeById(Id);

            if (practice == null)
            {
                return BadRequest("Không tồn tại");
            }

            return Ok(practice);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdatePractice(Practice practice)
        {
            var practiceInDB = await _unitOfWork.PracticeRepository.GetPracticeById(practice.Id);

            if (practiceInDB == null)
            {
                return BadRequest("Không tồn tại.");
            }

            practiceInDB.PracticeName = practice.PracticeName.ToLower();
            practiceInDB.Question = practice.Question;
            practiceInDB.AnswerA = practice.AnswerA;
            practiceInDB.AnswerB = practice.AnswerB;
            practiceInDB.AnswerC = practice.AnswerC;
            practiceInDB.AnswerD = practice.AnswerD;
            practiceInDB.Answer = practice.Answer;

            _unitOfWork.PracticeRepository.UpdatePractice(practiceInDB);

            if (await _unitOfWork.Complete())
            {
                return Ok();
            }

            return BadRequest("Failed to update practice");

        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("delete/{Id}")]
        public async Task<ActionResult> DeletePractice(int Id)
        {
            var practiceInDB = await _unitOfWork.PracticeRepository.GetPracticeById(Id);

            if (practiceInDB == null)
            {
                return BadRequest("Không tồn tại.");
            }

            _unitOfWork.PracticeRepository.DeletePractice(practiceInDB);

            if (await _unitOfWork.Complete())
            {
                return Ok();
            }

            return BadRequest("Failed to delete practice");
        }
    }
}
