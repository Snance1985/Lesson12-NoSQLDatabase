using l12_nosql.Models;
using l12_nosql.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace l12_nosql.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoffeeController : ControllerBase
{
    private readonly ILogger<CoffeeController> _logger;
    private readonly ICoffeeRepository _coffeeRepository;

    public CoffeeController(ILogger<CoffeeController> logger, ICoffeeRepository repository)
    {
        _logger = logger;
        _coffeeRepository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Coffee>>> GetCoffee()
    {
        return Ok(await _coffeeRepository.GetAllCoffee());
    }


    [HttpGet]
    [Route("{coffeeId}")]
    public async Task<ActionResult<Coffee>> GetCoffeeById(string coffeeId)
    {
        var coffee = await _coffeeRepository.GetCoffeeById(coffeeId);
        if (coffee == null)
        {
            return NotFound();
        }
        return Ok(coffee);
    }

    [HttpPost]
    public async Task <ActionResult<Coffee>> CreateCoffee(Coffee coffee)
    {
        if (!ModelState.IsValid || coffee == null)
        {
            return BadRequest();
        }
        var newCoffee = await _coffeeRepository.CreateCoffee(coffee);
        return Created(nameof(GetCoffeeById), newCoffee);
    }

    [HttpPut]
    [Route("{coffeeId}")]
    public async Task<ActionResult<Coffee>> UpdateCoffee(Coffee coffee)
    {
        if (!ModelState.IsValid || coffee == null)
        {
            return BadRequest();
        }
        return Ok(await _coffeeRepository.UpdateCoffee(coffee));
    }

    [HttpDelete]
    [Route("{coffeeId}")]
    public async Task<ActionResult> DeleteCoffee(string coffeeId)
    {
        await _coffeeRepository.DeleteCoffeeById(coffeeId);
        return NoContent();
    }
}