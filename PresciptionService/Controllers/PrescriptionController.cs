using Microsoft.AspNetCore.Mvc;

namespace PresciptionService.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionController : ControllerBase
{

    private readonly ILogger<PrescriptionController> _logger;
    private readonly PrescriptionContext _prescriptionContext;

    public PrescriptionController(ILogger<PrescriptionController> logger, PrescriptionContext prescriptionContext)
    {
        _logger = logger;
        _prescriptionContext = prescriptionContext;
    }

    [HttpGet(Name = "GetPrescriptions")]
    public IEnumerable<Prescription> Get()
    {
        return _prescriptionContext.Prescriptions.ToList();
    }
}
