using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_HOCTIENGANH.Interfaces;

namespace WEB_HOCTIENGANH.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IUserRepository UserRepository => new UserRepository(_context, _mapper);

        public IVocabularyRepository VocabularyRepository => new VocabularyRepository(_context);

        public IGrammarRepository GrammarRepository => new GrammarRepository(_context);

        public IPracticeRepository PracticeRepository => new PracticeRepository(_context);

        public IQuestionReadingRepository QuestionReadingRepository => new QuestionReadingRepository(_context);

        public IParagraphRepository ParagraphRepository => new ParagraphRepository(_context);

        // Khi gọi phương thức Complete này tức là đang lưu những thay đổi vào Databases.
        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        // Kiểm tra xem bản ghi đang được truy xuất ra khỏi Database có bị thay đổi giá trị hay không.
        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
