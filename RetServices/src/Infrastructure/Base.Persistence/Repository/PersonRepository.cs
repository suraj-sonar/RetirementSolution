using Base.Application.Logging;
using Base.Application.RepositoryContracts;
using Base.Domain;
using Base.Persistence.DBContext;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Persistence.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DapperDbContext _context;
        private readonly IAppLogger<PersonRepository> _logger;
        public PersonRepository(DapperDbContext context, IAppLogger<PersonRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        async Task<int> IPersonRepository.AddPersonAsync(Person person)
        {
            //write code to add person to database using dapper
            string query = "INSERT INTO [dbo].[Persons] ([FirstName] ,[LastName] ,[Gender] ,[Birthdate] ,[EmailID] ,[DateCreated] ,[DateModified])" +
                "VALUES (@FirstName ,@LastName ,@Gender ,@Birthdate  ,@EmailID ,@DateCreated  ,@DateModified ); SELECT CAST(SCOPE_IDENTITY() as int)";
            var now = DateTime.UtcNow;
            var para = new
            {
                person.FirstName,
                person.LastName,
                person.Gender,
                person.Birthdate,
                person.EmailID,
                DateCreated = now,
                DateModified = now
            };
            int id = await _context.DbConnection.QuerySingleAsync<int>(query , para);
            return id;
        }

        Task IPersonRepository.DeletePersonAsync(int id)
        {
            //implement delete person by id
            string query = "DELETE FROM [dbo].[Persons] WHERE [Id] = @Id";
            var parameters = new { Id = id };
            return _context.DbConnection.ExecuteAsync(query, parameters);
        }

        public async Task<List<Person>> GetAllPersonsAsync()
        {
            _logger.LogInformation("Fetching all persons from the database.");
            string query = "SELECT * FROM [dbo].[Persons]";
            var result = await _context.DbConnection.QueryAsync<Person>(query);
            _logger.LogInformation($"Fetched {result.Count()} persons from the database.");
            return result.ToList();
        }

        async Task<Person?> IPersonRepository.GetPersonByIdAsync(int id)
        {
            //implement get person by id
            string query = "SELECT * FROM [dbo].[Persons] WHERE [Id] = @Id";
            var parameters = new { Id = id };
            var person = await _context.DbConnection.QuerySingleOrDefaultAsync<Person>(query, parameters);
            return person;
        }

        async Task IPersonRepository.UpdatePersonAsync(Person person)
        {
            //implement update person
            string query = @"UPDATE [dbo].[Persons]
                             SET [FirstName] = @FirstName,
                                 [LastName] = @LastName,
                                 [Gender] = @Gender,
                                 [Birthdate] = @Birthdate,
                                 [EmailID] = @EmailID,
                                 [DateModified] = @DateModified
                             WHERE [Id] = @Id";
            var parameters = new
            {
                person.FirstName,
                person.LastName,
                person.Gender,
                person.Birthdate,
                person.EmailID,
                DateModified = DateTime.UtcNow,
                person.id
            };
            await _context.DbConnection.ExecuteAsync(query, parameters);
        }
    }
}
