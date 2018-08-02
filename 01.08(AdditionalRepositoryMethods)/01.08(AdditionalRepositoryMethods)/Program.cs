using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AdditionalRepositoryMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Record> records;
            using (Repository repo = new Repository())
            {

                //repo.CreateRecord(new Record
                //{
                //    Author = "Ronald",
                //    Text = "Bye!",
                //    RecordDate = DateTime.Now
                //});

                records = repo.GetRecords();
                repo.UpdateRecord(20, "Bye2!", "Vadmitriyi", "2018 - 08 - 01 19:55:04");
                repo.DeleteRecord(20);
                records = repo.GetRecords();
            }
            foreach (var record in records)
            {
                Console.WriteLine($"{record.Id}\t{record.Text}");
            }
        }
    }
}
