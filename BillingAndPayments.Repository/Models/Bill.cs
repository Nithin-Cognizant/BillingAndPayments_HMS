using System.ComponentModel.DataAnnotations;

namespace BillingAndPayments.Repository.Models
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }
        public int PatientId { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime BillDate { get; set; }
    }


    public enum PaymentStatus
    {
        PAID,
        UNPAID
    }

}
