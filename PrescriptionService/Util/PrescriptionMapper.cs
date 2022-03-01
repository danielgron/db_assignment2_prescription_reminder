using PrescriptionService.DTO;

namespace PrescriptionService.Util
{
    public class PrescriptionMapper
    {
        public static PrescriptionDto ToDto(Prescription prescription)
        {
            var dto = new PrescriptionDto();

            dto.Creation = prescription.Creation;
            dto.Expiration = prescription.Expiration;
            dto.Patient = new PatientDto();
            dto.Patient.FirstName = prescription.PrescribedToNavigation.PersonalDatum.FirstName;
            dto.Patient.LastName = prescription.PrescribedToNavigation.PersonalDatum.LastName;
            dto.Patient.Email = prescription.PrescribedToNavigation.PersonalDatum.Email;
            dto.Medicine = new MedicineDto();
            dto.Medicine.Name = prescription.Medicine.Name;

            return dto;
        }

        

    }
}
