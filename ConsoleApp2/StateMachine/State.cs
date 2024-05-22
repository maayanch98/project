public class State
{
    public int numberOfEmployees;
    public int incomeBalance;
    public int numberOfCustomers;
    public int advertisingBudget;
    public int numberOfProducts;
    public Employee[] employeeArr;
    public Product[] productArr;
    public int sumOfSalary;
    public int sumOfSale;
    public int expenses;
    public int revenues;
    public int BusinessEfficiency;

    public State(Store store)
    {
        this.numberOfEmployees = store.numberOfEmployees;
        this.incomeBalance = store.incomeBalance;
        this.numberOfCustomers = store.numberOfCustomers;
        this.advertisingBudget = store.advertisingBudget;
        this.numberOfProducts = store.numberOfProducts;
        this.employeeArr = store.employeeArr;
        this.productArr = store.productArr;
        this.sumOfSalary = calcSumOfSalary(store);
        this.sumOfSale = calcSumOfSale(store);
        this.expenses = calcExpenses(store);
        this.revenues = calcRevenues(store);
        this.BusinessEfficiency= calcBusinessEfficiency(store);
    }

    public static int calcSumOfSalary(Store store)
    {
        int sumOfSalary = 0;
        for (int i = 0; i < store.numberOfEmployees; i++)
        {
            sumOfSalary += store.employeeArr[i].Salary;
        }
        return sumOfSalary;
    }

    public static int calcSumOfSale(Store store)
    {
        int sumOfSale = 0;
        for (int i = 0; i < store.numberOfProducts; i++)
            sumOfSale += store.productArr[i].NetProfit;
        return sumOfSale;
    }
    public static int calcExpenses(Store store)
    {
        return calcSumOfSalary(store) + store.advertisingBudget;
    }

    public static int calcRevenues(Store store)
    {
        return calcSumOfSale(store) + store.incomeBalance;
    }
    public static int calcBusinessEfficiency(Store store)
    {
        return (calcRevenues(store)/calcExpenses(store))*100;
    }

    public static void Enter(Action action)
    { 
    
    }

    public static void Exit(Action action)
    { 
        
    }



}
