using SouthAfricanId;
using SouthAfricanId.Generation;
using SouthAfricanId.Models;

namespace ConsoleApp2
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            SouthAfricanIdDecoder decoder = new SouthAfricanIdDecoder();
            SouthAfricanId.Generation.IdNumberGenerator generator = new SouthAfricanId.Generation.IdNumberGenerator();
            var idNumber = generator.GenerateIdNumber();
            Console.WriteLine(idNumber);
            Console.WriteLine("Hello, World!");


            var decoded = decoder.Decode(idNumber);

            if (decoded != null)
            {
                Console.WriteLine($"Valid ID: {decoded.IdNumber}");
                Console.WriteLine($"Date of Birth: {decoded.AgeInfo?.DateOfBirth:yyyy-MM-dd}");
                Console.WriteLine($"Age: {decoded.AgeInfo?.AgeString}");
                Console.WriteLine($"Gender: {decoded.Gender}");
                Console.WriteLine($"Older than 18: {decoded.AgeInfo.Years >= 18}");
            }
            else
            {
                Console.WriteLine("Invalid ID number.");
            }
        }
    }
}


