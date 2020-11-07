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
    public class ParagraphController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParagraphController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("create")]
        public async Task<ActionResult<Paragraph>> CreateParagraph(Paragraph paragraph)
        {
            if (await _unitOfWork.ParagraphRepository.ParagraphExists(paragraph.ToeicNumber, paragraph.ToeicPart, paragraph.QuestionNumber))
            {
                return BadRequest("Đã tồn tại.");
            }

            _unitOfWork.ParagraphRepository.AddParagraphAsync(paragraph);

            if (await _unitOfWork.Complete())
            {
                return Ok();
            }

            return BadRequest("Failed to add.");
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpGet("getparagraphbyid/{Id}")]
        public async Task<ActionResult<Paragraph>> GetParagraphById(int Id)
        {
            var paragraph = await _unitOfWork.ParagraphRepository.GetParagraphById(Id);

            if (paragraph == null)
            {
                return BadRequest("Không tồn tại.");
            }

            return Ok(paragraph);
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpGet("getparatoeicpart")]
        public async Task<ActionResult<IEnumerable<Paragraph>>> GetParagraphsToeicNumberPart([FromQuery] ParagraphParams paragraphParams)
        {
            var paragraphs = await _unitOfWork.ParagraphRepository.GetParagraphsToeicNumberPart(paragraphParams);

            Response.AddPaginationHeader(paragraphs.CurrentPage,
                paragraphs.PageSize, paragraphs.TotalCount, paragraphs.TotalPages);

            return Ok(paragraphs);
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpGet("getparaquesnumber/{ToeicNumber}/{ToeicPart}/{QuestionNumber}")]
        public async Task<ActionResult<Paragraph>> GetParagraphByQuestionNumber(string ToeicNumber, string ToeicPart, string QuestionNumber)
        {
            var paragraph = await _unitOfWork.ParagraphRepository.GetParagraphByQuestionNumber(ToeicNumber, ToeicPart, QuestionNumber);

            if (paragraph == null)
            {
                return BadRequest("Không tồn tại.");
            }

            return Ok(paragraph);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateParagraph(Paragraph paragraph)
        {
            var paragraphInDB = await _unitOfWork.ParagraphRepository.GetParagraphById(paragraph.Id);

            if (paragraphInDB == null)
            {
                return BadRequest("Không tồn tại.");
            }

            paragraphInDB.ToeicNumber = paragraph.ToeicNumber;
            paragraphInDB.ToeicPart = paragraph.ToeicPart;
            paragraphInDB.QuestionNumber = paragraph.QuestionNumber;
            paragraphInDB.ParagraphText = paragraph.ParagraphText;

            _unitOfWork.ParagraphRepository.UpdateParagraph(paragraphInDB);

            if (await _unitOfWork.Complete())
            {
                return Ok();
            }

            return BadRequest("Failed to update paragraph.");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("delete/{ToeicNumber}")]
        public async Task<ActionResult> DeleteParagraphs(string ToeicNumber)
        {
            _unitOfWork.ParagraphRepository.DeleteParagraphs(ToeicNumber);

            if (await _unitOfWork.Complete())
            {
                return Ok();
            }

            return BadRequest("Failed to delete paragraph.");
        }
    }
}
