using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost] //indica que é post e não get , post pode mexer e atualizar banco 
        [ValidateAntiForgeryToken] // segurança , previne que usem sua sesão aberta para enviar dados 
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller); //insere no banco
            return RedirectToAction(nameof(Index)); //redireciona o usuario para a tela index 
        }
    }
}