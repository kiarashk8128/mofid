namespace ConsoleApp1.Model;

public class Customer
{
    private string name;
    private int id;
    private int balance;
    private int discountCode=-1;
    private static List<Customer> customers = new List<Customer>();

    public Customer(string name, int id)
    {
        this.name = name;
        this.id = id;
        customers.Add(this);
    }

    public static Customer getCustomerById(int id)
    {
        foreach (Customer customer in customers)
        {
            if (customer.id == id)
            {
                return customer;
            }
        }
        return null;
    }

    public void increaseCustomerBalance(int balance)
    {
        this.balance+=balance;
    }

    public int Balance
    {
        get => balance;
        set => balance = value;
    }

    public int Id => id;

    public int DiscountCode
    {
        get => discountCode;
        set => discountCode = value;
    }

    public void decreaseBalance(int balance)
    {
        this.balance -= balance;
    }

    public string Name
    {
        get => name;
        set => name = value ?? throw new ArgumentNullException(nameof(value));
    }
}