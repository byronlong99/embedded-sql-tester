using EmbeddedSQLTester.SQLitePlatformConversion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedSQLTester.Tests.StatementConverters
{
    [TestClass]
    public class SQLitePlatformConverterInsertTests
    {
        [TestMethod]
        public void Convert_SchemaDotTable_BecomesSchemaUnderscoreTable()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();

            var query = @"INSERT INTO SchemaName.[TableName] (Column1, Column2) VALUES (@Value1, @Value2)";

            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);

            // Assert 
            Assert.AreEqual("INSERT INTO SchemaName_TableName (Column1, Column2) VALUES (@Value1, @Value2)", resultingQuery);
        }
    }
}
