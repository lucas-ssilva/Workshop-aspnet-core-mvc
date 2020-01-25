using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMVC.Models
{
    public class SalesRecord
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "0:dd/MM/yyyy")]
        [Display(Name = "Data")]
        public DateTime Date { get; set; }
        [Display(Name = "Valor")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double Amount { get; set; }
        [Display(Name = "Status")]
        public SaleStatus Status { get; set; }
        [Display(Name = "Vendedor")]
        public Seller Seller { get; set; }

        public SalesRecord() { }

        public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}
