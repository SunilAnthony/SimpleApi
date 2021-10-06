namespace SimpleApi.Models
{
  public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /* EF Relations */
        public IEnumerable<Order> Orders { get; set; }
    }
}