using System.Threading.Tasks;
using HomeBoard.Models.Trains;

namespace HomeBoard.WebApp.Services
{
    public interface ITrainsService
    {
        Task<StationBoard> GetStationBoard();
    }
}