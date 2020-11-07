using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_HOCTIENGANH.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IVocabularyRepository VocabularyRepository { get; }
        IGrammarRepository GrammarRepository { get; }
        IPracticeRepository PracticeRepository { get; }
        IQuestionReadingRepository QuestionReadingRepository { get; }
        IParagraphRepository ParagraphRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
