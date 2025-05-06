namespace BillingAndPayments.Repository.Interfaces
{
    using BillingAndPayments.Repository.Models;
    using System.Collections.Generic;

    public interface IBillRepository
    {
        Bill GetBillById(int billId);
        List<Bill> GetAllBills();
        void AddBill(Bill bill);
        void UpdateBill(Bill bill);
        void DeleteBill(int billId);
        void SaveChanges();

    }
}