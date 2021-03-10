namespace CQRS.Account.Models
{
    public class CreateAccountModel
    {
        public string Iban { get; set; }
        public string Type { get; set; }
    }
}
