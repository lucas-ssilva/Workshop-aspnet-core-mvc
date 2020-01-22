using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo {0} é Obrigatorio")]  // definir que o campo é obrigatorio 
        [StringLength(60,MinimumLength = 3,ErrorMessage = "Tamanho invalido para o campo {0}, deve estar entre {2} e {1} caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatorio")]  // definir que o campo é obrigatorio 
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "E-mail Inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo {0} é Obrigatorio")]  // definir que o campo é obrigatorio 
        [Display(Name = "Data de Aniversário")] // altera como o nome da classe é mostrado no front
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        [Range(100.0, 50000.0, ErrorMessage = "{0} deve ter um valor entre {1} e {2}")]
        [Required(ErrorMessage = "Campo {0} é Obrigatorio")]  // definir que o campo é obrigatorio 
        [Display(Name = "Salário Base")]
        [DisplayFormat(DataFormatString = "{0:F2}")] // altera para duas casas decimais 
        public double BaseSalary { get; set; }

        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;

        }

        public void AddSalles(SalesRecord SR)
        {
            Sales.Add(SR);
        }
        public void RemoveSalles(SalesRecord SR)
        {
            Sales.Remove(SR);
        }
        public virtual double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(x => x.Date >= initial && x.Date <= final).Sum(x => x.Amount);
        }
    }
}
