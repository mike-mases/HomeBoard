using System.Threading.Tasks;
using HomeBoard.WebApp.Builders;
using Microsoft.AspNetCore.SignalR;

namespace WebApp.Hubs
{
    public class HomeBoardUpdateHub : Hub
    {
        private readonly IHomeBoardViewModelBuilder _builder;

        public HomeBoardUpdateHub(IHomeBoardViewModelBuilder builder)
        {
            _builder = builder;
        }

        public async Task UpdateHomeBoard()
        {
            
        }
    }
}