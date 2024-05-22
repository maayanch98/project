using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Data.SQLite
{
    internal class SaveNewStore
    {
        public static void saveDataStore(Store store)
        {
            string connectionString = "Data Source=store.db;Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Create a table to store store details if it doesn't exist
                string createTableQuery = "CREATE TABLE IF NOT EXISTS StoreDetails (" +
                    "Id INTEGER PRIMARY KEY, " +
                    "NumberOfEmployees INTEGER, " +
                    "IncomeBalance INTEGER, " +
                    "NumberOfCustomers INTEGER, " +
                    "AdvertisingBudget INTEGER, " +
                    "NumberOfProducts INTEGER)";
                SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, connection);
                createTableCommand.ExecuteNonQuery();

                // Insert store details into the table
                string insertQuery = "INSERT INTO StoreDetails (Id, NumberOfEmployees, IncomeBalance, NumberOfCustomers, AdvertisingBudget, NumberOfProducts) " +
                    "VALUES (@Id, @NumberOfEmployees, @IncomeBalance, @NumberOfCustomers, @AdvertisingBudget, @NumberOfProducts)";
                SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Id", store.id);
                insertCommand.Parameters.AddWithValue("@NumberOfEmployees", store.numberOfEmployees);
                insertCommand.Parameters.AddWithValue("@IncomeBalance", store.incomeBalance);
                insertCommand.Parameters.AddWithValue("@NumberOfCustomers", store.numberOfCustomers);
                insertCommand.Parameters.AddWithValue("@AdvertisingBudget", store.advertisingBudget);
                insertCommand.Parameters.AddWithValue("@NumberOfProducts", store.numberOfProducts);
                insertCommand.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
