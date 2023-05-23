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
            for (int row = 2; row <= rowCount; row++) // first row is header
            {
                var entity = new Artist
                {
                    Name = worksheet.Cells[row, 1].Value.ToString(),
                    UniqueIdentifier = Guid.Parse(worksheet.Cells[row, 2].Value.ToString()),
                    Country = worksheet.Cells[row, 3].Value.ToString(),
                    Aliases = worksheet.Cells[row, 4].Value?.ToString()
                };

                yield return entity;
            }
        }
    }
}
