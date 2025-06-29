namespace BankATM.Application.DTO
{
    public class BankAccountResponseDTO
    {
        public string AccountNumber { get; set; }

        public string Entity { get; set; }

        public decimal Balance { get; set; }
    }
}
