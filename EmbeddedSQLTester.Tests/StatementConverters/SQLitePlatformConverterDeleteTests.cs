using EmbeddedSQLTester.SQLitePlatformConversion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedSQLTester.Tests.StatementConverters
{
    [TestClass]
    public class SQLitePlatformConverterDeleteTests
    {
        [TestMethod]
        public void Convert_SchemaDotTable_BecomesSchemaUnderscoreTable()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();

            var query = @"DELETE FROM SchemaName.[TableName] WHERE OtherColumnName = 5";

            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);

            // Assert 
            Assert.AreEqual("DELETE FROM SchemaName_TableName WHERE OtherColumnName = 5", resultingQuery);
        }
    }
}
