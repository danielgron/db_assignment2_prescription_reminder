using Microsoft.AspNetCore.Mvc;

namespace TestDataApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestDataController : ControllerBase
{

    private readonly ILogger<TestDataController> _logger;

    public TestDataController(ILogger<TestDataController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "SeedTestData")]
    public Task Get()
    {
        
        DbSeeder.SeedTestData(1000000);
        return null;
    }
}
