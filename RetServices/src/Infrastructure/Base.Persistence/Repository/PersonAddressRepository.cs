using Base.Application.Logging;
using Base.Application.RepositoryContracts;
using Base.Domain;
using Base.Persistence.DBContext;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Persistence.Repository
{
    public class PersonAddressRepository : IPersonAddressRepository
    {
        private readonly IDbConnection _db;
        
        private readonly DapperDbContext _context;
        private readonly IAppLogger<PersonRepository> _logger;
        public PersonAddressRepository(DapperDbContext context, IAppLogger<PersonRepository> logger)
        {
            _context = context;
            _db = context.DbConnection;
            _logger = logger;
        }
        public async Task<List<PersonAddress>> GetAllAsync()
            => (await _db.QueryAsync<PersonAddress>("SELECT * FROM PersonAddresses")).ToList();

        public async Task<PersonAddress> GetByIdAsync(int id)
            => await _db.QueryFirstOrDefaultAsync<PersonAddress>("SELECT * FROM PersonAddresses WHERE Id = @Id", new { Id = id });

        public async Task<int> AddAsync(PersonAddress address)
    => await _db.ExecuteScalarAsync<int>(
        @"INSERT INTO PersonAddresses 
            (PersonId, Address_Line_1, Address_Line_2, City, State, ZipCode, Country, StartDate, EndDate, AddressTypeId, DateCreated, DateModified) 
          VALUES 
            (@PersonId, @Address_Line_1, @Address_Line_2, @City, @State, @ZipCode, @Country, @StartDate, @EndDate, @AddressTypeId, @DateCreated, @DateModified);
          SELECT CAST(SCOPE_IDENTITY() as int);", address);

        public async Task<bool> UpdateAsync(PersonAddress address)
            => (await _db.ExecuteAsync(
                @"UPDATE PersonAddresses SET 
            Address_Line_1 = @Address_Line_1,
            Address_Line_2 = @Address_Line_2,
            City = @City,
            State = @State,
            ZipCode = @ZipCode,
            Country = @Country,
            StartDate = @StartDate,
            EndDate = @EndDate,
            AddressTypeId = @AddressTypeId,
            DateCreated = @DateCreated,
            DateModified = @DateModified
          WHERE Id = @Id", address)) > 0;


        public async Task<bool> DeleteAsync(int id)
            => (await _db.ExecuteAsync("DELETE FROM PersonAddresses WHERE Id = @Id", new { Id = id })) > 0;
    }

}
