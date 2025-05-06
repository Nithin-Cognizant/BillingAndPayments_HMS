namespace BillingAndPayments.Repository.Repositories
{
    using BillingAndPayments.Repository.Interfaces;
    using BillingAndPayments.Repository.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class BillRepository : IBillRepository
    {
        private readonly BillingContext _context;

        public BillRepository(BillingContext context)
        {
            _context = context;
        }

        public Bill GetBillById(int billId)
        {
            return _context.Bills.Find(billId);
        }

        public List<Bill> GetAllBills()
        {
            return _context.Bills.ToList();
        }

        public void AddBill(Bill bill)
        {
            _context.Bills.Add(bill);
        }

        public void UpdateBill(Bill bill)
        {
            _context.Entry(bill).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void DeleteBill(int billId)
        {
            var bill = _context.Bills.Find(billId);
            if (bill != null)
            {
                _context.Bills.Remove(bill);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}