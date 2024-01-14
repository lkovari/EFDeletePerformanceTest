This code snippet is not an exhaustive solution. It just focused on checking the performance differences between RemoveRange vs. ExecuteDelete and Raw Sql Delete in Entity Framework.\

Based on the test the ExecuteDelete about six times faster than the RemoveRange.\
(Before you run, please customize the DB. connection string, because it is still hardcoded currently)\\

Known bug(s):\
When execute update-database with OnModelCreating it never end with done and not emmit error\
Measurement:\
             Delete the content of a ZipCode Entity (41690 row):\
             Avg: ExecuteDelete: 112ms RemoveRange: 1767ms RawSql: 63ms\
             ExecuteDelete 1.7x slower than the Raw SQL\
             RemoveRange 16x slower than ExecuteDelete\
