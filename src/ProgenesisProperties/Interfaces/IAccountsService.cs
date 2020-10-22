using System.Threading.Tasks;
using ProgenesisProperties.data.Entities;
using ProgenesisProperties.Models;

namespace ProgenesisProperties.Interfaces
{
    public interface IAccountsService
    {
         Task <ApplicationUser> CreateUserAsync (SignUpModel model);
         
    }
}