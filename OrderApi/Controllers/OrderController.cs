using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Repository;
using Shared.DTOs;
using Shared.Models;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrder orderInterface) : ControllerBase
    {
        [HttpPost]

        public async Task<ActionResult<ServiceResponse>> AddOrder(Order order)
        {
            var response = await orderInterface.AddOrderAsync(order);
            return response.Flag?Ok(response):BadRequest(response);
            
        }
    }
}
