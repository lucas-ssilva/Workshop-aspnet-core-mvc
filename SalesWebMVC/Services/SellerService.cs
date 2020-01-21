using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Services.Exceptions;

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
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            Seller obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller seller)
        {
            if(!_context.Seller.Any(x => x.Id == seller.Id)) // busca no banco se existe algum vendedor com o id informado ao chamar o metodo
            {
                throw new NotFoundException("Id não encontrado!");
            }
            try
            {
                _context.Seller.Update(seller);
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}
