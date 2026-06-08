#nullable enable
#pragma warning disable CS8600, CS8602
using System;
using NUnit.Framework;
using VideoRent.Data;

namespace VideoRent.Data.UnitTests
{
    [TestFixture]
    public class GeneroDataTests
    {
        [Test]
        public void GeneroData_Constructor_WithValidConnectionString_CreatesInstance()
        {
            // Arrange
            var connectionString = "Server=.;Database=master;Trusted_Connection=True;";

            // Act
            var type = Type.GetType("VideoRent.Data.GeneroData, VideoRent.Data");
            Assert.That(type, Is.Not.Null, "GeneroData type should be loadable from the VideoRent.Data assembly");
            var instance = Activator.CreateInstance(type!, new object?[] { connectionString });
            Assert.That(instance, Is.Not.Null);

            // Assert
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void GeneroData_Constructor_WithNull_CreatesInstance()
        {
            // Arrange
            string? connectionString = null;

            // Act
            var type = Type.GetType("VideoRent.Data.GeneroData, VideoRent.Data");
            Assert.That(type, Is.Not.Null, "GeneroData type should be loadable from the VideoRent.Data assembly");
            var instance = Activator.CreateInstance(type!, new object?[] { connectionString });
            Assert.That(instance, Is.Not.Null);

            // Assert
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void GetGeneros_WithInvalidConnectionString_ThrowsException()
        {
            // Arrange
            var connectionString = "InvalidConnectionString";
            var type = Type.GetType("VideoRent.Data.GeneroData, VideoRent.Data");
            Assert.That(type, Is.Not.Null, "GeneroData type should be loadable from the VideoRent.Data assembly");
            var instance = Activator.CreateInstance(type!, new object?[] { connectionString });
            Assert.That(instance, Is.Not.Null);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await ((dynamic)instance).GetGeneros());
        }


    }
}
