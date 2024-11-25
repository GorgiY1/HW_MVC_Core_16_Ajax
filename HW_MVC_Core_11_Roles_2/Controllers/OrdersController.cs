using HW_MVC_Core_11_Roles_2.Models;
using HW_MVC_Core_11_Roles_2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HW_MVC_Core_11_Roles_2.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProductsService _orderProductsService;

        public OrdersController(IOrderProductsService orderProductsService)
        {
            _orderProductsService = orderProductsService;
        }

        // GET: api/Orders
        [HttpGet("Read")]
        public async Task<JsonResult> GetAllOrderProducts()
        {
            var result = await _orderProductsService.ReadAsync();

            // Configure JSON serialization to handle circular references
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true // Optional: Makes the JSON output more readable
            };

            return new JsonResult(result, options);
        }

        // GET: api/Orders/{id}
        [HttpGet("{id}")]
        public async Task<JsonResult> GetOrderProduct(Guid? id)
        {
            var result = await _orderProductsService.GetByIdAsync(id);
            if (result == null)
            {
                return new JsonResult(new { message = "Not Found" }) { StatusCode = 404 };
            }
            // Configure JSON serialization to handle circular references
            // Configure JSON serialization to handle circular references
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true // Optional: Makes the JSON output more readable
            };

            return new JsonResult(result, options);
        }

        // POST: api/Orders/Create
        [HttpPost("Create")]
        public async Task<JsonResult> CreateOrderProduct([FromBody] OrderProducts orderProduct)
        {
            if (orderProduct == null)
            {
                return new JsonResult(new { message = "Bad Request" }) { StatusCode = 400 };
            }

            var createdOrderProduct = await _orderProductsService.CreateAsync(orderProduct);
            return new JsonResult(createdOrderProduct) { StatusCode = 201 };
        }

        // PUT: api/Orders/{id}
        [HttpPut("{id}")]
        public async Task<JsonResult> UpdateOrderProduct(Guid? id, [FromBody] OrderProducts orderProduct)
        {
            if (orderProduct == null || id != orderProduct.Id)
            {
                return new JsonResult(new { message = "Bad Request" }) { StatusCode = 400 };
            }

            var updatedOrderProduct = await _orderProductsService.UpdateAsync(id, orderProduct);
            if (updatedOrderProduct == null)
            {
                return new JsonResult(new { message = "Not Found" }) { StatusCode = 404 };
            }
            // Configure JSON serialization to handle circular references
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true // Optional: Makes the JSON output more readable
            };

            return new JsonResult(updatedOrderProduct, options);
        }

        // DELETE: api/Orders/{id}
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteOrderProduct(Guid id)
        {
            var success = await _orderProductsService.DeleteAsync(id);
            if (!success)
            {
                return new JsonResult(new { message = "Not Found" }) { StatusCode = 404 };
            }

            return new JsonResult(new { message = "No Content" }) { StatusCode = 204 };
        }
    }


}
