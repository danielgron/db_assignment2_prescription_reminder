using RenewalService.Dto;
using RestSharp;

namespace RenewalService
{
    public class RestSharpRenewalService : IRenewalService
    {
        private string _prescriptionServiceEndpoint;
        private string _notificationServiceEndpoint;

        public RestSharpRenewalService(string prescriptionServiceEndpoint, string notificationServiceEndpoint)
        {
            _prescriptionServiceEndpoint = prescriptionServiceEndpoint;
            _notificationServiceEndpoint = notificationServiceEndpoint;
        }

        public void NotifyRenewals()
        {
            CancellationToken cancellationToken = new CancellationToken();
            var prescriptionClient = new RestClient(_prescriptionServiceEndpoint);
            var prescriptionRequest = new RestRequest("Prescription");
            var prescriptionResponse = prescriptionClient.GetAsync< List<PrescriptionDto>> (prescriptionRequest, cancellationToken).Result;

            var notificationClient = new RestClient(_notificationServiceEndpoint);

            foreach (var prescription in prescriptionResponse)
            {
                var email = prescription.Patient.Email;
                MailRequestDto mailRequest = new MailRequestDto();
                mailRequest.ToEmail = email;
                mailRequest.Subject = "Prescription Expiring";
                mailRequest.Body = $"Your prescription for {prescription.Medicine.Name} expires {prescription.Expiration}";
                var notificationRequest = new RestRequest("api/Email").AddBody(mailRequest);

                var response = notificationClient.PostAsync(notificationRequest, cancellationToken).Result;

            }
        }
    }
}
