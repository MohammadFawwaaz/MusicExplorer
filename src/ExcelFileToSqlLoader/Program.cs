using ExcelFileToSqlLoader.Infrastructure;
using ExcelFileToSqlLoader.Utils;
using OfficeOpenXml;

namespace ExcelFileToSqlLoader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Excel loader started..");

            try
            {
                // Set EPPlus license context to NonCommercial
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                string excelFilePath = "docs/artists.xlsx";

                if (!File.Exists(excelFilePath))
                {
                    Console.WriteLine("Excel file not found.");
                    Console.ReadKey();
                    return;
                }

                using (var context = new DatabaseContext())
                {
                    var reader = new ExcelFileReader(new FileInfo(excelFilePath));
                    var data = reader.ReadAll();

                    context.Artist.AddRange(data);
                    context.SaveChanges();
                }

                Console.WriteLine("Data loaded successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occured while loading file: {e.Message}");
                throw;
            }
        }
    }
}
