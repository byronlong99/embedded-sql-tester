using EmbeddedSQLTester.SQLitePlatformConversion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedSQLTester.Tests.StatementConverters
{
    [TestClass]
    public class SQLitePlatformConverterUpdateTests
    {
        [TestMethod]
        public void Convert_SchemaDotTable_BecomesSchemaUnderscoreTable()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();

            var query = @"UPDATE SchemaName.[TableName] SET ColumnName = 0 WHERE OtherColumnName = 5";

            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);

            // Assert 
            Assert.AreEqual("UPDATE SchemaName_TableName SET ColumnName = 0 WHERE OtherColumnName = 5", resultingQuery);
        }

        [TestMethod]
        public void Convert_WhereClausesNotAffected()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();

            var query = @"UPDATE SchemaName.TableName T SET ColumnName = 0 WHERE T.ColumnName = 0";

            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);

            // Assert 
            var expectedQuery = @"UPDATE SchemaName_TableName T SET ColumnName = 0 WHERE T.ColumnName = 0";

            Assert.AreEqual(expectedQuery, resultingQuery);
        }
        
        [TestMethod]
        public void Convert_LikeStatementsAreConverted()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();

            var query = @"UPDATE SchemaName.TableName T SET ColumnName = 0 WHERE T.ColumnName LIKE '%' + @Parameter + '%')";

            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);

            // Assert 
            var expectedQuery = @"UPDATE SchemaName_TableName T SET ColumnName = 0 WHERE T.ColumnName LIKE '%' || @Parameter || '%'";

            Assert.AreEqual(expectedQuery, resultingQuery);
        }
    }
}
