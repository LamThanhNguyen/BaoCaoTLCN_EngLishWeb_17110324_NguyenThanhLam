using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_HOCTIENGANH.Entities;
using WEB_HOCTIENGANH.Helpers;
using WEB_HOCTIENGANH.Interfaces;

namespace WEB_HOCTIENGANH.Data
{
    public class ParagraphRepository : IParagraphRepository
    {
        private readonly DataContext _context;

        public ParagraphRepository(DataContext context)
        {
            _context = context;
        }
        public void AddParagraphAsync(Paragraph paragraph)
        {
            _context.Paragraphs.Add(paragraph);
        }

        public async void DeleteParagraphs(string ToeicNumber)
        {
            var paragraphs = await _context.Paragraphs
                                .Where(para => para.ToeicNumber == ToeicNumber)
                                .Select(para => para)
                                .ToListAsync();

            _context.Paragraphs.RemoveRange(paragraphs);
        }

        public async Task<Paragraph> GetParagraphById(int Id)
        {
            return await _context.Paragraphs.FindAsync(Id);
        }

        public async Task<Paragraph> GetParagraphByQuestionNumber(string ToeicNumber, string ToeicPart, string QuestionNumber)
        {
            var paragraph = await _context.Paragraphs
                                .Where(para => para.ToeicNumber == ToeicNumber && para.ToeicPart == ToeicPart && para.QuestionNumber == QuestionNumber)
                                .Select(para => para)
                                .FirstOrDefaultAsync();

            return paragraph;
        }

        public async Task<PagedList<Paragraph>> GetParagraphsToeicNumberPart(ParagraphParams paragraphParams)
        {
            var paragraphs = _context.Paragraphs.AsQueryable();
            paragraphs = paragraphs.Where(para => para.ToeicNumber == paragraphParams.ToeicNumber && para.ToeicPart == paragraphParams.ToeicPart);
            paragraphs = paragraphs.Select(para => para);
            paragraphs = paragraphs.OrderBy(para => para.QuestionNumber);

            return await PagedList<Paragraph>.CreateAsync(paragraphs, paragraphParams.PageNumber, paragraphParams.PageSize);
        }

        public async Task<bool> ParagraphExists(string ToeicNumber, string ToeicPart, string QuestionNumber)
        {
            return await _context.Paragraphs.AnyAsync(x => x.ToeicNumber == ToeicNumber && x.ToeicPart == ToeicPart && x.QuestionNumber == QuestionNumber);
        }

        public void UpdateParagraph(Paragraph paragraph)
        {
            _context.Entry(paragraph).State = EntityState.Modified;
        }
    }
}
