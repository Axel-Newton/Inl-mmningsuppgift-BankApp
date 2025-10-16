namespace Bankapp.Domain
{
    public enum TransactionType
    {
        Deposit,
        Withdraw,
        TransferIn,
        TransferOut
    }
    
    public class Transaction
    {
        public Guid Id { get; set; } =Guid.NewGuid();
        public Guid? FromAccountId { get; set; }
        public Guid? ToAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        public TransactionType TransactionType { get; set; } 
        public string Description { get; set; }

        public Transaction(Guid fromAccountId, Guid toAccountId, decimal amount, DateTime timeStamp, string description, TransactionType transactionType)
        {
            FromAccountId = fromAccountId;
            ToAccountId = toAccountId;
            Amount = amount;
            TimeStamp = DateTime.Now;
            Description = description;
            TransactionType = transactionType;
        }
    }
}

