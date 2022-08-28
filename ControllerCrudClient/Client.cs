namespace ControllerCrudClient
{
    public class Client
    {
        public string cpf { get; set; }

        public string name { get; set; }

        public DateTime birthDate { get; set; }

        public int age => DateTime.Now.DayOfYear < birthDate.DayOfYear ?
            (DateTime.Now.Year - birthDate.Year) - 1 : (DateTime.Now.Year - birthDate.Year);

    }
}