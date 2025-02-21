using Ambev.DeveloperEvaluation.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents an item in a sale transaction, including product details, quantity, unit price, and discount applied.
    /// This entity follows domain-driven design principles and includes business rules for calculating item totals.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale to which the item belongs.
        /// This establishes the relationship between the sale and the items.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets or sets the name of the product in the sale.
        /// This is used to identify the product being sold.
        /// </summary>
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product purchased.
        /// This determines how many units of the product were bought in this transaction.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// This is the price for one unit of the product, before any discounts are applied.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount applied to the product.
        /// The discount is applied based on the quantity of items purchased.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the total amount for the item, after applying the discount.
        /// This value is calculated by multiplying the unit price by the quantity and then applying the discount.
        /// </summary>
        public decimal TotalAmount { get; set; }

        
    }

}

