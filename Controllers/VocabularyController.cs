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
    public class VocabularyController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public VocabularyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("create")]
        public async Task<ActionResult<Vocabulary>> CreateVocabulary(Vocabulary vocabulary)
        {
            if (await _unitOfWork.VocabularyRepository.VocabularyExists(vocabulary.EngName))
            {
                return BadRequest("Đã tồn tại.");
            }

            vocabulary.EngName = vocabulary.EngName.ToLower();
            vocabulary.VietName = vocabulary.VietName.ToLower();
            

            _unitOfWork.VocabularyRepository.AddVocabularyAsync(vocabulary);

            if (await _unitOfWork.Complete())
            {
                return Ok();
            }

            return BadRequest("Failed to add.");
        }


        [HttpGet("getvocabularies")]
        public async Task<ActionResult<IEnumerable<Vocabulary>>> GetVocabularies([FromQuery] VocabulariesParams vocabulariesParams)
        {
            var vocabularies = await _unitOfWork.VocabularyRepository.GetVocabularies(vocabulariesParams);

            Response.AddPaginationHeader(vocabularies.CurrentPage,
                vocabularies.PageSize, vocabularies.TotalCount, vocabularies.TotalPages);

            return Ok(vocabularies);
        }

        [HttpGet("getvocabularybyid/{Id}")]
        public async Task<ActionResult<Vocabulary>> GetVocabularyById(int Id)
        {
            var vocabulary = await _unitOfWork.VocabularyRepository.GetVocabularyById(Id);

            if (vocabulary == null)
            {
                return BadRequest("Không tồn tại.");
            }

            return Ok(vocabulary);
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpGet("getvocabularybyengname/{engname}")]
        public async Task<ActionResult<Vocabulary>> GetVocabularyByEngName(string engname)
        {
            var vocabulary = await _unitOfWork.VocabularyRepository.GetVocabularyByEngName(engname);

            if (vocabulary == null)
            {
                return NotFound();
            }

            return Ok(vocabulary);
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpGet("getvocabularybyvietname/{vietname}")]
        public async Task<ActionResult<Vocabulary>> GetVocabularyByVietName(string vietname)
        {
            var vocabulary = await _unitOfWork.VocabularyRepository.GetVocabularyByVietName(vietname);

            if (vocabulary == null)
            {
                return NotFound();
            }

            return Ok(vocabulary);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateVocabulary(Vocabulary vocabulary)
        {
            var vocabularyInDB = await _unitOfWork.VocabularyRepository.GetVocabularyById(vocabulary.Id);

            if (vocabularyInDB == null)
            {
                return BadRequest("Không tồn tại");
            }

            vocabularyInDB.VietName = vocabulary.VietName.ToLower();
            vocabularyInDB.EngName = vocabulary.EngName.ToLower();
            vocabularyInDB.Description = vocabulary.Description;
            vocabularyInDB.Image = vocabulary.Image;

            _unitOfWork.VocabularyRepository.UpdateVocabulary(vocabularyInDB);

            if (await _unitOfWork.Complete())
            {
                return Ok();
            }

            return BadRequest("Failed to update vocabulary");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("delete/{Id}")]
        public async Task<ActionResult> DeleteVocabulary(int Id)
        {
            var vocabularyInDB = await _unitOfWork.VocabularyRepository.GetVocabularyById(Id);

            if (vocabularyInDB == null)
            {
                return BadRequest("Không tồn tại.");
            }

            _unitOfWork.VocabularyRepository.DeleteVocabulary(vocabularyInDB);

            if (await _unitOfWork.Complete())
            {
                return Ok();
            }

            return BadRequest("Failed to delete vocabulary");
        }
    }
}
