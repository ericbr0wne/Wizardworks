using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WizardWorks.Squares.API.Models.SquareModel;

namespace WizardWorks.Squares.API.Repositories
{
    public interface ISquareRepository
    {
        Task<IEnumerable<Square>> GetAllAsync();
        Task SaveAsync(IEnumerable<Square> squares);
    }
}