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
    public class QuestionReadingRepository : IQuestionReadingRepository
    {
        private readonly DataContext _context;

        public QuestionReadingRepository(DataContext context)
        {
            _context = context;
        }
        public void AddQuestionReadingAsync(QuestionReading questionReading)
        {
            _context.QuestionReadings.Add(questionReading);
        }

        public async void DeleteQuestionReadings(string ToeicNumber)
        {
            var questionReadings = await _context.QuestionReadings
                                        .Where(ques => ques.ToeicNumber == ToeicNumber)
                                        .Select(ques => ques)
                                        .ToListAsync();

            _context.QuestionReadings.RemoveRange(questionReadings);
        }

        public async Task<QuestionReading> GetQuestionReadingById(int Id)
        {
            return await _context.QuestionReadings.FindAsync(Id);
        }

        public async Task<PagedList<QuestionReading>> GetQuestionReadingsPart(QuestionReadingParams questionReadingParams)
        {
            var questionReadings = _context.QuestionReadings.AsQueryable();
            questionReadings = questionReadings.Where(ques => ques.ToeicNumber == questionReadingParams.ToeicNumber && ques.ToeicPart == questionReadingParams.ToeicPart);
            questionReadings = questionReadings.Select(ques => ques);
            questionReadings = questionReadings.OrderBy(ques => ques.QuestionNumber);

            return await PagedList<QuestionReading>.CreateAsync(questionReadings, questionReadingParams.PageNumber, questionReadingParams.PageSize);
        }

        public async Task<IEnumerable<string>> GetToeicNumbers()
        {
            var toeicNumbers = await _context.QuestionReadings
                                        .IgnoreQueryFilters()
                                        .Select(toeic => toeic.ToeicNumber)
                                        .Distinct()
                                        .ToListAsync();
            return toeicNumbers;

        }

        public void UpdateQuestionReading(QuestionReading questionReading)
        {
            _context.Entry(questionReading).State = EntityState.Modified;
        }

        public async Task<bool> QuestionReadingExists(string ToeicNumber, string ToeicPart, string QuestionNumber)
        {
            return await _context.QuestionReadings.AnyAsync(x => x.ToeicNumber == ToeicNumber && x.ToeicPart == ToeicPart && x.QuestionNumber == QuestionNumber);
        }
    }
}
