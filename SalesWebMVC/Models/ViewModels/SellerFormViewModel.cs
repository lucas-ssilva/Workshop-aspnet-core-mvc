using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models.ViewModels
{
    public class SellerFormViewModel // classe com os dados necessarios para cadastrar um vendedor no caso um vendedor já com todos os dados basicos mas uma lista de departamentos para escolher na tela
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }

    }
}
