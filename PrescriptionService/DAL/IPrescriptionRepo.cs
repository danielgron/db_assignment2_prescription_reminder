namespace PresciptionService.DAL
{
    public interface IPrescriptionRepo
    {
        public IEnumerable<Prescription> GetPrescriptionsExpiringLatest(DateOnly expiringDate);
    }
}
