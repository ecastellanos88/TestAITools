// Controllers/ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using ProductApiDemo.Models;
using ProductApiDemo.Services;

namespace ProductApiDemo.Controllers;

/// <summary>
/// Controller for managing product operations including CRUD operations
/// </summary>
[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _service;

    public ProductsController(ProductService service)
    {
        _service = service;
    }

    public IActionResult GetById(int id)
    {
        var product = _service.GetById(id);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public IActionResult Add(Product product)
    {
        _service.Add(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Product updated)
    {
        return _service.Update(id, updated) ? NoContent() : NotFound();
    }
}
