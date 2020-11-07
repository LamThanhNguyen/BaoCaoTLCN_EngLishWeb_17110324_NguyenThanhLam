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
    public class PracticeRepository : IPracticeRepository
    {
        private readonly DataContext _context;

        public PracticeRepository(DataContext context)
        {
            _context = context;
        }

        public void AddPracticeAsync(Practice practice)
        {
            _context.Practices.Add(practice);
        }

        public void DeletePractice(Practice practice)
        {
            _context.Practices.Remove(practice);
        }

        public async Task<Practice> GetPracticeById(int Id)
        {
            return await _context.Practices.FindAsync(Id);
        }

        public async Task<PagedList<Practice>> GetPracticesByPracName(PracticesParams practicesParams)
        {
            var practices = _context.Practices.AsQueryable();
            practices = practices.Where(prac => prac.PracticeName == practicesParams.Pracname);
            practices = practices.Select(prac => prac);

            return await PagedList<Practice>.CreateAsync(practices, practicesParams.PageNumber, practicesParams.PageSize);
        }

        public async Task<IEnumerable<string>> GetPracticesName()
        {
            var practices = await _context.Practices
                                .IgnoreQueryFilters()
                                .Select(prac => prac.PracticeName)
                                .ToListAsync();

            return practices;
        }

        public void UpdatePractice(Practice practice)
        {
            _context.Entry(practice).State = EntityState.Modified;
        }

        public async Task<bool> PracticeExists(string pracquestion)
        {
            return await _context.Practices.AnyAsync(x => x.Question == pracquestion);
        }
    }
}
