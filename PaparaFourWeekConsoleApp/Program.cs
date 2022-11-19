using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PaparaFourWeekConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IDbConnection con = new SqlConnection("Server=DESKTOP-ESCCKVH\\SQLEXPRESS;Database=PaparaTestDb;Trusted_Connection=True;");
            con.Open();
            con.Execute(@"INSERT INTO [dbo].[Companies]
([Name]
, [Adress]
, [City]
, [TaxNumber]
, [Email]
, [IsDeleted]
, [CreatedDate]
, [CreatedBy]
, [LastupdateAt]
, [LastUpdateBy])
VALUES(@Name, 
@City,
@Adress, 
@TaxNumber, 
@Email, 
@IsDeleted, 
@CreatedDate, 
@CreatedBy, 
@LastUpdateAt,
@LastUpdateBy)", new Company
            {
                Name = "google",
                Adress = "Los Angeles",
                City = "USA",
                CreatedBy = "Dapperuser",
                CreatedDate = DateTime.Now,
                Email = "support@gmail.com",
                IsDeleted = false,
                TaxNumber = "+24454534534"
            });

            var companyList = con.Query<Company>("select * from Companies").ToList();
           foreach(var item in companyList)
            {
                Console.WriteLine(item.Name);

            }
            con.Close();

        }
    }
}
