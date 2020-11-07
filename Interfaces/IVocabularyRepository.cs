using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_HOCTIENGANH.Entities;
using WEB_HOCTIENGANH.Helpers;

namespace WEB_HOCTIENGANH.Interfaces
{
    public interface IVocabularyRepository
    {
        void AddVocabularyAsync(Vocabulary vocabulary);
        Task<Vocabulary> GetVocabularyById(int Id);
        Task<Vocabulary> GetVocabularyByEngName(string engname);
        Task<Vocabulary> GetVocabularyByVietName(string vietname);
        Task<PagedList<Vocabulary>> GetVocabularies(VocabulariesParams vocabulariesParams);
        void UpdateVocabulary(Vocabulary vocabulary);
        void DeleteVocabulary(Vocabulary vocabulary);
        Task<bool> VocabularyExists(string engname);
    }
}
