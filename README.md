### SQL Unit Testing Made Easy

#### Backstory

After working in the enterprise for several years I began to write unit tests.  Eventually, I found myself writing 
the tests **(TDD)** before writing the code itself.  One limitation I found was that I was not able to write tests for my
SQL queries, which in some cases made up a significant portion of my codebase.  I tried using an **ORM** specifically, [OrmLite](https://ormlite.com/),
but that resulted in having to express complex queries in the ORM itself.  This became quiet clumsy as the query scaled in complexity and it also
became difficult to control the query's performance.  My response was to find a way to make no compromises on how I express my SQL without making
any compromises on my unit testing.  With this library I am able to develop and test the majority of my code without any dependency on infrastructure,
VPN, or even the internet!  This has allowed me to essentially allowed me to take my productivity into my own hands.

####  Unit Testing Embedded SQL for better Coverage and Confidence

Consider the following example of embedded SQL code.

```csharp
using (var connection = await _storeOpsRealtimeDatabaseConnectionFactory.OpenDbConnectionAsync())
{
	var query = "SELECT FirstName
			, LastName
			FROM Employee.Employee";

			
}			
			
```

