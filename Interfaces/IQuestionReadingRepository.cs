using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_HOCTIENGANH.Entities;
using WEB_HOCTIENGANH.Helpers;

namespace WEB_HOCTIENGANH.Interfaces
{
    public interface IQuestionReadingRepository
    {
        void AddQuestionReadingAsync(QuestionReading questionReading);
        Task<QuestionReading> GetQuestionReadingById(int Id);
        Task<PagedList<QuestionReading>> GetQuestionReadingsPart(QuestionReadingParams questionReadingParams);
        Task<IEnumerable<string>> GetToeicNumbers();
        void UpdateQuestionReading(QuestionReading questionReading);
        void DeleteQuestionReadings(string ToeicNumber);
        Task<bool> QuestionReadingExists(string ToeicNumber, string ToeicPart, string QuestionNumber);
    }
}
