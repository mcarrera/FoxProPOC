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

            var conn = Connection;
            conn.Open();
            var comm = conn.CreateCommand();
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
                var stringBuilder = new StringBuilder();
                foreach (var column in row)
                {
                    stringBuilder.Append($"{column.ToString().Trim()} ");
                }
                Console.WriteLine(stringBuilder.ToString().Trim());
            }
        }

        private static OleDbConnection Connection
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["FoxProPOC.Properties.Settings.ConnectionString"].ConnectionString;
                return new OleDbConnection(connectionString);
            }
        }
    }


}
