namespace BillingAndPayments.BusinessLogic.Services
{
    using BillingAndPayments.BusinessLogic.Interfaces;
    using BillingAndPayments.Repository.Interfaces;
    using BillingAndPayments.Repository.Models;
    using System.Collections.Generic;

    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;

        public BillService(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public Bill GetBillById(int billId)
        {
            return _billRepository.GetBillById(billId);
        }

        public List<Bill> GetAllBills()
        {
            return _billRepository.GetAllBills();
        }

        public void SaveBill(Bill bill)
        {
            if (bill.BillId > 0)
            {
                _billRepository.UpdateBill(bill);
            }
            else
            {
                _billRepository.AddBill(bill);
            }
            _billRepository.SaveChanges();
        }

        public void DeleteBill(int billId)
        {
            _billRepository.DeleteBill(billId);
            _billRepository.SaveChanges();
        }
    }
}