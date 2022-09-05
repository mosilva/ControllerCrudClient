using System.ComponentModel.DataAnnotations;

namespace ControllerCrudClient.Core
{
    public class Client
    {
        [Required(ErrorMessage = "CPF � obrigat�rio!")]
        public string cpf { get; set; }

        [MaxLength(100, ErrorMessage = "O nome � muito grande, ele s� pode ter 100 caracteres")]
        [MinLength(3, ErrorMessage = "O nome � muito pequeno, ele tem que ter 3 caracteres")]
        public string nome { get; set; }

        [Required(ErrorMessage = "A data de nascimento � obrigat�ria!")]
        public DateTime dataNascimento { get; set; }

        public int idade => DateTime.Now.DayOfYear < dataNascimento.DayOfYear ?
            (DateTime.Now.Year - dataNascimento.Year) - 1 : (DateTime.Now.Year - dataNascimento.Year);

    }
}