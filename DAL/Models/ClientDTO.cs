namespace DAL.Models
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string Surname { get; set; }

        public override string ToString()
        {
            return Surname.ToString();
        }
    }
}
