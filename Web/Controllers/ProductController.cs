using DotNetApi.Model.Cqrs.Products;
using DotNetApi.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DotNetApi.Web.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GetProductsRequestModel model)
        {
            var request = new GetProductsQuery(model.Code, model.Name, model.PageNumber, model.PageSize);
            var products = await _mediator.Send(request);

            return Ok(products);
        }

        [HttpPost]
        [Route("/products", Name = "AddProduct")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add([FromBody] CreateProductCommand command)
        {
            await _mediator.Send(command);
            var product = await _mediator.Send(new GetProductByCodeQuery(command.Code));

            if (product is null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return CreatedAtAction(nameof(Get), new { product.Code }, product);
        }
    }
}
