using System.ComponentModel.DataAnnotations;

namespace ControllerCrudClient
{
    public class Client
    {
        [Required(ErrorMessage = "CPF é obrigatório!")]
        public string cpf { get; set; }

        [MaxLength(100, ErrorMessage = "O nome é muito grande, ele só pode ter 100 caracteres")]
        [MinLength(3, ErrorMessage = "O nome é muito pequeno, ele tem que ter 3 caracteres")]
        public string name { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória!")]
        public DateTime birthDate { get; set; }

        public int age => DateTime.Now.DayOfYear < birthDate.DayOfYear ?
            (DateTime.Now.Year - birthDate.Year) - 1 : (DateTime.Now.Year - birthDate.Year);

    }
}