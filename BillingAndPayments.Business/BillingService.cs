using BillingAndPayments.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq


namespace BillingAndPayments.Business
{
    public class BillingService
    {

        private readonly BillingContext _context;

        public BillingService(BillingContext context)
        {
            _context = context;
        }

        public Bill GetBillById(int billId)
        {
            return _context.Bills.FirstOrDefault(b => b.BillId == billId);
        }

        public List<Bill> GetAllBills()
        {
            return _context.Bills.ToList();
        }

        public void SaveBill(Bill bill)
        {
            if (bill.BillId > 0)
            {
                _context.Entry(bill).State = EntityState.Modified;
            }
            else
            {
                _context.Bills.Add(bill);
            }
            _context.SaveChanges();
        }

        public void DeleteBill(int billId)
        {
            var bill = _context.Bills.Find(billId);
            if (bill != null)
            {
                _context.Bills.Remove(bill);
                _context.SaveChanges();
            }
        }
    }

}
}
