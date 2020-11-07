using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_HOCTIENGANH.Entities;
using WEB_HOCTIENGANH.Helpers;

namespace WEB_HOCTIENGANH.Interfaces
{
    public interface IPracticeRepository
    {
        void AddPracticeAsync(Practice practice);
        Task<Practice> GetPracticeById(int Id);
        Task<PagedList<Practice>> GetPracticesByPracName(PracticesParams practicesParams);
        Task<IEnumerable<string>> GetPracticesName();
        void UpdatePractice(Practice vocabulary);
        void DeletePractice(Practice practice);
        Task<bool> PracticeExists(string pracquestion);

    }
}
