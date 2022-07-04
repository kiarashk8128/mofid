using System.Collections;

namespace ConsoleApp1.Model;

public class Confectionary
{
    private static int balance;
    private static SortedList<int, int> discounts=new SortedList<int, int>();

    public Confectionary()
    {
    }

    public static int Balance
    {
        get => balance;
        set => balance = value;
    }

    public static void increaseBalance(int amount)
    {
       balance += amount;
    }

    public static bool isDiscountExist(int code)
    {
        if (discounts.ContainsKey(code))
        {
            return true;
        }
        return false;
    }

    public static void addDiscount(int code, int price)
    {
        discounts.Add(code, price);
    }

    public static int getDiscountPriceByCode(int code)
    {
        if(code==-1){
            return 0;
        }
        return discounts[code];
    }
}