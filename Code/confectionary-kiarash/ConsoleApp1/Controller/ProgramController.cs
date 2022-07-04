using ConsoleApp1.Model;
using System;
using System.Text.RegularExpressions;
using System.Collections;


namespace ConsoleApp1;

public class ProgramController
{
    bool check = true;

    public void run()
    {
        while (true)
        {
            string firstCommand = Console.ReadLine().Trim();
            if (!firstCommand.Equals("create confectionary"))
            {
                Console.WriteLine("invalid command");
                continue;
            }
            else
            {
                break;
            }
        }

        Confectionary confectionary = new Confectionary();
        string command = Console.ReadLine().Trim();
        while (!command.Equals("end"))
        {
            if (Regex.IsMatch(command, "add customer id (\\d+) name ([A-Za-z]+)"))
            {
                Match match = commandMatcher(command, "add customer id (\\d+) name ([A-Za-z]+)");
                addCustomer(int.Parse(match.Groups[1].Value), Convert.ToString(match.Groups[2].Value));
            }
            else if (Regex.IsMatch(command, "increase balance customer (\\d+) amount (\\d+)"))
            {
                Match match = commandMatcher(command, "increase balance customer (\\d+) amount (\\d+)");
                chargeCustomerBalance(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            }
            else if (Regex.IsMatch(command, "add warehouse material ([A-Za-z]+) amount (\\d+)"))

            {
                Match match = commandMatcher(command, "add warehouse material ([A-Za-z]+) amount (\\d+)");
                addWarehouse(Convert.ToString(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            }
            else if (Regex.IsMatch(command, "increase warehouse material ([A-Za-z]+) amount (\\d+)"))
            {
                Match match = commandMatcher(command, "increase warehouse material ([A-Za-z]+) amount (\\d+)");
                increaseWarehouseMaterials(Convert.ToString(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            }
            else if (Regex.IsMatch(command, "add sweet name ([A-Za-z]+) price (\\d+) materials: (.+)"))
            {
                Match match = Regex.Match(command, "add sweet name ([A-Za-z]+) price (\\d+) materials: (.+)");
                addSweet(match);
            }
            else if (Regex.IsMatch(command, "increase sweet ([A-za-z]+) amount (\\d+)"))
            {
                Match match = Regex.Match(command, "increase sweet ([A-za-z]+) amount (\\d+)");
                increaseSweet(Convert.ToString(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            }
            else if (Regex.IsMatch(command, "add discount code (\\d+) price (\\d+)"))
            {
                Match match = Regex.Match(command, "add discount code (\\d+) price (\\d+)");
                addDiscount(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            }
            else if (Regex.IsMatch(command, "add discount code (\\d+) to customer id (\\d+)"))
            {
                Match match = Regex.Match(command, "add discount code (\\d+) to customer id (\\d+)");
                addDiscountToCustomer(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            }
            else if (Regex.IsMatch(command, "sell sweet ([A-Za-z]+) amount (\\d+) to customer (\\d+)"))
            {
                Match match = Regex.Match(command, "sell sweet ([A-Za-z]+) amount (\\d+) to customer (\\d+)");
                sellSweet(Convert.ToString(match.Groups[1].Value), int.Parse(match.Groups[2].Value),int.Parse(match.Groups[3].Value));
            }
            else if (Regex.IsMatch(command, "accept transaction (\\d+)"))
            {
                Match match = Regex.Match(command, "accept transaction (\\d+)");
                acceptTransaction(int.Parse(match.Groups[1].Value));
            }
            else if (command.Equals("print transactions list"))
            {
                printTransactions();
            }
            else if (command.Equals("print income"))
            {
                printIncome();
            }
            else
            {
                Console.WriteLine("invalid command");
            }

            command = Console.ReadLine().Trim();
        }
    }

    private void addCustomer(int id, string name)
    {
        if (check)
        {
            Customer customer = new Customer(name, id);
            check = false;
        }
        else
        {
            if (Customer.getCustomerById(id) != null)
            {
                Console.WriteLine("customer with this id already exists");
            }
            else
            {
                Customer customer = new Customer(name, id);
            }
        }
    }

    private void chargeCustomerBalance(int id, int amount)
    {
        if (Customer.getCustomerById(id) == null)
        {
            Console.WriteLine("customer not found");
        }
        else
        {
            Customer.getCustomerById(id).increaseCustomerBalance(amount);
            Console.WriteLine(Customer.getCustomerById(id).Balance);
        }
    }

    private void addWarehouse(string name, int amount)
    {
        if (Warehouse.getWarehouseByName(name) != null)
        {
            Console.WriteLine("warehouse having this material already exists");
        }
        else
        {
            Warehouse warehouse = new Warehouse(name, amount);
        }
    }

    private void increaseWarehouseMaterials(string name, int amount)
    {
        if (Warehouse.getWarehouseByName(name) == null)
        {
            Console.WriteLine("warehouse not found");
        }
        else
        {
            Warehouse.getWarehouseByName(name).increaseMaterials(amount);
        }
    }

    private void addSweet(Match match)
    {
        string name = match.Groups[1].Value;
        if (Sweet.getSweetByName(name) == null)
        {
            string sweetName = Convert.ToString(match.Groups[1].Value);
            int price = int.Parse(match.Groups[2].Value);
            string[] materials = match.Groups[3].Value.Split(",");
            SortedList<string, int> materialList = new SortedList<string, int>();
            List<string> notFoundMaterial = new List<string>();
            for (int i = 0; i < materials.Length; i++)
            {
                
                if (Regex.IsMatch(materials[i], "([A-Za-z ]+)([0-9]+)"))
                {
                    Match matcher = commandMatcher(materials[i], "([A-Za-z ]+)([0-9]+)");
                    
                    string materialName = matcher.Groups[1].Value.Trim();
                    int amountOfMaterial = int.Parse(matcher.Groups[2].Value);
                    Console.WriteLine(materialName);
                    Console.WriteLine(amountOfMaterial);
                    if (Warehouse.getWarehouseByName(materialName) != null)
                    {
                        
                        materialList.Add(materialName, amountOfMaterial);
                    }
                    else
                    {
                        notFoundMaterial.Add(materialName);
                    }
                }
            }

            if (notFoundMaterial.Count != 0)
            {
                Console.Write("not found warehouse(s): ");
                for (int j = 0; j < notFoundMaterial.Count; j++)
                {
                    Console.Write(notFoundMaterial[j] + " ");
                }

                return;
            }
            else
            {
                Sweet sweet = new Sweet(sweetName, price, materialList);
                return;
            }
        }
        else
        {
            Console.WriteLine("sweet already exists!");
            return;
        }
    }

    private void increaseSweet(string name, int amount)
    {
        if (Sweet.getSweetByName(name) == null)
        {
            Console.WriteLine("sweet not found!");
            return;
        }
        else
        {
            List<string> notFoundMaterial = new List<string>();
            SortedList<string, int> needed = Sweet.getSweetByName(name).getMaterials();
            foreach (var foundMaterial in needed)
            {
                if (foundMaterial.Value * amount > Warehouse.getWarehouseByName(foundMaterial.Key).Amount)
                {
                    notFoundMaterial.Add(foundMaterial.Key);
                }
            }

            if (notFoundMaterial.Count > 0)
            {
                Console.Write("insufficient material(s): ");
                for (int j = 0; j < notFoundMaterial.Count; j++)
                {
                    Console.Write(notFoundMaterial[j] + " ");
                }
            }
            else
            {
                foreach (var foundMaterial in needed)
                {
                    Warehouse.getWarehouseByName(foundMaterial.Key).Amount-=foundMaterial.Value * amount;

                }
                Sweet.getSweetByName(name).increaseSweets(amount);
            }
        }
    }

    private void addDiscount(int code, int price)
    {
        if (Confectionary.isDiscountExist(code))
        {
            Console.WriteLine("discount code already exists");
        }
        else
        {
            Confectionary.addDiscount(code, price);
        }
    }

    private void addDiscountToCustomer(int code, int id)
    {
        if (Confectionary.isDiscountExist(code))
        {
            if (Customer.getCustomerById(id) != null)
            {
                Customer.getCustomerById(id).DiscountCode = code;
            }
            else
            {
                Console.WriteLine("customer with this id does not exist!");
            }
        }
        else
        {
            Console.WriteLine("discount code does not exist!");
        }
    }

    private void sellSweet(string sweetName, int amount , int customerId)
        {
            if (Sweet.getSweetByName(sweetName) != null)
            {
                if (Sweet.getSweetByName(sweetName).Amount >= amount)
                {
                    if (Customer.getCustomerById(customerId) != null)
                    {
                        if (Customer.getCustomerById(customerId).Balance >=
                            Sweet.getSweetByName(sweetName).Price * amount)
                        {
                            Transaction transaction =new Transaction(customerId,Sweet.getSweetByName(sweetName).Price * amount,Customer.getCustomerById(customerId).DiscountCode);
                                Sweet.getSweetByName(sweetName).Amount -= amount;
                                
                                Console.WriteLine("transaction" + transaction.TransactionId + " successfully created");
                        }
                        else
                        {
                            Console.WriteLine("customer doesn't have enough money");
                        }
                    }
                    else
                    {
                        Console.WriteLine("customer not found");
                    }
                }
                else
                {
                    Console.WriteLine("insufficient sweet");
                }
            }
            else
            {
                Console.WriteLine("sweet not found");
            }
        }

    private void acceptTransaction(int transactionId)
    {
        if (Transaction.getTransactionById(transactionId) == null)
        {
            Console.WriteLine("no waiting transaction with this id was found");
        }
        else
        {
            if (!Transaction.getTransactionById(transactionId).isTransactionAccepted())
            {
                Transaction.getTransactionById(transactionId).exchangeMoney();
                Transaction.getTransactionById(transactionId).setAccepted(true);
            }
        }
    }

    private void printTransactions()
    {
        
        foreach (Transaction tx in Transaction.getTransactions())
        {
            Console.WriteLine("transaction "+tx.TransactionId+" "+ tx.CustomerId + " "+ tx.Amount+ " "+ tx.DiscountCode+" "+tx.FinalPayment);
        }
    }

    private void printIncome()
    {
        Console.WriteLine(Confectionary.Balance);
    }
    


    private Match commandMatcher(string command, string pattern)
    {
        Match match = Regex.Match(command, pattern);
        return match;
    }
}