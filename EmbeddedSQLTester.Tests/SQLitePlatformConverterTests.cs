using EmbeddedSQLTester.SQLitePlatformConversion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmbeddedSQLTester.Tests
{
    [TestClass]
    public class SQLitePlatformConverterTests
    {
        [TestMethod]
        public void Convert_SchemaDotTable_BecomesSchemaUnderscoreTable()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();

            var query = @"SELECT * FROM SchemaName.[TableName]";

            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);

            // Assert 
            Assert.AreEqual("SELECT * FROM SchemaName_TableName", resultingQuery);
        }

        [TestMethod]
        public void Convert_SchemaDotTableWithNoLock_BecomesSchemaUnderscoreTable()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();

            var query = @"SELECT * FROM SchemaName.[TableName] WITH(NOLOCK)";

            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);

            // Assert 
            Assert.AreEqual("SELECT * FROM SchemaName_TableName", resultingQuery.Trim());
        }

        [TestMethod]
        public void Convert_WhereClausesNotAffected()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();

            var query = @"SELECT * FROM SchemaName.TableName T WHERE T.ColumnName = 0";

            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);

            // Assert 
            var expectedQuery = @"SELECT * FROM SchemaName_TableName T WHERE T.ColumnName = 0";

            Assert.AreEqual(expectedQuery, resultingQuery);
        }
        
        [TestMethod]
        public void Convert_LikeStatementsAreConverted()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();

            var query = @"SELECT * FROM SchemaName.TableName T WHERE T.ColumnName LIKE '%' + @Parameter + '%')";

            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);

            // Assert 
            var expectedQuery = @"SELECT * FROM SchemaName_TableName T WHERE T.ColumnName LIKE '%' || @Parameter || '%'";

            Assert.AreEqual(expectedQuery, resultingQuery);
        }

        [TestMethod]
        public void Convert_SelectsNotAffected()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();           

            var query = @"SELECT T.ColumnName FROM SchemaName.TableName";

            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);

            // Assert 
            var expectedQuery = @"SELECT T.ColumnName FROM SchemaName_TableName";

            Assert.AreEqual(expectedQuery, resultingQuery);
        }

        [TestMethod]
        public void Convert_GroupByNotAffected()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();           

            var query = @"SELECT * FROM SchemaName.TableName T GROUP BY T.ColumnName";

            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);

            // Assert 
            var expectedQuery = @"SELECT * FROM SchemaName_TableName T GROUP BY T.ColumnName";

            Assert.AreEqual(expectedQuery, resultingQuery);
        }

        [TestMethod]
        public void Convert_OrderNotAffected()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();           

            var query = @"SELECT * FROM SchemaName.TableName T ORDER BY T.ColumnName";

            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);

            // Assert 
            var expectedQuery = @"SELECT * FROM SchemaName_TableName T ORDER BY T.ColumnName";

            Assert.AreEqual(expectedQuery, resultingQuery);
        }

        [TestMethod]
        public void Convert_JoinsAreAffected()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();           

            var query = @"SELECT * FROM SchemaName.TableName T LEFT JOIN SchemaName.JoinTableName1 JTN ON T.Id = JTN.Id LEFT JOIN SchemaName.JoinTableName2 JTN2 ON JTN1.Id2 = JTN1.Id2";

            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);

            // Assert 
            var expectedQuery = @"SELECT * FROM SchemaName_TableName T LEFT JOIN SchemaName_JoinTableName1 JTN ON T.Id = JTN.Id LEFT JOIN SchemaName_JoinTableName2 JTN2 ON JTN1.Id2 = JTN1.Id2";

            Assert.AreEqual(expectedQuery, resultingQuery);
        }        

        [TestMethod]
        public void Convert_JoinsAreNotAffectedWithNoLock()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();

            var query = @"SELECT * FROM SchemaName.TableName T LEFT JOIN SchemaName.JoinTableName1 JTN WITH(nolock) ON T.Id = JTN.Id LEFT JOIN SchemaName.JoinTableName2 JTN2 WITH(NOLOCK) ON JTN1.Id2 = JTN1.Id2";

            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);

            // Assert 
            var expectedQuery = @"SELECT * FROM SchemaName_TableName T LEFT JOIN SchemaName_JoinTableName1 JTN  ON T.Id = JTN.Id LEFT JOIN SchemaName_JoinTableName2 JTN2  ON JTN1.Id2 = JTN1.Id2";

            Assert.AreEqual(expectedQuery, resultingQuery);
        }

        [TestMethod]
        public void Convert_PagingIsConverted()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();

            var query = @"SELECT * FROM Product.Product P ORDER BY P.ProductId OFFSET (@Page -1) * @RowPerPage ROWS FETCH NEXT @RowPerPage ROWS ONLY";

            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);

            // Assert 
            var expectedQuery = @"SELECT * FROM Product_Product P ORDER BY P.ProductId LIMIT (@RowPerPage) OFFSET @Page -1";

            Assert.AreEqual(expectedQuery, resultingQuery);
        }        
        
        [TestMethod]
        public void Convert_MinutesDATEADDIsConverted()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();
            
            var query = @"SELECT DATEADD(minute, amounttoadd, date) FROM SchemaName.TableName";
            
            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);
            
            var expectedQuery = @"SELECT DATETIME(date, amounttoadd ||' minutes') FROM SchemaName_TableName";                        

            Assert.AreEqual(expectedQuery, resultingQuery);
        }
        
        [TestMethod]
        public void Convert_SecondsDATEADDIsConverted()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();
            
            var query = @"SELECT DATEADD(second, amounttoadd, date) FROM SchemaName.TableName";
            
            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);
            
            var expectedQuery = @"SELECT DATETIME(date, amounttoadd ||' seconds') FROM SchemaName_TableName";                        

            Assert.AreEqual(expectedQuery, resultingQuery);
        }
        
        [TestMethod]
        public void Convert_SecondsDATEADDIsConvertedInWhereClause()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();
            
            var query = @"SELECT * FROM SchemaName.TableName WHERE DATEADD(second, amounttoadd, date) < @DateTime";
            
            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);
            
            var expectedQuery = @"SELECT * FROM SchemaName_TableName WHERE DATETIME(date, amounttoadd ||' seconds') < @DateTime";                        

            Assert.AreEqual(expectedQuery, resultingQuery);
        }
        
        [TestMethod]
        public void Convert_ConvertStatementIsRemoved()
        {
            // Arrange
            var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();
            
            var query = @"SELECT CAST(columnName as varchar) FROM SchemaName.TableName";
            
            // Act
            var resultingQuery = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(query);
            
            var expectedQuery = @"SELECT CAST(columnName as text) FROM SchemaName_TableName";                        

            Assert.AreEqual(expectedQuery, resultingQuery);
        }      
    }
}
