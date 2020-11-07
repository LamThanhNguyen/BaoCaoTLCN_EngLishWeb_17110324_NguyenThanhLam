using AutoMapper;
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
    public class VocabularyRepository : IVocabularyRepository
    {
        private readonly DataContext _context;

        public VocabularyRepository(DataContext context)
        {
            _context = context;
        }

        public void AddVocabularyAsync(Vocabulary vocabulary)
        {
            _context.Vocabularies.Add(vocabulary);
        }

        public void DeleteVocabulary(Vocabulary vocabulary)
        {
            _context.Vocabularies.Remove(vocabulary);
        }

        public async Task<PagedList<Vocabulary>> GetVocabularies(VocabulariesParams vocabulariesParams)
        {
            var vocabularies = _context.Vocabularies.AsQueryable();
            vocabularies = vocabularies.Select(vocabulary => vocabulary);

            return await PagedList<Vocabulary>.CreateAsync(vocabularies,
                vocabulariesParams.PageNumber, vocabulariesParams.PageSize);
        }

        public async Task<Vocabulary> GetVocabularyById(int Id)
        {
            return await _context.Vocabularies.FindAsync(Id);
        }

        public async Task<Vocabulary> GetVocabularyByEngName(string engname)
        {
            return await _context.Vocabularies
                .SingleOrDefaultAsync(x => x.EngName == engname.ToLower());
        }

        public async Task<Vocabulary> GetVocabularyByVietName(string vietname)
        {
            return await _context.Vocabularies
                .SingleOrDefaultAsync(x => x.VietName == vietname.ToLower());
        }

        public void UpdateVocabulary(Vocabulary vocabulary)
        {
            _context.Entry(vocabulary).State = EntityState.Modified;
        }

        public async Task<bool> VocabularyExists(string engname)
        {
            return await _context.Vocabularies.AnyAsync(x => x.EngName == engname.ToLower());
        }
    }
}
