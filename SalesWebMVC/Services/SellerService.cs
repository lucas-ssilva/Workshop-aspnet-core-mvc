using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

       public List <Seller> FindAll()
        {
            return _context.Seller.ToList(); // acessa as base e converte a tabela selecionada em lista . sincrona - a aplicação para ate a lista voltar 
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj); //insere objeto no banco de dados 
            _context.SaveChanges(); // salva alteração 
        }
    }
}
