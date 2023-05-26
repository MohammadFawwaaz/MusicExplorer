using ExcelFileToSqlLoader.Models;
using OfficeOpenXml;

namespace ExcelFileToSqlLoader.Utils
{
    public class ExcelFileReader
    {
        private readonly FileInfo file;

        public ExcelFileReader(FileInfo file)
        {
            this.file = file;
        }

        public IEnumerable<Artist> ReadAll()
        {
            using var package = new ExcelPackage(file);
            var worksheet = package.Workbook.Worksheets[0];

            int rowCount = worksheet.Dimension.Rows;

            // read and sanitize data before inserting to DB
            for (int row = 2; row <= rowCount; row++) // first row is header
            {
                var entity = new Artist();

                // Name
                var nameCell = worksheet.Cells[row, 1].Value;
                if (nameCell != null)
                {
                    entity.Name = nameCell.ToString().Trim();
                }

                // UniqueIdentifier
                var uniqueIdentifierCell = worksheet.Cells[row, 2].Value;
                if (uniqueIdentifierCell != null && Guid.TryParse(uniqueIdentifierCell.ToString().Trim(), out var uniqueIdentifier))
                {
                    entity.UniqueIdentifier = uniqueIdentifier;
                }

                // Country
                var countryCell = worksheet.Cells[row, 3].Value;
                if (countryCell != null)
                {
                    entity.Country = countryCell.ToString().Trim();
                }

                // Aliases
                var aliasesCell = worksheet.Cells[row, 4].Value;
                if (aliasesCell != null)
                {
                    var aliases = aliasesCell.ToString().Split(',')
                                      .Select(x => x.Trim())
                                      .ToList();

                    entity.Aliases = string.Join(",", aliases);
                }

                yield return entity;
            }
        }
    }
}
