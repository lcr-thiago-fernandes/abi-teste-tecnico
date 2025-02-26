using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;


/// <summary>
/// Implementation of ISaleRepository using Entity Framework Core
/// </summary>
public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SaleRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new sale in the database
    /// </summary>
    /// <param name="sale">The sale to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale</returns>
    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    /// <summary>
    /// Deletes a sale from the database, including its items
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the sale was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await _context.Sales
                                 .Include(s => s.Items)
                                 .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (sale == null)
        {
            return false;
        }

        _context.SaleItems.RemoveRange(sale.Items);
        _context.Sales.Remove(sale);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    /// <summary>
    /// Retrieves a sale by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
                                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a list of all sales in the database
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of sales</returns>
    public async Task<IEnumerable<Sale>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Sales
                                .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Updates an existing sale in the database
    /// </summary>
    /// <param name="sale">The sale to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated sale</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the sale is not found</exception>
    public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        var existingSale = await _context.Sales
                                         .FirstOrDefaultAsync(s => s.Id == sale.Id, cancellationToken);

        if (existingSale == null)
        {
            throw new KeyNotFoundException("Sale not found");
        }

        existingSale.SaleNumber = sale.SaleNumber;
        existingSale.SaleDate = sale.SaleDate;
        existingSale.Customer = sale.Customer;
        existingSale.TotalAmount = sale.TotalAmount;
        existingSale.Branch = sale.Branch;
        existingSale.IsCancelled = sale.IsCancelled;

        foreach (var item in sale.Items)
        {
            var existingItem = existingSale.Items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Product = item.Product;
                existingItem.Quantity = item.Quantity;
                existingItem.UnitPrice = item.UnitPrice;
                existingItem.Discount = item.Discount;
                existingItem.TotalAmount = item.TotalAmount;
            }
            else
            {
                existingSale.Items.Add(item);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return existingSale;
    }
}
