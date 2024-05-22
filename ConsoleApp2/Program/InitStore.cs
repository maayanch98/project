using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Program
{
    public class InitStore
    {
        public static Store initStore(int id)
        {
            int numberOfEmployees = GetUserInputAsInt("Enter the number of employee:");

            int incomeBalance = GetUserInputAsInt("Enter the income balance:");

            int numCustomer = GetUserInputAsInt("Enter the number of Customer: ");

            int advertisingBudget = GetUserInputAsInt("Enter the advertising Budget: ");

            int numberOfDiffrentProducts = GetUserInputAsInt("Enter the number of diffrent products");

            Employee[] arrEmploy=new Employee[numberOfEmployees];

            for(int i =0; i< numberOfEmployees; i++) 
            {
                arrEmploy[i] = initEmoloyee(i);
            }
            Product[] arrProduct = new Product[numberOfDiffrentProducts];

            for (int i = 0; i < numberOfDiffrentProducts; i++)
            {
                arrProduct[i] = initProduct(i);
            }
            Store newStore=new Store(id, numberOfEmployees, incomeBalance, numCustomer, advertisingBudget, numberOfDiffrentProducts, arrEmploy, arrProduct);
            return newStore;
        }

        public static Product initProduct(int id)
        {
            Console.WriteLine("Enter the net profit:");
            int netProfit = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the number of products in stock:");
            int numberOfProductsInStock = Convert.ToInt32(Console.ReadLine());

            return new Product(id,netProfit, numberOfProductsInStock);
        }

        public static Employee initEmoloyee(int id)
        {
            Console.WriteLine("Enter the Level Managment:");
            int managementLevel = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the salary:");
            int salary = Convert.ToInt32(Console.ReadLine());

            return new Employee(id,managementLevel, salary);
        }

        private static int GetUserInputAsInt(string s)
        {
            int input;
            while (true)
            {
                Console.Write(s);
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    break; // If successfully parsed, exit the loop
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }
            }
            return input;
        }
    }
}
