namespace ConsoleApp1.Model;

public class Warehouse
{
    private int amount;
    private string materialName;
    private static List<Warehouse> warehouses = new List<Warehouse>();

    public Warehouse(string materialName, int amount)
    {
        this.materialName = materialName;
        this.amount = amount;
        warehouses.Add(this);
    }

    public void increaseMaterials(int amount)
    {
        this.amount += amount;
    }

    public int Amount
    {
        get => amount;
        set => amount = value;
    }

    public void decreaseMaterials(int amount)
    {
        this.amount -= amount;
    }

    public static Warehouse getWarehouseByName(string name)
    {
        foreach (Warehouse warehouse in warehouses)
        {
            if (warehouse.materialName == name)
            {
                return warehouse;
            }
        }
        return null;
    }
}