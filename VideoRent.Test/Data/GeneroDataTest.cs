using NUnit.Framework;

using System.Linq;

using System.Threading.Tasks;

using Microsoft.Data.SqlClient;

using VideoRent.Data;

namespace VideoRent.Test.Data

{

    public class GeneroDataTests

    {

        private const string DbName = "VideoRent_C12973";

        private string ConnectionString => $"Data Source=163.178.173.130;User ID=lenguajes;Password=lenguajesparaiso2025;Initial Catalog={DbName};Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";

        private string MasterConnectionString => $"Data Source=163.178.173.130;User ID=lenguajes;Password=lenguajesparaiso2025;Initial Catalog=master;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";

        [OneTimeSetUp]

        public async Task OneTimeSetUp()

        {

            using (var conn = new SqlConnection(MasterConnectionString))

            {

                await conn.OpenAsync();

                using (var cmd = conn.CreateCommand())

                {

                    cmd.CommandText = $"IF DB_ID(N'{DbName}') IS NULL CREATE DATABASE [{DbName}];";

                    await cmd.ExecuteNonQueryAsync();

                }

            }

        }

        private async Task RecreateTableAsync()

        {

            using (var conn = new SqlConnection(ConnectionString))

            {

                await conn.OpenAsync();

                using (var cmd = conn.CreateCommand())

                {

                    cmd.CommandText = @"

IF OBJECT_ID('dbo.Genero','U') IS NOT NULL DROP TABLE dbo.Genero;

CREATE TABLE dbo.Genero (

    genero_id INT PRIMARY KEY IDENTITY(1,1),

    nombre_genero NVARCHAR(200) NOT NULL

);";

                    await cmd.ExecuteNonQueryAsync();

                }

            }

        }

        [SetUp]

        public async Task Setup()

        {

            await RecreateTableAsync();

        }

        [Test]

        public async Task GetGeneros_WhenNoRows_ReturnsEmpty()

        {

            var data = new GeneroData(ConnectionString);

            var result = await data.GetGeneros();

            Assert.That(result, Is.Not.Null);

            Assert.That(result, Is.Empty);

        }

        [Test]

        public async Task GetGeneros_ReturnsGeneros_InAscendingOrder()

        {

            using (var conn = new SqlConnection(ConnectionString))

            {

                await conn.OpenAsync();

                using (var cmd = conn.CreateCommand())

                {

                    cmd.CommandText = "INSERT INTO dbo.Genero (nombre_genero) VALUES (@n1), (@n2);";

                    cmd.Parameters.AddWithValue("@n1", "Comedy");

                    cmd.Parameters.AddWithValue("@n2", "Action");

                    await cmd.ExecuteNonQueryAsync();

                }

            }

            var data = new GeneroData(ConnectionString);

            var result = (await data.GetGeneros()).ToList();

            Assert.That(result.Count, Is.EqualTo(2));

            Assert.That(result[0].NombreGenero, Is.EqualTo("Action"));

            Assert.That(result[1].NombreGenero, Is.EqualTo("Comedy"));

            Assert.That(result.All(g => g.GeneroId > 0));

        }

        [OneTimeTearDown]

        public async Task OneTimeTearDown()

        {

            using (var conn = new SqlConnection(MasterConnectionString))

            {

                await conn.OpenAsync();

                using (var cmd = conn.CreateCommand())

                {

                    cmd.CommandText = $@"

ALTER DATABASE [{DbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

DROP DATABASE [{DbName}];";

                    await cmd.ExecuteNonQueryAsync();

                }

            }

        }

    }

}
