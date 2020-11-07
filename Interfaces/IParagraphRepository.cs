using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_HOCTIENGANH.Entities;
using WEB_HOCTIENGANH.Helpers;

namespace WEB_HOCTIENGANH.Interfaces
{
    public interface IParagraphRepository
    {
        void AddParagraphAsync(Paragraph paragraph);
        Task<Paragraph> GetParagraphById(int Id);
        Task<Paragraph> GetParagraphByQuestionNumber(string ToeicNumber, string ToeicPart, string QuestionNumber);
        Task<PagedList<Paragraph>> GetParagraphsToeicNumberPart(ParagraphParams paragraphParams);
        void UpdateParagraph(Paragraph paragraph);
        void DeleteParagraphs(string ToeicNumber);
        Task<bool> ParagraphExists(string ToeicNumber, string ToeicPart, string QuestionNumber);
    }
}
