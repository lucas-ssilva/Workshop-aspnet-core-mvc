using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMVCContext _context;

        public DepartmentService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync(); // acessa as base e converte a tabela selecionada em lista . sincrona - a aplicação para ate a lista voltar 
        } // usamos o async para evitar que a aplicação fique bloqueada ou seja eu posso fazer outros processamentos enquanto está sendo feita a consulta no BD
    }
}
