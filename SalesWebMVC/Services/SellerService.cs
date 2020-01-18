using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id)
        {
            Seller obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}
