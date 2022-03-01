using Bogus;

namespace TestDataApi;

public class DbSeeder
{
    
    const int ADDRESS_COUNT = 30;
    const int PHARMACY_COUNT = 10;
    const int DOCTOR_COUNT = 10;
    const int PATIENT_COUNT = 80;
    const int PHARMACEUT_COUNT = 10;
    

    static Faker<Address> addFaker = new Faker<Address>();
    static Faker<Pharmacy> pharmacyFaker = new Faker<Pharmacy>();
    static Faker<Doctor> doctorFaker = new Faker<Doctor>();
    static Faker<Patient> patientFaker = new Faker<Patient>();
    static Faker<Pharmaceut> pharmaceutFaker = new Faker<Pharmaceut>();
    static Faker<Prescription> prescriptionFaker = new Faker<Prescription>();


    private int addCount = 0;

    private PrescriptionContext _prescriptionContext;

    public DbSeeder(PrescriptionContext prescriptionContext)
    {
        _prescriptionContext = prescriptionContext;
    }


    public void SeedTestData(int count){
        var add = CreateAddresses();
        var pharmacies = CreatePharmacies(add);
        var meds = CreateMedicines();
        var patients = CreatePatients(add);
        var pharmaceuts = CreatePharmaceuts(add);
        var doctors = CreateDoctors(add);
        var prescriptions = CreatePrescriptions(meds, doctors, patients);

        _prescriptionContext.AddRange(pharmacies);
        _prescriptionContext.AddRange(meds);
        _prescriptionContext.AddRange(patients);
        _prescriptionContext.AddRange(pharmaceuts);
        _prescriptionContext.AddRange(doctors);
        _prescriptionContext.SaveChanges();
        _prescriptionContext.AddRange(prescriptions);
        _prescriptionContext.SaveChanges();
    }

    private List<Address> CreateAddresses(){
        

        addFaker
        .RuleFor(a => a.Streetname, (f, a) => f.Address.StreetName())
        .RuleFor(a => a.Streetnumber, (f, a) => f.Address.BuildingNumber())
        .RuleFor(a => a.Zipcode, (f, a) => f.Address.ZipCode("####"));

        return addFaker.Generate(ADDRESS_COUNT);

        _prescriptionContext.AddRange();
    }

    private List<Pharmacy> CreatePharmacies(List<Address> add)
    {
        var addCount = 0;
        pharmacyFaker
        .RuleFor(p => p.Address, (f, p) => add[addCount++])
        .RuleFor(p => p.PharmacyName, (f, p) => f.Company.CompanyName());

        return pharmacyFaker.Generate(PHARMACY_COUNT);
    }

    private List<Medicine> CreateMedicines()
    {
        var med = new List<Medicine>();

        med.Add(new Medicine() { Name = "Constaticimol" });
        med.Add(new Medicine() { Name = "Abracadabrasol" });
        med.Add(new Medicine() { Name = "Fecetiol" });
        med.Add(new Medicine() { Name = "Gormanol" });
        med.Add(new Medicine() { Name = "Crapinal" });
        med.Add(new Medicine() { Name = "Hamigel" });

        return med;
    }

    private List<Pharmaceut> CreatePharmaceuts(List<Address> add)
    {
        int count = 0;
        pharmaceutFaker
        .RuleFor(p => p.PersonalData, (f, p) => CreatePersonalData("pharmaceut", $"pharmaceut{count++}"));

        return pharmaceutFaker.Generate(PHARMACEUT_COUNT);
    }
    private List<Patient> CreatePatients(List<Address> add)
    {
        patientFaker
            .RuleFor(p => p.Cpr, (f, p) => (f.Person.DateOfBirth.ToString("ddMMyy") + "1" + f.Random.Number(9).ToString() + f.Random.Number(9).ToString() + f.Random.Number(9).ToString()))
            .RuleFor(p => p.PersonalData, (f, p) => CreatePersonalData("patient", $"{p.Cpr}"));

        return patientFaker.Generate(PATIENT_COUNT);
    }

    private List<Doctor> CreateDoctors(List<Address> add)
    {
        int count = 0;
        doctorFaker
        .RuleFor(d => d.PersonalData, (f, d) => CreatePersonalData("doctor", $"doctor{count++}"));
        
        return doctorFaker.Generate(DOCTOR_COUNT);
    }

    private List<Prescription> CreatePrescriptions(List<Medicine> meds, List<Doctor> docs, List<Patient> patients)
    {
        var random = new Random();

        prescriptionFaker
        .RuleFor(p => p.Creation, (f, p) => f.Date.Between(DateTime.Now.AddDays(-30), DateTime.Now))
        .RuleFor(p => p.Expiration, (f, p) => DateOnly.FromDateTime(p.Creation).AddDays(30))
        .RuleFor(p => p.PrescribedByNavigation, (f, p) => docs[random.Next(docs.Count)])
        .RuleFor(p => p.PrescribedToNavigation, (f, p) => patients[random.Next(patients.Count)])

        .RuleFor(p => p.Medicine, (f, p) => meds[random.Next(meds.Count)]);

        return prescriptionFaker.Generate(DOCTOR_COUNT);
    }

    private PersonalDatum CreatePersonalData(string emailPostfix, string username)
    {
        var personalDataFaker = new Faker<PersonalDatum>()
        .RuleFor(p => p.FirstName, (f, p) => f.Name.FirstName())
        .RuleFor(p => p.LastName, (f, p) => f.Name.LastName())
        .RuleFor(p => p.Email, (f, p) => $"{p.FirstName}@{p.LastName}.emailPostfix")
        .FinishWith((f, p) => p.Login = CreateLoginInfo(username, p.FirstName));
        return personalDataFaker.Generate();
    }

    private LoginInfo CreateLoginInfo(string username, string password)
    {
        var salt = Salt.Create();
        var hash = Hash.Create(password, salt);

        LoginInfo loginInfo = new LoginInfo();
        loginInfo.Password = hash;
        loginInfo.Salt = salt;
        loginInfo.Username = username;

        return loginInfo;
    }
    
}
