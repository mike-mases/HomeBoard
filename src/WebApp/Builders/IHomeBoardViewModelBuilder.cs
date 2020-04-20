using System.Threading.Tasks;
using HomeBoard.Models;

namespace HomeBoard.WebApp.Builders
{
    public interface IHomeBoardViewModelBuilder
    {
        Task<HomeBoardViewModel> BuildViewModel();
    }
}