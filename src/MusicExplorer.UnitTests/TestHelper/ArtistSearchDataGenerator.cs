using Bogus;
using MusicExplorer.Common.Models.DbContext;

namespace MusicExplorer.UnitTests.TestHelper
{
    public static class ArtistSearchDataGenerator
    {
        public static List<Artist> GenerateDummyData(int count = 10)
        {
            var faker = new Faker();

            var artists = new List<Artist>();

            for (int i = 0; i < count; i++)
            {
                var artist = new Artist
                {
                    Id = faker.Random.Long(),
                    Name = faker.Name.FullName(),
                    UniqueIdentifier = Guid.NewGuid(),
                    Country = faker.Address.Country(),
                    Aliases = faker.Random.Bool(0.3f) ? faker.Lorem.Word() + "," + faker.Lorem.Word() : null
                };

                artists.Add(artist);
            }

            return artists;
        }
    }
}
