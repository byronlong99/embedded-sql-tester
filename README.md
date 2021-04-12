### SQL Unit Testing Made Easy

#### Backstory

After working in the enterprise for several years I began to write unit tests.  Eventually, I found myself writing 
the tests **(TDD)** before writing the code itself.  One limitation I found was that I was not able to write tests for my
SQL queries, which in some cases made up a significant portion of my codebase.  I tried using an **ORM** specifically [Ormlite](https://ormlite.com/)
but that resulted in having to express complex queries in the ORM itself.  This became quiet clumsy as the query scaled in complexity and it also
became difficult to control the query's performance.