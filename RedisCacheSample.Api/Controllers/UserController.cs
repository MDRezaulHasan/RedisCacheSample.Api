using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedisCacheSample.Api.DB;
using RedisCacheSample.Api.Models;
using RedisCacheSample.Api.Services;

namespace RedisCacheSample.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly ICacheService _cacheService;
    private readonly AppDbContext _appDbContext;

    public UserController(ILogger<UserController> logger, ICacheService cacheService, AppDbContext appDbContext)
    {
        _logger = logger;
        _cacheService = cacheService;
        _appDbContext = appDbContext;
    }


    [HttpGet("get_user")]
    public async Task<IActionResult> GetUser()
    {
        var cachedUser = _cacheService.GetData<IEnumerable<User>>("user");
        if (cachedUser != null && cachedUser.Count() > 0)
            return Ok(cachedUser);
        cachedUser = await _appDbContext.Users.ToListAsync();
        
       //Set expiry time
       var expiryTime = DateTimeOffset.Now.AddSeconds(20);
       
       //Saving data in redis cache
       _cacheService.SetData<IEnumerable<User>>("user", cachedUser, expiryTime);
       return Ok(cachedUser);
    }

    [HttpPost("add_user")]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        var addedUser = await _appDbContext.Users.AddAsync(user);
        var expiryTime = DateTimeOffset.Now.AddSeconds(20);
        _cacheService.SetData<User>($"user{user.Id}", addedUser.Entity, expiryTime);
        
        await _appDbContext.SaveChangesAsync();
        return Ok(addedUser.Entity);
    }

    [HttpDelete("delete_user")]
    public async Task<IActionResult> DeleteUser(int Id)
    {
        var deletedUser = await _appDbContext.Users.FindAsync(Id);
        if (deletedUser == null)
            return NotFound();
        
        _appDbContext.Users.Remove(deletedUser);
        _appDbContext.SaveChanges();
        _cacheService.RemoveData($"user{Id}");

        return Ok(Id);
    }
}