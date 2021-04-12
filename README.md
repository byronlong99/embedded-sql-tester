### SQL Unit Testing Made Easy

#### Backstory

After working in the enterprise for several years I began to write unit tests.  Eventually, I found myself writing 
the tests **(TDD)** before writing the code itself.  One limitation I found was that I was not able to write tests for my
SQL queries, which in some cases made up a significant portion of my codebase.  I tried using an **ORM** specifically, [OrmLite](https://ormlite.com/),
but that resulted in having to express complex queries in the ORM itself.  This became quiet clumsy as the query scaled in complexity and it also
became difficult to control the query's performance.  My response was to find a way to make no compromises on how I express my SQL without making
any compromises on my unit testing.  With this library I am able to develop and test the majority of my code without any dependency on infrastructure,
VPN, or even the internet!  This has allowed me to essentially allowed me to take my productivity into my own hands.

###  Unit Testing Embedded SQL for better Coverage and Confidence

Consider the following example of embedded SQL code that needs to be unit tested:

```csharp
using (var connection = await ormliteConnectionFactory.OpenDbConnectionAsync())
{
	var query = "SELECT FirstName
			, LastName
			FROM Personnel.Employee
			WHERE EmployeeId = @EmployeeId";

			return await connection.QueryAsync<Employee>(query, new {EmployeeId = employeeId});
}						
```

#### Setup

First, we just need to create the Ormlite Entities and create tables in sqlite with them.

* First install the nuget package **DotNetSqliteUnitestingTools**.
* Add the following code to your AssemblyInitialize.  This example uses **MSTest**.

```csharp
[AssemblyInitialize]
public static void AssemblyInit(TestContext context)
{
	SQLServerToOrmliteSQLiteDialectConverter.ConvertToOrmliteSQLiteDialect = true;
}
```

* Create an Ormlite POCO for the employee table.

```csharp
[Schema("Personnel")]
[Alias("Employee")]
public class Employee
{
	[AutoIncrement]
	public int EmployeeId { get; set; }public int EmployeeId { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }	
}
```

* Create an OrmliteConnectionFactory instance with **:memory:** as the connection string.

```csharp
var ormliteConnectionFactory = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider));
```

* Use the POCO class to create a table in the Sqlite in memory database.

```csharp
ormliteConnectionFactory.DropAndCreateTable<Employee>();
```

* Create and insert an Employee.

```csharp
var employee = new Employee();
employee.FirstName = "Fred";
employee.LastName = "Flintstone";
ormliteConnectionFactory.Save(employee);
```

#### Converting the query

We can now invoke the string extension method to convert from SQL Server dialect to Sqlite dialect and we will get our employee record!

```csharp
using (var connection = await ormliteConnectionFactory.OpenDbConnectionAsync())
{
	var query = "SELECT FirstName
			, LastName
			FROM Personnel.Employee
			WHERE EmployeeId = @EmployeeId";

			return await connection.QueryAsync<Employee>(query.ConvertToOrmliteSQLiteDialect(), new {EmployeeId = employeeId});
}						
```