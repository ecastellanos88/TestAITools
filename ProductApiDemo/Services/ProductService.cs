using ProductApiDemo.Models;

namespace ProductApiDemo.Services;

public class ProductService
{
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop", Price = 1200.50m, InStock = true },
        new Product { Id = 2, Name = "Phone", Price = 650.00m, InStock = false },
    };
    
    public IEnumerable<Product> GetAll() => _products;
    
    public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public void Add(Product product)
    {
        product.Id = _products.Max(p => p.Id) + 1;
        _products.Add(product);
    }

    public bool Update(int id, Product updated)
    {
        var product = GetById(id);
        if (product == null) return false;

        product.Name = updated.Name;
        product.Price = updated.Price;
        product.InStock = updated.InStock;
        return true;
    }
}