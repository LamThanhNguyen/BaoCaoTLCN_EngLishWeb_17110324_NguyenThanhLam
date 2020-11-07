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
    public class GrammarController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public GrammarController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("create")]
        public async Task<ActionResult<Grammar>> CreateGrammar(Grammar grammar)
        {
            if (await _unitOfWork.GrammarRepository.GrammarExists(grammar.GrammarName))
            {
                return BadRequest("Đã tồn tại.");
            }

            grammar.GrammarName = grammar.GrammarName.ToLower();

            _unitOfWork.GrammarRepository.AddGrammarAsync(grammar);

            if (await _unitOfWork.Complete())
            {
                return Ok();
            }

            return BadRequest("Failed to add");
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpGet("getgrammars")]
        public async Task<ActionResult<IEnumerable<Grammar>>> GetGrammars([FromQuery] GrammarsParams grammarsParams)
        {
            var grammars = await _unitOfWork.GrammarRepository.GetGrammars(grammarsParams);

            Response.AddPaginationHeader(grammars.CurrentPage,
                grammars.PageSize, grammars.TotalCount, grammars.TotalPages);

            return Ok(grammars);
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpGet("getgrammarbyid/{Id}")]
        public async Task<ActionResult<Grammar>> GetGrammarById(int Id)
        {
            var grammar = await _unitOfWork.GrammarRepository.GetGrammarById(Id);

            if (grammar == null)
            {
                return BadRequest("Không tồn tại.");
            }

            return Ok(grammar);
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpGet("getgrammarbygrammarname/{grammarname}")]
        public async Task<ActionResult<Grammar>> GetGrammarByGrammarName(string grammarname)
        {
            var grammar = await _unitOfWork.GrammarRepository.GetGrammarByGrammarName(grammarname);

            if (grammar == null)
            {
                return NotFound();
            }

            return Ok(grammar);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateGrammar(Grammar grammar)
        {
            var grammarInDB = await _unitOfWork.GrammarRepository.GetGrammarById(grammar.Id);

            if (grammarInDB == null)
            {
                return BadRequest("Không tồn tại");
            }

            grammarInDB.GrammarName = grammar.GrammarName.ToLower();
            grammarInDB.Structure = grammar.Structure;
            grammarInDB.Description = grammar.Description;
            grammarInDB.Example = grammar.Example;

            _unitOfWork.GrammarRepository.UpdateGrammar(grammarInDB);

            if (await _unitOfWork.Complete())
            {
                return Ok();
            }

            return BadRequest("Failed to update grammar");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("delete/{Id}")]
        public async Task<ActionResult> DeleteGrammar(int Id)
        {
            var grammarInDB = await _unitOfWork.GrammarRepository.GetGrammarById(Id);

            if (grammarInDB == null)
            {
                return BadRequest("Không tồn tại");
            }

            _unitOfWork.GrammarRepository.DeleteGrammar(grammarInDB);

            if (await _unitOfWork.Complete())
            {
                return Ok();
            }

            return BadRequest("Failed to delete grammar");
        }
    }
}
