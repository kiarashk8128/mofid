namespace ConsoleApp1.Model;

public class Transaction
{
    private static int idCounter = 1;
    private int id;
    private int transactionId;
    
    private int customerId;
    private int amount;
    private int discountCode;
    private int discountPrice;
    private int finalPayment;
    private bool isAccepted;
    private static List<Transaction> transactions = new List<Transaction>();

    public Transaction(int customerId, int amount, int discountCode)
    {
        this.customerId = customerId;
        this.amount = amount;
        this.discountCode = discountCode;
        if (discountCode == -1) {
            this.finalPayment = amount;
        } else {
            if (amount >= Confectionary.getDiscountPriceByCode(discountCode)) {
                this.finalPayment = amount - Confectionary.getDiscountPriceByCode(discountCode);
                
            } else {
                this.finalPayment = 0;
            }
            Customer.getCustomerById(customerId).DiscountCode =-1;
        }

        TransactionId = idCounter;
        idCounter++;
        transactions.Add(this);
    }

    public int Id => idCounter;

    public void setAccepted(bool isAccepted) {
        this.isAccepted = isAccepted;
    }
    public bool isTransactionAccepted() {
        return this.isAccepted;
    }
    public void exchangeMoney() {
        int payment = this.finalPayment;
        Customer.getCustomerById(customerId).decreaseBalance(payment);
        Confectionary.increaseBalance(payment);
    }

    public int CustomerId => customerId;

    public int DiscountCode => discountCode;

    public int Amount => amount;

    public int FinalPayment => finalPayment;
    public static Transaction getTransactionById(int id) {
        foreach (Transaction transaction in transactions)
        {
            if (id == transaction.TransactionId)
            {
                return transaction;
            }
        }
        return null;
    }
    public static List<Transaction> getTransactions() {return transactions;}

    public int TransactionId
    {
        get => transactionId;
        set => transactionId = value;
    }
}