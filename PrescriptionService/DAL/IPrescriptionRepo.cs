namespace PrescriptionService.DAL
{
    public interface IPrescriptionRepo
    {
        public IEnumerable<Prescription> GetPrescriptionsExpiringLatest(DateOnly expiringDate);
        public IEnumerable<Prescription> GetPrescriptionsForUser(string username, string password);
        public Prescription MarkPrescriptionWarningSent(long prescriptionId);
    }
}
