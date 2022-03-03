using Bogus;

namespace TestDataApi;

public class DbSeeder
{
    
    const int ADDRESS_COUNT = 3000000;
    const int PHARMACY_COUNT = 1000;
    const int DOCTOR_COUNT = 100000;
    const int PATIENT_COUNT = 8000000;
    const int PHARMACEUT_COUNT = 100000;
    

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
        Console.WriteLine("Save Changes");
        _prescriptionContext.SaveChanges();
        _prescriptionContext.AddRange(prescriptions);
        Console.WriteLine("Save Changes");
        _prescriptionContext.SaveChanges();
    }

    private List<Address> CreateAddresses(){
        Console.WriteLine("Create Addresses");
        

        addFaker
        .RuleFor(a => a.Streetname, (f, a) => f.Address.StreetName())
        .RuleFor(a => a.Streetnumber, (f, a) => f.Address.BuildingNumber())
        .RuleFor(a => a.Zipcode, (f, a) => f.Address.ZipCode("####"));

        return addFaker.Generate(ADDRESS_COUNT);
    }

    private List<Pharmacy> CreatePharmacies(List<Address> add)
    {
        Console.WriteLine("Create Pharmacies");
        pharmacyFaker
        .RuleFor(p => p.Address, (f, p) => add[addCount++])
        .RuleFor(p => p.PharmacyName, (f, p) => f.Company.CompanyName());

        return pharmacyFaker.Generate(PHARMACY_COUNT);
    }

    private List<Medicine> CreateMedicines()
    {
        Console.WriteLine("Create Medicines");
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
        Console.WriteLine("Create Pharmaceuts");
        int count = 0;
        pharmaceutFaker
        .RuleFor(p => p.PersonalData, (f, p) => CreatePersonalData("pharmaceut", $"pharmaceut{count++}"));

        return pharmaceutFaker.Generate(PHARMACEUT_COUNT);
    }
    private IEnumerable<Patient> CreatePatients(List<Address> add)
    {
        Console.WriteLine("Create Patients");
        var perIteration = PATIENT_COUNT / 1000;
        for (int i = 0; i < 1000; i++)
        {
            Console.WriteLine($"Created {i*perIteration} patients");

            patientFaker
            .RuleFor(p => p.Cpr, (f, p) => (f.Person.DateOfBirth.ToString("ddMMyy") + f.Random.Number(9).ToString() + f.Random.Number(9).ToString() + f.Random.Number(9).ToString() + f.Random.Number(9).ToString()))
            .RuleFor(p => p.PersonalData, (f, p) => CreatePersonalData("patient", $"{p.Cpr}"));

            yield return patientFaker.Generate(perIteration);
        }
        
    }

    private List<Doctor> CreateDoctors(List<Address> add)
    {
        Console.WriteLine("Create Doctors");
        int count = 0;
        doctorFaker
        .RuleFor(d => d.PersonalData, (f, d) => CreatePersonalData("doctor", $"doctor{count++}"));
        
        return doctorFaker.Generate(DOCTOR_COUNT);
    }

    private List<Prescription> CreatePrescriptions(List<Medicine> meds, List<Doctor> docs, List<Patient> patients)
    {
        Console.WriteLine("Create Prescriptions");
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
