using EnterpriseCommerce.Application.Features.Products.Commands;
using EnterpriseCommerce.Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EnterpriseCommerce.Application.Features.Products.Commands;
using EnterpriseCommerce.Application.Features.Products.Queries;
using EnterpriseCommerce.Application.Features.Products;

namespace EnterpriseCommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }
}