namespace ConsoleApp1.Model;

using System.Collections;

public class Sweet
{
    private string name;
    private int price;
    private int amount;
    private SortedList<string, int> materials = new SortedList<string, int>();
    private static List<Sweet> sweets=new List<Sweet>();

    public Sweet(string name, int price, SortedList<string, int> materials)
    {
        this.name = name;
        this.price = price;
        this.materials = materials;
        sweets.Add(this);
    }

    public SortedList<string, int> getMaterials()
    {
        return this.materials;
    }

    public string Name
    {
        get => name;
        set => name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Price
    {
        get => price;
        set => price = value;
    }

    public int Amount
    {
        get => amount;
        set => amount = value;
    }

    public void increaseSweets(int amount)
    {
        this.amount += amount;
    }

    public void decreasematerialOfSweetfromWarehouse(int amount)
    {
        foreach (string materialName in this.materials.Keys)
        {
            Warehouse warehouse = Warehouse.getWarehouseByName(materialName);
            warehouse.Amount -= this.getMaterials()[materialName] * amount;
        }
    }

    public static Sweet getSweetByName(string name)
    {
        if (sweets.Count == 0)
        {
            return null;
        }
        foreach (Sweet sweet in sweets)
        {
            if (sweet.name == name)
            {
                return sweet;
            }
        }


        return null;
    }
}