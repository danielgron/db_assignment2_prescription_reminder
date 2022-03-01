namespace RenewalService.Dto
{
    public class PrescriptionDto
    {
        public int Id { get; set; }
        public DateTime? Expiration { get; set; }
        public DateTime Creation { get; set; }
        public MedicineDto Medicine { get; set; }
        public PatientDto Patient { get; set; }
    }
}
