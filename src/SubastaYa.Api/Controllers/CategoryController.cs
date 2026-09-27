using MediatR;
using Microsoft.AspNetCore.Mvc;
using SubastaYa.Aplication.UseCases.Categories.Commands.CreateCommand;
using SubastaYa.Aplication.UseCases.Categories.Commands.DeleteCommand;
using SubastaYa.Aplication.UseCases.Categories.Commands.UpdateCommand;
using SubastaYa.Aplication.UseCases.Categories.Queries.GetAllQuery;
using SubastaYa.Aplication.UseCases.Categories.Queries.GetByIdQuery;

namespace SubastaYa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ISender _sender;

        public CategoryController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var response = await _sender.Send(new GetAllCategoryQuery());
            return Ok(response);
        }

        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var response = await _sender.Send(new GetCategoryByIdQuery { Id = id });
            return Ok(response);
        }

        [HttpPost("categories")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var response = await _sender.Send(command);
            return Ok(response);
        }

        [HttpPut("categories/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryCommand command)
        {
            command.Id = id;
            var response = await _sender.Send(command);
            return Ok(response);
        }

        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = await _sender.Send(new DeleteCategoryCommand { Id = id });
            return Ok(response);
        }
    }
}
