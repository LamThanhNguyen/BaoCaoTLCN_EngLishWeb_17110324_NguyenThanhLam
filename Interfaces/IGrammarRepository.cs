using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_HOCTIENGANH.Entities;
using WEB_HOCTIENGANH.Helpers;

namespace WEB_HOCTIENGANH.Interfaces
{
    public interface IGrammarRepository
    {
        void AddGrammarAsync(Grammar grammar);
        Task<Grammar> GetGrammarById(int id);
        Task<Grammar> GetGrammarByGrammarName(string grammarname);
        Task<PagedList<Grammar>> GetGrammars(GrammarsParams grammarsParams);
        void UpdateGrammar(Grammar grammar);
        void DeleteGrammar(Grammar grammar);
        Task<bool> GrammarExists(string grammarname);
    }
}
