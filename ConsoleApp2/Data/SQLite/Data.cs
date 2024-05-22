using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.InteropServices;

public class Data
{
    static Random rnd = new Random();

    public static void CreateTables()
    {
        string connectionString = "Data Source=store.db;Version=3;";
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            // Create Store table
            string createStoreTableQuery = @"CREATE TABLE IF NOT EXISTS Store (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            NumberOfEmployees INTEGER,
                                            IncomeBalance INTEGER,
                                            NumberOfCustomers INTEGER,
                                            AdvertisingBudget INTEGER,
                                            NumberOfProducts INTEGER
                                        )";
            using (SQLiteCommand command = new SQLiteCommand(createStoreTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }

            // Create Employee table
            string createEmployeeTableQuery = @"CREATE TABLE IF NOT EXISTS Employee (
                                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                StoreId INTEGER,
                                                ManagementLevel INTEGER,
                                                Salary INTEGER,
                                                FOREIGN KEY (StoreId) REFERENCES Store(Id)
                                            )";
            using (SQLiteCommand command = new SQLiteCommand(createEmployeeTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }

            // Create Product table
            string createProductTableQuery = @"CREATE TABLE IF NOT EXISTS Product (
                                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                StoreId INTEGER,
                                                NetProfit INTEGER,
                                                StockSize INTEGER,
                                                FOREIGN KEY (StoreId) REFERENCES Store(Id)
                                            )";
            using (SQLiteCommand command = new SQLiteCommand(createProductTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public static void CreateData()
    {
        string connectionString = "Data Source=store.db;Version=3;";
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            // Begin transaction
            using (var transaction = connection.BeginTransaction())
            {
                // Generate 120 samples
                for (int i = 1; i <= 1163; i++)
                {
                    // Generate random store data
                    int id = i;
                    int incomeBalance = GenerateIncomeBalance();
                    bool isLargeBusiness = incomeBalance > 127063;

                    int numberOfEmployees = GenerateNumberOfEmployees(isLargeBusiness);
                    int numberOfCustomers = GenerateNumberOfCustomers(isLargeBusiness);
                    int advertisingBudget = GenerateAdvertisingBudget(incomeBalance, isLargeBusiness);
                    int numberOfProducts = GenerateNumberOfProducts(isLargeBusiness);

                    // Generate random employee data
                    Employee[] employees = GenerateRandomEmployees(numberOfEmployees);

                    // Generate random product data based on income balance and business size
                    Product[] products = GenerateRandomProducts(numberOfProducts, isLargeBusiness);

                    // Create a store object
                    Store myStore = new Store(id, numberOfEmployees, incomeBalance, numberOfCustomers, advertisingBudget, numberOfProducts, employees, products);

                    // Print store information
                    Console.WriteLine($"Sample {i}:");
                    Console.WriteLine(myStore);
                    Console.WriteLine("\nEmployees:");
                    foreach (Employee emp in employees)
                    {
                        Console.WriteLine(emp);
                    }
                    Console.WriteLine("\nProducts:");
                    foreach (Product prod in products)
                    {
                        Console.WriteLine(prod);
                    }

                    // Save data to the database
                    SaveDataToDatabase(connection, myStore);
                }

                // Commit transaction
                transaction.Commit();
            }
        }
    }







    static int GenerateIncomeBalance()
    {
        return rnd.Next(-200000, 1000000); // Adjust income balance range
    }

    static int GenerateNumberOfEmployees(bool isLargeBusiness)
    {
        return isLargeBusiness ? rnd.Next(23, 318) : rnd.Next(1, 28); // Adjust number of employees range
    }

    static int GenerateNumberOfProducts(bool isLargeBusiness)
    {
        return isLargeBusiness ? rnd.Next(3, 318) : rnd.Next(3, 34); // Adjust number of employees range
    }

    static int GenerateNumberOfCustomers(bool isLargeBusiness)
    {
        return isLargeBusiness ? rnd.Next(83, 20355) : rnd.Next(1, 175); // Adjust number of customers range
    }

    static int GenerateAdvertisingBudget(int incomeBalance, bool isLargeBusiness)
    {
        if (incomeBalance < 0)
        {
            return rnd.Next(32, 79); // Adjust advertising budget for negative income balance
        }
        else if (isLargeBusiness)
        {
            return rnd.Next((int)(0.17 * incomeBalance), (int)(0.32 * incomeBalance) + 1);
        }
        else
        {
            return rnd.Next((int)(0.04 * incomeBalance), (int)(0.13 * incomeBalance) + 1);
        }
    }

    


    static Employee[] GenerateRandomEmployees(int numberOfEmployees)
    {
        List<Employee> employees = new List<Employee>(numberOfEmployees);
        Random rnd = new Random();

        int numEmloyOfManager = Math.Min(rnd.Next(3, 15), numberOfEmployees);

        int manageMentLevel = 1;

        // Create and add employees to the list
        for (int i = 0; i < numberOfEmployees; i++)
        {
                employees.Add(new Employee(i,manageMentLevel, GenerateUniqueSalary()));
                if (i% numEmloyOfManager == 0) // Update management level every 3rd employee
                {
                    employees[i].ManagementLevel = employees[i].ManagementLevel + 1;
                    for(int k= 0; k< (i/numEmloyOfManager); k++)
                    {
                    employees[k].ManagementLevel = employees[k].ManagementLevel+1;
                    }
                }
        }
        // Shuffle employees for randomness
        Shuffle(employees.ToArray());

        return employees.ToArray();
    }


    static void Shuffle<T>(T[] array)
    {
        Random rnd = new Random();
        int n = array.Length;
        while (n > 1)
        {
            int k = rnd.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }


    static int GenerateUniqueSalary()
    {
        Random rnd = new Random();

        // Generate the first four digits between 1 and 9
        int firstDigit = rnd.Next(0, 5);
        int secondDigit = rnd.Next(1, 10);
        int thirdDigit = rnd.Next(1, 10);
        int fourthDigit = rnd.Next(1, 10);
        int fifthDigit = rnd.Next(1, 10);

  
        // Construct the salary
        int salary = firstDigit * 10000 + secondDigit * 1000 + thirdDigit * 100 + fourthDigit * 10 + fifthDigit;

        return salary;
    }



    static Product[] GenerateRandomProducts(int numberOfProducts, bool isLargeBusiness)
    {
        List<Product> products = new List<Product>();

        for (int i = 0; i < numberOfProducts; i++)
        {
            int netProfit = GenerateRandomPrice(isLargeBusiness);
            int stockSize = rnd.Next(5, 51); // Random stock size between 5 and 50
            products.Add(new Product(i,netProfit, stockSize));
        }

        return products.ToArray();
    }

    static int GenerateRandomPrice(bool isLargeBusiness)
    {
        // Define price distribution based on business size
        int[] priceDistribution;
        if (isLargeBusiness)
        {
            priceDistribution = new int[] { 17, 42, 28, 13 }; // 2, 3, 4, 5 digits
        }
        else
        {
            priceDistribution = new int[] { 14, 39, 36, 11 }; // 1, 2, 3, 4 digits
        }

        // Define price digits
        int[] priceDigits =  new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 } ;

        // Select the number of digits for the price based on the distribution
        int priceDigitIndex = GetWeightedRandomIndex(priceDistribution);
        int priceDigit = priceDigits[priceDigitIndex];

        // Shuffle digits for randomness
        Shuffle(priceDigits);

        // Generate the price
        int price = 0;
        for (int i = 0; i < priceDigit; i++)
        {
            price += priceDigits[i] * (int)Math.Pow(10, i);
        }
        return price;
    }


    static int GetWeightedRandomIndex(int[] weights)
    {
        int totalWeight = weights.Sum();
        int randomNumber = rnd.Next(1, totalWeight + 1);
        int cumulativeWeight = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            cumulativeWeight += weights[i];
            if (randomNumber <= cumulativeWeight)
            {
                return i;
            }
        }
        return weights.Length - 1; // Default to the last index
    }

    static void SaveDataToDatabase(SQLiteConnection connection, Store store)
    {
        // Insert store data into Store table
        string insertStoreQuery = "INSERT INTO Store (NumberOfEmployees, IncomeBalance, NumberOfCustomers, AdvertisingBudget, NumberOfProducts) VALUES (@NumberOfEmployees, @IncomeBalance, @NumberOfCustomers, @AdvertisingBudget, @NumberOfProducts)";
        using (SQLiteCommand command = new SQLiteCommand(insertStoreQuery, connection))
        {
            command.Parameters.AddWithValue("@NumberOfEmployees", store.numberOfEmployees);
            command.Parameters.AddWithValue("@IncomeBalance", store.incomeBalance);
            command.Parameters.AddWithValue("@NumberOfCustomers", store.numberOfCustomers);
            command.Parameters.AddWithValue("@AdvertisingBudget", store.advertisingBudget);
            command.Parameters.AddWithValue("@NumberOfProducts", store.numberOfProducts);
            command.ExecuteNonQuery();
        }

        long storeId;
        // Get the last inserted store Id
        using (SQLiteCommand command = new SQLiteCommand("SELECT last_insert_rowid()", connection))
        {
            storeId = (long)command.ExecuteScalar();
        }

        // Insert employee data into Employee table
        for (int i = 0; i < store.numberOfEmployees; i++)
        {
            string insertEmployeeQuery = "INSERT INTO Employee (StoreId, ManagementLevel, Salary) VALUES (@StoreId, @ManagementLevel, @Salary)";
            using (SQLiteCommand command = new SQLiteCommand(insertEmployeeQuery, connection))
            {
                command.Parameters.AddWithValue("@StoreId", storeId);
                command.Parameters.AddWithValue("@ManagementLevel", store.employeeArr[i].ManagementLevel);
                command.Parameters.AddWithValue("@Salary", store.employeeArr[i].Salary);
                command.ExecuteNonQuery();
            }
        }
            
        // Insert product data into Product table
        for (int i = 0; i < store.numberOfProducts; i++)
        {
            string insertProductQuery = "INSERT INTO Product (StoreId, NetProfit, StockSize) VALUES (@StoreId, @NetProfit, @StockSize)";
            using (SQLiteCommand command = new SQLiteCommand(insertProductQuery, connection))
            {
                command.Parameters.AddWithValue("@StoreId", storeId);
                command.Parameters.AddWithValue("@NetProfit", store.productArr[i].NetProfit);
                command.Parameters.AddWithValue("@StockSize", store.productArr[i].StockSize);
                command.ExecuteNonQuery();
            }
        }
    }
}


