
# SouthAfricanIdValidator

This package provides robust validation, extraction, decoding, and generation for South African ID numbers. It includes:

- Checksum validation
- Date of birth and gender extraction
- Age calculation (years, months, days)
- ID number generation (random or parameterized)
- Decoding of ID numbers into structured models

## Features

- Validate South African ID numbers for correct format, date, and checksum
- Extract date of birth, age, and gender from the ID number
- Generate valid South African ID numbers (random or with parameters)
- Decode ID numbers into structured models

## Usage Example

```csharp
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


```

## How It Works

- The `Validator` class validates the ID number using the Luhn algorithm and checks the embedded date of birth.
- The `AgeExtraction` and `GenderExtraction` classes extract age and gender information from the ID number.
- The `IdNumberGenerator` class generates valid ID numbers, either randomly or with parameters.
- The `SouthAfricanIdDecoder` class decodes the ID number into a structured model (`DecodedIdModel`).
- The `AgeInformation` class provides age in years, months, days, and the date of birth.

## API

### Validator

- `Validate(string idNumber)` – Validates the ID number format, date, and checksum

### AgeExtraction

- `Extract(string idNumber)` – Returns an `AgeInformation` object if extraction is successful

### GenderExtraction

- `Extract(string idNumber)` – Returns a `Gender` enum value

### IdNumberGenerator

- `GenerateIdNumber(DateTime birthday, Gender gender, bool isCitizen = true)` – Generates a valid ID number with parameters
- `GenerateIdNumber()` – Generates a random valid ID number

### SouthAfricanIdDecoder

- `Decode(string idNumber)` – Returns a `DecodedIdModel` with all extracted info

### AgeInformation

- `Years`, `Months`, `Days` – Age breakdown
- `DateOfBirth` – Extracted date of birth
- `AgeString` – Formatted age string

### DecodedIdModel

- `IdNumber` – The decoded ID number
- `AgeInfo` – Extracted age information
- `Gender` – Extracted gender information

## License

This project is licensed under the Apache License 2.0. You must give appropriate credit to the original author in any derivative works or distributions.

See the LICENSE file for full details.
