using Microsoft.AspNetCore.Mvc;
using PrescriptionService.DAL;
using PrescriptionService.DTO;
using PrescriptionService.Util;

namespace PrescriptionService.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionController : ControllerBase
{

    private readonly ILogger<PrescriptionController> _logger;
    private readonly IPrescriptionRepo _prescriptionRepo;

    public PrescriptionController(ILogger<PrescriptionController> logger, IPrescriptionRepo prescriptionRepo)
    {
        _logger = logger;
        _prescriptionRepo = prescriptionRepo;
    }

    [HttpGet(Name = "GetPrescriptions")]
    public IEnumerable<PrescriptionDto> Get()
    {
        var result = _prescriptionRepo.GetPrescriptionsExpiringLatest(DateOnly.FromDateTime(DateTime.Now.AddDays(7))).Select(x => PrescriptionMapper.ToDto(x));
        return result;
    }
}
