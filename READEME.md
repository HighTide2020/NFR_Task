# NFR Task

This C# program monitors a source directory for new files and moves them to a destination directory. If the moved file is a text file, it logs the first line in a log file. It also handles errors and ensures that required directories exist. Make sure to replace sourceDirectory, destinationDirectory, and logFile with your desired paths.


# SQL
This SQL query appears to be providing a report on the production statistics for chickens in a farm or similar scenario. Let's break down what each part of the query is doing:

`SELECT:` It selects the columns that will be included in the output.
`Name:` Name of the chicken.
`Max(DateLayed) - Min(DateLayed)` AS DaysInProduction: Calculates the number of days the chicken has been in production by subtracting the earliest laying date from the latest laying date.
`Count(*)` AS Qty: Counts the number of eggs laid by the chicken.
Qty / DaysInProduction AS EPD: Calculates the average eggs produced per day (EPD) by dividing the total quantity of eggs by the total days in production.
FROM: It specifies the tables involved in the query.
Chickens and Eggs are joined on Chickens.ID = Eggs.ChickenID, indicating that Chickens table contains information about individual chickens and Eggs table contains information about the eggs they lay.
GROUP BY Name: Groups the results by the Name column, aggregating the statistics for each chicken separately.
So, the output of this query would be a report listing the name of each chicken, the total days it has been in production, the total quantity of eggs it has laid, and the average eggs produced per day.

In the context of the provided SQL query, "EPD" stands for "Eggs Per Day." It represents the average number of eggs produced per day by each chicken. The calculation for EPD is performed by dividing the total quantity of eggs laid `(Qty)`` by the total number of days the chicken has been in production `(DaysInProduction)`.


Here's the SQL to accomplish the tasks:

Show me which Chicken layed the most eggs in the past 30 days:
sql
```SQL
SELECT TOP 1
    c.Name AS ChickenName,
    COUNT(*) AS EggCount
FROM
    Chickens c
JOIN
    Eggs e ON c.ID = e.ChickenID
WHERE
    e.DateLayed >= DATEADD(day, -30, GETDATE()) -- Select eggs laid in the past 30 days
GROUP BY
    c.Name
ORDER BY
    EggCount DESC;
```
Show me which Chicken layed the fewest Eggs in December of 2015:
sql
``` SQL
SELECT TOP 1
    c.Name AS ChickenName,
    COUNT(*) AS EggCount
FROM
    Chickens c
JOIN
    Eggs e ON c.ID = e.ChickenID
WHERE
    YEAR(e.DateLayed) = 2015
    AND MONTH(e.DateLayed) = 12 -- Select eggs laid in December 2015
GROUP BY
    c.Name
ORDER BY
    EggCount ASC;
What is the oldest age (in days) that one of our hens was when they layed an egg:
sql
Copy code
SELECT TOP 1
    DATEDIFF(day, c.DateOfBirth, MIN(e.DateLayed)) AS OldestAgeWhenLayed
FROM
    Chickens c
JOIN
    Eggs e ON c.ID = e.ChickenID
GROUP BY
    c.ID
ORDER BY
    OldestAgeWhenLayed DESC;
```
These queries should provide the requested information based on the provided tables. Adjust the column names and table names as necessary if they differ from the ones provided.
