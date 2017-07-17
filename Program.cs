using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxProPOC
{
    class Program
    {
        static void Main(string[] args)
        {
            var connString = ConfigurationManager.ConnectionStrings["FoxProPOC.Properties.Settings.ConnectionString"]
                .ConnectionString;

            OleDbConnection conn = new OleDbConnection(connString);
            conn.Open();

            OleDbCommand comm = conn.CreateCommand();
            comm.CommandText = @"SELECT * FROM crop";

            IDataReader reader = comm.ExecuteReader();

            var arrayList = new ArrayList();
            while (reader != null && reader.Read())
            {
                var values = new object[reader.FieldCount];
                reader.GetValues(values);
                arrayList.Add(values);
            }

            reader?.Close();
            conn.Close();

            foreach (object[] row in arrayList)
            {
                foreach (var column in row)
                {
                    Console.WriteLine(column.ToString());
                }

            }
        }
    }
}
