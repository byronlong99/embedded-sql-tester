namespace EmbeddedSQLTester.SQLitePlatformConversion
{
    public static class MyExtensions
    {
        public static string ConvertToOrmliteSQLiteDialect(this string input)
        {
            var result = input;
            
            if (SQLServerToOrmliteSQLiteDialectConverter.ConvertToOrmliteSQLiteDialect)
            {
                var sqlPlatformConverter = new SQLServerToOrmliteSQLiteDialectConverter();
                result = sqlPlatformConverter.ConvertToOrmliteSQLiteSQL(input);
            }
            
            return result;
        }
    }
}