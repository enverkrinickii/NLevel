namespace DAL.Models
{
    public class ManagerDTO
    {
        public int Id { get; set; }
        public string Surname { get; set; }

        public override string ToString()
        {
            return Surname;
        }
    }
}
