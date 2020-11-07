using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_HOCTIENGANH.Entities;
using WEB_HOCTIENGANH.Extensions;
using WEB_HOCTIENGANH.Helpers;
using WEB_HOCTIENGANH.Interfaces;

namespace WEB_HOCTIENGANH.Controllers
{
    public class QuestionReadingController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionReadingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("create")]
        public async Task<ActionResult<QuestionReading>> CreateQuestionReading(QuestionReading questionReading)
        {
            if (await _unitOfWork.QuestionReadingRepository.QuestionReadingExists(questionReading.ToeicNumber, questionReading.ToeicPart, questionReading.QuestionNumber))
            {
                return BadRequest("Đã tồn tại.");
            }

            _unitOfWork.QuestionReadingRepository.AddQuestionReadingAsync(questionReading);

            if (await _unitOfWork.Complete())
            {
                return Ok();
            }

            return BadRequest("Failed to add.");
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpGet("getquestionreadingbyid/{Id}")]
        public async Task<ActionResult<QuestionReading>> GetQuestionReadingById(int Id)
        {
            var questionReading = await _unitOfWork.QuestionReadingRepository.GetQuestionReadingById(Id);

            if (questionReading == null)
            {
                return BadRequest("Không tồn tại.");
            }

            return Ok(questionReading);
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpGet("getquestionreadingspart")]
        public async Task<ActionResult<IEnumerable<QuestionReading>>> GetQuestionReadingsPart([FromQuery] QuestionReadingParams questionReadingParams)
        {
            var questionReadings = await _unitOfWork.QuestionReadingRepository.GetQuestionReadingsPart(questionReadingParams);

            Response.AddPaginationHeader(questionReadings.CurrentPage,
                questionReadings.PageSize, questionReadings.TotalCount, questionReadings.TotalPages);

            return Ok(questionReadings);
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpGet("gettoeicnumbers")]
        public async Task<ActionResult<IEnumerable<string>>> GetToeicNumbers()
        {
            var toeicNumbers = await _unitOfWork.QuestionReadingRepository.GetToeicNumbers();

            return Ok(toeicNumbers);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateQuestionReading(QuestionReading questionReading)
        {
            var questionReadingInDB = await _unitOfWork.QuestionReadingRepository.GetQuestionReadingById(questionReading.Id);

            if (questionReadingInDB == null)
            {
                return BadRequest("Không tồn tại.");
            }

            questionReadingInDB.ToeicNumber = questionReading.ToeicNumber;
            questionReadingInDB.ToeicPart = questionReading.ToeicPart;
            questionReadingInDB.QuestionNumber = questionReading.QuestionNumber;
            questionReadingInDB.Question = questionReading.Question;
            questionReadingInDB.AnswerA = questionReading.AnswerA;
            questionReadingInDB.AnswerB = questionReading.AnswerB;
            questionReadingInDB.AnswerC = questionReading.AnswerC;
            questionReadingInDB.AnswerD = questionReading.AnswerD;
            questionReadingInDB.Answer = questionReading.Answer;

            _unitOfWork.QuestionReadingRepository.UpdateQuestionReading(questionReadingInDB);

            if (await _unitOfWork.Complete())
            {
                return Ok();
            }

            return BadRequest("Failed to update question reading.");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("delete/{ToeicNumber}")]
        public async Task<ActionResult> DeleteQuestionReadings(string ToeicNumber)
        {
            _unitOfWork.QuestionReadingRepository.DeleteQuestionReadings(ToeicNumber);

            if (await _unitOfWork.Complete())
            {
                return Ok();
            }

            return BadRequest("Failed to delete question reading.");
        }
    }
}
