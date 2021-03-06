﻿using System;
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

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        [HttpPost] //indica que é post e não get , post pode mexer e atualizar banco 
        [ValidateAntiForgeryToken] // segurança , previne que usem sua sesão aberta para enviar dados 
        public async Task<IActionResult> Create(Seller seller)
        {
            if(!ModelState.IsValid) // dupla proteção - caso o JS esteja desabilitado previne cadastros que não estão cumprindo as regras dos campos 
            {
                var departments = await _departmentService.FindAllAsync();
                var viewmodel = new SellerFormViewModel { Departments = departments, Seller = seller };
                return View(viewmodel);
            }
           await _sellerService.InsertAsync(seller); //insere no banco
            return RedirectToAction(nameof(Index)); //redireciona o usuario para a tela index 
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error),new {message = "Id não pode ser nulo, Tente de novo" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            return View(obj);
        }

        [HttpPost] //indica que é post e não get , post pode mexer e atualizar banco 
        [ValidateAntiForgeryToken] // segurança , previne que usem sua sesão aberta para enviar dados 
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id); //remove do banco
                return RedirectToAction(nameof(Index)); //redireciona o usuario para a tela index 
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = "Não é possivel deletar, Vendedor possui vendas Ativas" });
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não pode ser nulo, Tente de novo" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            return View(obj);
        }

        public async Task< IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não pode ser nulo, Tente de novo" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewmodel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewmodel);
        }

        [HttpPost] //indica que é post e não get , post pode mexer e atualizar banco 
        [ValidateAntiForgeryToken] // segurança , previne que usem sua sesão aberta para enviar dados 
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid) // dupla proteção - caso o JS esteja desabilitado previne cadastros que não estão cumprindo as regras dos campos 
            {
                var departments = await _departmentService.FindAllAsync();
                var viewmodel = new SellerFormViewModel { Departments = departments, Seller = seller };
                return View(viewmodel);
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Informado na requisição não bate com o ID do vendedor" });
            }
            try
            {
               await _sellerService.UpdateAsync(seller); //remove do banco
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