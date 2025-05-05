using Microsoft.AspNetCore.Mvc;
using BillingAndPayments.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace BillingAndPaymentsModule.Controllers
{
    public class BillingController : Controller
    {
        public readonly BillingContext _context;

        public BillingController(BillingContext context)
        {
            _context = context;
        }

        public IActionResult Index(int billId = 0)
        {
            Bill bill = new Bill();
            ViewBag.Action = "Submit";
            if (billId > 0)
            {
                var data = (from b in _context.Bills
                            where b.BillId == billId
                            select b).ToList();
                if (billId > 0)
                {
                    bill.BillId = data[0].BillId;
                    bill.PatientId = data[0].PatientId;
                    bill.TotalAmount = data[0].TotalAmount;
                    bill.PaymentStatus = data[0].PaymentStatus;
                    bill.BillDate = data[0].BillDate;
                    ViewBag.Action = "Update";
                }
            }

            ViewBag.PaymentStatusList = new SelectList(Enum.GetValues(typeof(PaymentStatus)).Cast<PaymentStatus>());
            return View(bill);
        }

        [HttpPost]
        public IActionResult Index(Bill bill)
        {
            if (bill.BillId > 0)
            {
                _context.Entry(bill).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                _context.Bills.Add(bill);
            }

            _context.SaveChanges();
            return RedirectToAction("Show");
        }

        public IActionResult Show()
        {
            var data = _context.Bills.ToList();
            return View(data);
        }

        public IActionResult Delete(int billId)
        {
            var bill = _context.Bills.Find(billId);
          
                _context.Bills.Remove(bill);
               _context.SaveChanges();
            
            return RedirectToAction("Show");
        }
    }
}
