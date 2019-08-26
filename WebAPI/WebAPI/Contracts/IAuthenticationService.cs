using System.Threading.Tasks;
using WebAPI.Models.Requests;

namespace WebAPI.Contracts
{
    public interface IAuthenticationService
    {
        Task<SignInResponse> SignInAsync(SignInRequest request);
        Task<SignUpResponse> SignUpAsync(SignUpRequest request);
    }
}
