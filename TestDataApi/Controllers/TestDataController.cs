using Microsoft.AspNetCore.Mvc;

namespace TestDataApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestDataController : ControllerBase
{
    
    private readonly ILogger<TestDataController> _logger;
    private readonly PrescriptionContext _pc;

    public TestDataController(ILogger<TestDataController> logger, PrescriptionContext pc)
    {
        _logger = logger;
        _pc = pc;
    }
    

    [HttpGet(Name = "SeedTestData")]
    public Task Get()
    {
        
        new DbSeeder(_pc).SeedTestData(1000000);
        return null;
    }
    
}
