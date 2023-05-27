using Bogus;
using MusicExplorer.Client.Models;

namespace MusicExplorer.UnitTests.TestHelper
{
    public static class ArtistReleaseDataGenerator
    {
        public static MusicBrainzReleasesResponse GenerateDummyData()
        {
            var faker = new Faker();

            var releaseCount = 1;
            var releaseOffset = faker.Random.Long(1, 100);

            var releases = new List<Release>();

            for (int i = 0; i < releaseCount; i++)
            {
                var release = new Release
                {
                    TextRepresentation = new TextRepresentation { Script = faker.Lorem.Word(), Language = faker.Address.Country() },
                    PackagingId = faker.Random.Guid(),
                    ReleaseEvents = GenerateReleaseEvents(faker),
                    Country = faker.Address.Country(),
                    Date = faker.Date.Recent().ToString("yyyy-MM-dd"),
                    ArtistCredit = GenerateArtistCredits(faker),
                    StatusId = faker.Random.Guid(),
                    LabelInfo = GenerateLabelInfos(faker),
                    Disambiguation = faker.Lorem.Sentence(),
                    Id = faker.Random.Guid(),
                    Status = faker.Lorem.Word(),
                    Quality = faker.Lorem.Word(),
                    CoverArtArchive = new CoverArtArchive { Front = faker.Random.Bool(), Back = faker.Random.Bool(), Count = faker.Random.Long(1, 5), Artwork = faker.Random.Bool(), Darkened = faker.Random.Bool() },
                    Media = GenerateMedia(faker),
                    Barcode = faker.Random.Guid().ToString(),
                    Asin = faker.Lorem.Word(),
                    Title = faker.Lorem.Sentence(),
                    Packaging = faker.Lorem.Word()
                };

                releases.Add(release);
            }

            var musicBrainzReleasesResponse = new MusicBrainzReleasesResponse
            {
                Releases = releases,
                ReleaseCount = releaseCount,
                ReleaseOffset = releaseOffset
            };


            return musicBrainzReleasesResponse;
        }

        private static List<ReleaseEvent> GenerateReleaseEvents(Faker f)
        {
            return f.Make(2, () => new ReleaseEvent
            {
                Area = new Miscellaneous { Type = f.Lorem.Word(), SortName = f.Name.FullName(), Disambiguation = f.Lorem.Sentence(), Id = f.Random.Guid(), TypeId = f.Random.Guid(), Name = f.Lorem.Word(), LabelCode = f.Random.Long(1, 100), Iso31661Codes = new List<string> { f.Random.AlphaNumeric(2), f.Random.AlphaNumeric(2) } },
                Date = f.Date.Recent().ToString("yyyy-MM-dd")
            }).ToList();
        }

        private static List<ArtistCredit> GenerateArtistCredits(Faker f)
        {
            return f.Make(2, () => new ArtistCredit
            {
                Name = f.Name.FullName(),
                Artist = new Miscellaneous
                {
                    Type = f.Lorem.Word(),
                    SortName = f.Name.FullName(),
                    Disambiguation = f.Lorem.Sentence(),
                    Id = f.Random.Guid(),
                    TypeId = f.Random.Guid(),
                    Name = f.Name.FullName(),
                    LabelCode = f.Random.Long(1, 100),
                    Iso31661Codes = new List<string> { f.Random.AlphaNumeric(2), f.Random.AlphaNumeric(2) }
                },
                Joinphrase = f.Lorem.Word()
            }).ToList();
        }

        private static List<LabelInfo> GenerateLabelInfos(Faker f)
        {
            return f.Make(2, () => new LabelInfo
            {
                Label = new Miscellaneous { Type = f.Lorem.Word(), SortName = f.Name.FullName(), Disambiguation = f.Lorem.Sentence(), Id = f.Random.Guid(), TypeId = f.Random.Guid(), Name = f.Lorem.Word(), LabelCode = f.Random.Long(1, 100), Iso31661Codes = new List<string> { f.Random.AlphaNumeric(2), f.Random.AlphaNumeric(2) } },
                CatalogNumber = f.Random.AlphaNumeric(6)
            }).ToList();
        }

        private static List<Media> GenerateMedia(Faker f)
        {
            return f.Make(2, () => new Media
            {
                Title = f.Lorem.Word(),
                Format = f.Lorem.Word(),
                TrackOffset = f.Random.Long(1, 5),
                Position = f.Random.Long(1, 5),
                FormatId = f.Random.Guid(),
                Tracks = GenerateTracks(f),
                TrackCount = f.Random.Long(1, 10)
            }).ToList();
        }

        private static List<Track> GenerateTracks(Faker f)
        {
            return f.Make(2, () => new Track
            {
                Id = f.Random.Guid(),
                Position = f.Random.Long(1, 10),
                Length = f.Random.Long(1, 300),
                Title = f.Lorem.Word(),
                ArtistCredit = GenerateArtistCredits(f),
                Recording = new Recording { Title = f.Lorem.Word(), FirstReleaseDate = f.Date.Recent().ToString("yyyy-MM-dd"), Disambiguation = f.Lorem.Sentence(), Length = f.Random.Long(1, 300), Id = f.Random.Guid(), ArtistCredit = GenerateArtistCredits(f), Video = f.Random.Bool() },
                Number = f.Random.AlphaNumeric(1)
            }).ToList();
        }
    }
}