using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using Exer2.Models;
using System.Data.Entity;
using System.Reflection;

namespace Exer2.Tests
{
    public class TestBase
    {
        protected Mock<QT2DbEntities> MockDbContext { get; private set; }

        protected Mock<DbSet<T>> MockDbSet<T>() where T : class
        {
            return new Mock<DbSet<T>>();
        }

        public void SetupMockContext()
        {
            MockDbContext = new Mock<QT2DbEntities>();
        }

        protected void SetPrivateDbField<T>(T controller, QT2DbEntities mockDb) where T : class
        {
            var dbField = typeof(T).GetField("db", BindingFlags.NonPublic | BindingFlags.Instance);
            if (dbField != null)
            {
                dbField.SetValue(controller, mockDb);
            }
        }
    }
}