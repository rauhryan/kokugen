using System;
using Kokugen.Core.Domain;
using NUnit.Framework;
using FluentNHibernate.Testing;

namespace Kokugen.Tests.Persistence.Tests
{
    [TestFixture]
    public class ProjectPersistenceTester : PersistenceTesterContext<Project>
    {
        [Test]
        public void Can_save_Project()
        {
            var company = new Company {Name = "Test", Address = new Address{ StreetLine1 = "Test Street 123", City = "Weatherford", State = "TX", ZipCode = "78483"}};
            Specification
                .CheckProperty(x => x.Name, "Test Project Six Two")
                .CheckProperty(x => x.Description, "This is a test project used for persistence testing")
                .CheckProperty(x => x.StartDate, DateTime.Today)
                .CheckProperty(x => x.EndDate, DateTime.Today.AddDays(5))
                .CheckProperty(x => x.AverageTimeSpentPerSession, 12.8)
                .CheckProperty(x => x.TotalTime, 768.34)
                .CheckReference(x => x.Company, company)
                .VerifyTheMappings();

        }
    }

    [TestFixture]
    public class UserPersistenceTester : PersistenceTesterContext<User>
    {
        [Test]
        public void Can_Save_User()
        {
            Specification
                .CheckProperty(x => x.FirstName, "John")
                .CheckProperty(x => x.LastName, "User")
                .CheckProperty(x => x.EmailAddress, "john@john.com");
        }
    }
}