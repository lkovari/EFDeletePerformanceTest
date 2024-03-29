﻿using EFDeletePerformanceTest.TestCases;

namespace DeletePerformanceTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            var deleteExecutor = new DeleteExecutor();

            /* Measurement 
             Delete the content of a ZipCode Entity (41690 row):
             1.   ExecuteDelete: 201ms RemoveRange: 1667ms RawSql: 70ms
             2.   ExecuteDelete: 104ms RemoveRange: 1920ms RawSql: 66ms
             3.   ExecuteDelete: 105ms RemoveRange: 2003ms RawSql: 59ms
             4.   ExecuteDelete: 107ms RemoveRange: 1766ms RawSql: 57ms
             5.   ExecuteDelete: 116ms RemoveRange: 1572ms RawSql: 58ms
             6.   ExecuteDelete:  90ms RemoveRange: 1816ms RawSql: 59ms
             7.   ExecuteDelete: 103ms RemoveRange: 1762ms RawSql: 68ms
             8.   ExecuteDelete:  96ms RemoveRange: 1763ms RawSql: 68ms
             9.   ExecuteDelete: 103ms RemoveRange: 1740ms RawSql: 60ms
             10.  ExecuteDelete: 100ms RemoveRange: 1670ms RawSql: 67ms

             Avg: ExecuteDelete: 112ms RemoveRange: 1767ms RawSql: 63ms
            */
            Console.WriteLine("EntityFramework RemoveRange() vs. ExecuteDelete() performance comparison");
            Console.WriteLine();

            deleteExecutor.ExecuteDelete();

        }
    }
}
