
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TasteTrove.Api.Controllers;

[Route("api/food-items")]
public class FoodItemsController : ApiController
{

    [HttpGet]
    public IActionResult ListFoodItems()
    {

        return Ok(Array.Empty<string>());

    }

}
