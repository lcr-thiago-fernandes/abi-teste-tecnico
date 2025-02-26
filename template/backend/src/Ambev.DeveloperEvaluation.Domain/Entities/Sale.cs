

using Ambev.DeveloperEvaluation.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale transaction with its details, including items, customer information, and the total amount.
    /// This entity follows domain-driven design principles and includes business rules for calculating the sale's total amount.
    /// </summary>
    public class Sale : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the Sale class.
        /// </summary>
        public Sale()
        {
            Items = new List<SaleItem>();
        }

        /// <summary>
        /// Gets or sets the unique number that identifies the sale.
        /// This number is typically used for reference purposes in the system.
        /// </summary>
        public int SaleNumber { get; set; }

        /// <summary>
        /// Gets or sets the date when the sale was made.
        /// This timestamp is critical for identifying when the transaction occurred.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer who made the purchase.
        /// This is used for identifying the buyer and linking the sale to a customer record.
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the sale.
        /// This is the sum of the individual item totals after applying discounts.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the branch where the sale took place.
        /// This helps in identifying the location of the transaction.
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Gets or sets the list of items included in the sale.
        /// Each item represents a product purchased and includes its quantity, price, and any discounts applied.
        /// </summary>
        public List<SaleItem> Items { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sale is cancelled.
        /// If set to true, the sale is considered cancelled and should not be processed further.
        /// </summary>
        public bool IsCancelled { get; set; }


        /// <summary>
        /// Applies the discount based on the quantity of items purchased.
        /// The discount is applied as follows:
        /// - 10% for quantities between 4 and 9 items.
        /// - 20% for quantities between 10 and 20 items.
        /// - Throws an exception if the quantity exceeds 20 items.
        /// </summary>
        public void ApplyDiscount()
        {
            if(Items.Sum(x=>x.Quantity) > 20) 
                throw new InvalidOperationException("Não é permitido vender mais de 20 unidades de um item.");

            decimal totalAmount = 0;

            foreach (var item in Items)
            {
                if (item.Quantity >= 4 && item.Quantity <= 9)
                {
                    item.Discount = 0.10m; // 10% discount for 4 to 9 items
                }
                else if (item.Quantity >= 10 && item.Quantity <= 20)
                {
                    item.Discount = 0.20m; // 20% discount for 10 to 20 items
                }

                TotalAmount = TotalAmount + (item.Quantity * item.UnitPrice * (1 - item.Discount));

            }
        }
    }
}