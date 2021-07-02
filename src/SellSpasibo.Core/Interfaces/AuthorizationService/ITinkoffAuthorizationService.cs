using System.Threading.Tasks;

namespace SellSpasibo.Core.Interfaces.AuthorizationService
{
    public interface ITinkoffAuthorizationService
    {
        public Task ContinueAuthorize(string login, string code);
        public Task StartAuthorizeInAccount(string login);

    }
}