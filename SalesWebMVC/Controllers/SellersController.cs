using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        [HttpPost] //indica que é post e não get , post pode mexer e atualizar banco 
        [ValidateAntiForgeryToken] // segurança , previne que usem sua sesão aberta para enviar dados 
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller); //insere no banco
            return RedirectToAction(nameof(Index)); //redireciona o usuario para a tela index 
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if(id == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost] //indica que é post e não get , post pode mexer e atualizar banco 
        [ValidateAntiForgeryToken] // segurança , previne que usem sua sesão aberta para enviar dados 
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id); //remove do banco
            return RedirectToAction(nameof(Index)); //redireciona o usuario para a tela index 

        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (id == null)
            {
                return NotFound();
            }
            return View(obj);
        }
    }
}