using Microsoft.Graph;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RandREng.GraphClient
{
    public interface IAuthenticationProvider2 : IAuthenticationProvider
    {
        Task<AuthenticationHeaderValue> GetAuthenticationHeaderAsync();
    }
}