using Dapper;
using Npgsql;

namespace PrescriptionService.DAL
{
    public class DapperPrescriptionRepo : IPrescriptionRepo
    {
        private string _connectionsString;

        public DapperPrescriptionRepo(string connectionString)
        {
            _connectionsString = connectionString;
        }
        public IEnumerable<Prescription> GetPrescriptionsExpiringLatest(DateOnly expiringDate)
        {
            //TODO get connectionstring from settings
            using (var connection = new NpgsqlConnection(_connectionsString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

                var lookup = new Dictionary<long, Prescription>();

                var query =
                    @$"
                        SELECT
                        pr.id, pr.expiration, pr.creation, pr.medicine_id, pr.prescribed_to,
                        pat.id, pat.personal_data_id,
                        med.id, med.name,
                        dat.id, dat.email, dat.first_name, dat.last_name
                        
                        FROM prescription pr
                            INNER JOIN patient pat ON pr.prescribed_to = pat.id
                            INNER JOIN personal_data dat ON pat.personal_data_id = dat.id
                            INNER JOIN medicine med ON pr.medicine_id = med.id
                        WHERE pr.expiration < @exp::date
                        
                    ";

                var param = new DynamicParameters();
                param.Add("@exp", expiringDate.ToString("yyyy-MM-dd"));

                
                connection.Query<Prescription, Patient, Medicine, PersonalDatum, Prescription>(query, (pr, pat, med, dat) => {
                    Prescription prescription;
                    if (!lookup.TryGetValue(pr.Id, out prescription))
                        lookup.Add(pr.Id, prescription = pr);

                    prescription.PrescribedToNavigation = pat;
                    prescription.Medicine = med;
                    pat.PersonalDatum = dat;
                    return prescription;
                }, splitOn:"id, id, id, id", param: param).AsQueryable();
                var resultList = lookup.Values;



                return resultList;
            }
        }
    }
}
