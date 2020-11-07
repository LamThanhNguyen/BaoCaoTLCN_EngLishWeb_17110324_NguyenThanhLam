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
    public class GrammarRepository : IGrammarRepository
    {
        private readonly DataContext _context;

        public GrammarRepository(DataContext context)
        {
            _context = context;
        }

        public void AddGrammarAsync(Grammar grammar)
        {
            _context.Grammars.Add(grammar);
        }

        public void DeleteGrammar(Grammar grammar)
        {
            _context.Grammars.Remove(grammar);
        }

        public async Task<Grammar> GetGrammarByGrammarName(string grammarname)
        {
            return await _context.Grammars
                .SingleOrDefaultAsync(x => x.GrammarName == grammarname.ToLower());
        }

        public async Task<Grammar> GetGrammarById(int id)
        {
            return await _context.Grammars.FindAsync(id);
        }

        public async Task<PagedList<Grammar>> GetGrammars(GrammarsParams grammarsParams)
        {
            var grammars = _context.Grammars.AsQueryable();
            grammars = grammars.Select(grammar => grammar);

            return await PagedList<Grammar>.CreateAsync(grammars,
                grammarsParams.PageNumber, grammarsParams.PageSize); 
        }

        public void UpdateGrammar(Grammar grammar)
        {
            _context.Entry(grammar).State = EntityState.Modified;
        }

        public async Task<bool> GrammarExists(string grammarname)
        {
            return await _context.Grammars.AnyAsync(x => x.GrammarName == grammarname.ToLower());
        }
    }
}
