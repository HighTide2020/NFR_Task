
# SQL


## Question 1.
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

## Question 2.
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
```

## Question 3
What is the oldest age (in days) that one of our hens was when they layed an egg:
``` sql
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
## What is this SQL trying to give us?


This SQL query appears to be trying to provide information about chickens and their egg production.

## What does EPD stand for?

In the context of the provided SQL query, "EPD" stands for "Eggs Per Day." It represents the average number of eggs produced per day by each chicken.
