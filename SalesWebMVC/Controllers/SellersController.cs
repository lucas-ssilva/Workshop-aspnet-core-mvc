using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;


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
                return RedirectToAction(nameof(Error),new {message = "Id não pode ser nulo, Tente de novo" });
            }
            var obj = _sellerService.FindById(id.Value);
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
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
                return RedirectToAction(nameof(Error), new { message = "Id não pode ser nulo, Tente de novo" });
            }
            var obj = _sellerService.FindById(id.Value);
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não pode ser nulo, Tente de novo" });
            }
            var obj = _sellerService.FindById(id.Value);
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewmodel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewmodel);
        }

        [HttpPost] //indica que é post e não get , post pode mexer e atualizar banco 
        [ValidateAntiForgeryToken] // segurança , previne que usem sua sesão aberta para enviar dados 
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Informado na requisição não bate com o ID do vendedor" });
            }
            try
            {
                _sellerService.Update(seller); //remove do banco
                return RedirectToAction(nameof(Index)); //redireciona o usuario para a tela index
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message});
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
            }
    }
    }