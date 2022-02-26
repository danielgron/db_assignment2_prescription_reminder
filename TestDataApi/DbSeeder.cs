using Bogus;

namespace TestDataApi;

public class DbSeeder
{
    const int ADDRESS_COUNT = 3000000;
    static Faker<Address> addFaker = new Faker<Address>();

    private PrescriptionContext _prescriptionContext;

    public DbSeeder(PrescriptionContext prescriptionContext)
    {
        _prescriptionContext = prescriptionContext;
    }


    public static void SeedTestData(int count){
        CreateAddresses();
    }

    private static void CreateAddresses(){
        addFaker
        .RuleFor( a => a.Streetname, (f, a) => f.Address.StreetName())
        .RuleFor( a => a.Streetnumber, (f, a) => f.Address.StreetAddress());
        
        var addresses = addFaker.Generate(ADDRESS_COUNT);
    }
}
