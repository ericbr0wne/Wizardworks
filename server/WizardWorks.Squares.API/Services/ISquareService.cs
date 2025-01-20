using System.Collections.Generic;
using System.Threading.Tasks;
using static WizardWorks.Squares.API.Models.SquareModel;

namespace WizardWorks.Squares.API.Services
{
    public interface ISquareService
    {
        Task<IEnumerable<Square>> GetSquaresAsync();
        Task<Square> AddSquareAsync();
    }
}