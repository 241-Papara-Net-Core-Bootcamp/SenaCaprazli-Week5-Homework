using Dapper;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PaparaThirdWeek.Data.Abstracts;
using PaparaThirdWeek.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Data.Concretes
{
    public class DapperRepository<T> : IDapperRepository<T> where T : BaseEntity
    {
        public IConfiguration Configuration { get; }
        private readonly IMapper _mapper;

        public DapperRepository(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            _mapper = mapper;
        }

        public async Task<int> Add(T entity)
        {
            var sql = @"INSERT INTO [dbo].[Companies]
           ([Name]
           ,[Adress]
           ,[City]
           ,[TaxNumber]
           ,[Email]
           ,[IsDeleted]
           ,[CreatedDate]
           ,[CreatedBy]
           ,[LastUpdateAt]
           ,[LastUpdateBy])
     VALUES(@Name, @Adress,@City,@TaxNumber,@Email,@IsDeleted,@CreatedDate,@CreatedBy,@LastUpdateAt,@LastUpdateBy)";
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity); 
                return result;
            }
        }

        public async Task<int> DeleteById(int id)
        {
            var sql = "delete from Companies where Id=@Id";
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            var sql = "select * from Companies";
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<T>(sql);
                return result.ToList();
            }
        }

        public async Task<T> GetById(int id)
        {
            var sql = "select * from Companies where Id=@Id";
            string temp = Configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(temp))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<T>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> Update(T entity, int id)
        {
            var sql = "update Companies set Name=@Name, Adress=@Adress,City=@City,TaxNumber=@TaxNumber,Email=@Email,IsDeleted=@IsDeleted,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,LastUpdateAt=@LastUpdateAt,LastUpdateBy=@LastUpdateBy";
            using (var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;

            }
        }
    }
}
