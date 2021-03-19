namespace FirstApplication.Model
{
    public class CustomerModel
    {
        //public ProductModel()
        //{
        //}
        public int Id { get; set; }

        public int  Age {get; set; }

        public string Address { get; set; }

        

        public string Name { get; set; }
        public decimal Salary { get; set; }

        public int RequestedLimit { get; set; }

        public string TCKN { get; set; }

        public bool Account { get; set; }

        public int OfferedLimit { get; set; }

        public int CreditScore { get; set; }
    }
}