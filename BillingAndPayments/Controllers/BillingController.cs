using BillingAndPayments.BusinessLogic.Interfaces;
using BillingAndPayments.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace BillingAndPaymentsModule.Controllers
{
    public class BillingController : Controller
    {
        private readonly IBillService _billService; // Declare the field

        // Constructor to inject IBillService
        public BillingController(IBillService billService)
        {
            _billService = billService; // Initialize the field
        }

        public IActionResult Index(int billId = 0)
        {
            Bill bill = new Bill();
            ViewBag.Action = "Submit";

            if (billId > 0)
            {
                var existingBill = _billService.GetBillById(billId);
                if (existingBill != null)
                {
                    bill = existingBill;
                    ViewBag.Action = "Update";
                }
            }

            // Fix for CS0411: Explicitly specify the type argument for Cast<T>()
            ViewBag.PaymentStatusList = new SelectList(Enum.GetValues(typeof(PaymentStatus)).Cast<PaymentStatus>());
            return View(bill);
        }

        [HttpPost]
        public IActionResult Index(Bill bill)
        {
            _billService.SaveBill(bill); // Use the SaveBill method of the service
            return RedirectToAction("Show");
        }

        public IActionResult Show()
        {
            var data = _billService.GetAllBills();
            return View(data);
        }

        public IActionResult Delete(int billId)
        {
            _billService.DeleteBill(billId);
            return RedirectToAction("Show");
        }
    }
}