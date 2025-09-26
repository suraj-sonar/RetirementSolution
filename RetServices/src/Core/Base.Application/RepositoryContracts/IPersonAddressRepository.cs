using Base.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.RepositoryContracts
{
    public interface IPersonAddressRepository
    {
        Task<List<PersonAddress>> GetAllAsync();
        Task<PersonAddress> GetByIdAsync(int id);
        Task<int> AddAsync(PersonAddress address);
        Task<bool> UpdateAsync(PersonAddress address);
        Task<bool> DeleteAsync(int id);
    }

}
