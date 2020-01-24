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

       public async Task<List <Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync(); // acessa as base e converte a tabela selecionada em lista . sincrona - a aplicação para ate a lista voltar 
        }

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj); //insere objeto no banco de dados 
           await _context.SaveChangesAsync(); // salva alteração 
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                Seller obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e )
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == seller.Id); // busca no banco se existe algum vendedor com o id informado ao chamar o metodo
            if (!hasAny)
            {
                throw new NotFoundException("Id não encontrado!");
            }
            try
            {
                _context.Seller.Update(seller);
               await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}
