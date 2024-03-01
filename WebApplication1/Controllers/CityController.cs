using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CityController : ControllerBase
{
    private readonly IDbContextFactory<AppContext> _dbFactory;

    public CityController(IDbContextFactory<AppContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var db = await _dbFactory.CreateDbContextAsync();
        var cities = await db.Cities.ToListAsync();
        return Ok(cities);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var db = await _dbFactory.CreateDbContextAsync();
        var city = await db.Cities.FindAsync(id);
        if (city is null)
        {
            return NotFound(NotFound);
        }

        return Ok(city);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var db = await _dbFactory.CreateDbContextAsync();
        var city = await db.Cities.FindAsync(id);
        if (city is null)
        {
            return NotFound(NotFound);
        }

        _ = db.Cities.Remove(city);
        await db.SaveChangesAsync();

        return Ok(city);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] City entity)
    {
        var db = await _dbFactory.CreateDbContextAsync();
        var exists = db.Cities.Contains(entity);
        if (exists)
        {
            return BadRequest(AlreadyPresent);
        }

        await db.Cities.AddAsync(entity);
        await db.SaveChangesAsync();
        return Ok(entity);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put([FromBody] City entity, int id)
    {
        var db = await _dbFactory.CreateDbContextAsync();
        if (entity.CityId != id)
        {
            entity.CityId = id;
        }

        db.Cities.Update(entity);
        await db.SaveChangesAsync();
        return Ok();
    }

    private const string AlreadyPresent = "Запись уже существует";
    private const string NotFound = "Запись с этим id не найдена";
}