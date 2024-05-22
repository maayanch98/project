
public class Action
{
    public ActionType Type;
    public int PercentageChange;
    public bool isMorePercentage;

    public Action(ActionType type, int percentageChange, bool isMorePercentage, State state)
    {
        Type = type;
        PercentageChange = percentageChange;
        this.isMorePercentage = isMorePercentage;
    }
}

public enum ActionType
{
    AdvertisingBudget,
    SalaryEmployee,
    ProductPrices,
    ProductStock,
    NumberDifferentProducts,
    NumberOfEmployees
}
