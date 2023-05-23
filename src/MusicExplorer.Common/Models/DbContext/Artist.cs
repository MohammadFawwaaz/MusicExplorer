using System.ComponentModel.DataAnnotations;

namespace MusicExplorer.Common.Models.DbContext
{
    public class Artist
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public Guid UniqueIdentifier { get; set; }
        public string Country { get; set; }
        public string? Aliases { get; set; }
    }
}
