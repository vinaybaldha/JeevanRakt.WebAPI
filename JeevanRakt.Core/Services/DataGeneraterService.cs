using Bogus;
using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Core.Services
{
    public class DataGeneraterService
    {
        Faker<Donor> DonorFake;
        Faker<Recipient> RecipientFake;

        public DataGeneraterService()
        {
            Randomizer.Seed = new Random(123);

            DonorFake = new Faker<Donor>()
                .RuleFor(u=>u.DonorId, f=>f.Random.Guid())
                .RuleFor(u=>u.DonorName, f=>f.Name.FirstName())
                .RuleFor(u=>u.DonorAge, f=>f.Random.Int(18,50))
                .RuleFor(u=>u.DonorGender, f=>f.PickRandom<Gender>().ToString())
                .RuleFor(u=>u.DonorAddress, f=>f.Address.StreetAddress())
                .RuleFor(u=>u.DonorBloodType, f=>f.PickRandom<BloodType>().ToString())
                .RuleFor(u => u.DonorContactNumber, f => f.Random.Long(1000000000,9999999999).ToString());


            Randomizer.Seed = new Random(456);

            RecipientFake = new Faker<Recipient>()
                .RuleFor(u => u.RecipientId, f => f.Random.Guid())
                .RuleFor(u => u.RecipientName, f => f.Name.FirstName())
                .RuleFor(u => u.RecipientAge, f => f.Random.Int(18, 50))
                .RuleFor(u => u.RecipientGender, f => f.PickRandom<Gender>().ToString())
                .RuleFor(u => u.RecipientAddress, f => f.Address.StreetAddress())
                .RuleFor(u => u.RecipientBloodType, f => f.PickRandom<BloodType>().ToString())
                .RuleFor(u => u.RecipientContactNumber, f => f.Random.Long(1000000000, 9999999999).ToString());
        }

        public Donor GenerateDonor()
        {
            return DonorFake.Generate();
        }

        public Recipient GenerateRecipient()
        {
            return RecipientFake.Generate();
        }

    }
}
