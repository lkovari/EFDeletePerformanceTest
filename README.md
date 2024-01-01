This code snippet is not an exhaustive solution. It just focused on checking the performance differences between RemoveRange vs. ExecuteDelete in Entity Framework. 

Based on the test the ExecuteDelete about six times faster than the RemoveRange.

(Before you run, please customize the DB. connection string, because it is still hardcoded currently)

Known bug(s):
When execute update-database with OnModelCreating it never end with done and not emmit error
