public class Employee
{
    public int Id { get; set; }
    public int ManagementLevel;
    public int Salary;

    public Employee(int id,int managementLevel, int salary)
    {
        Id = id;
        ManagementLevel = managementLevel;
        Salary = salary;
    }

    public override string ToString()
    {
        return $"Management level: {ManagementLevel}\n" +
               $"Salary: {Salary}";
    }
}
