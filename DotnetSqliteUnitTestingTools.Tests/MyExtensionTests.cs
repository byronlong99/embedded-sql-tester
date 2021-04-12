using DotnetSqliteUnittestingTools.SQLitePlatformConversion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotnetSqliteUnittestingTools.Tests
{
    [TestClass]
    public class MyExtensionTests
    {
        [TestMethod]
        public void ConvertToOrmliteSQLiteDialectTest()
        {
            // Arrange   
            SQLServerToOrmliteSQLiteDialectConverter.ConvertToOrmliteSQLiteDialect = true;
            
            var query = @"SELECT * FROM SchemaName.TableName T WHERE T.ColumnName = 0";

            // Act
            var convertedQuery = query.ConvertToOrmliteSQLiteDialect();

            var expectedQuery = @"SELECT * FROM SchemaName_TableName T WHERE T.ColumnName = 0";

            Assert.AreEqual(expectedQuery, convertedQuery);
        }
    }
}