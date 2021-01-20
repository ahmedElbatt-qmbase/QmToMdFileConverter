using QmToMdFileConverter.Interfaces;
using QmToMdFileConverter.Services;
using System;
using System.Threading.Tasks;

namespace QmToMdFileConverter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int num;
            int value = 0;
            string choice;
            do
            {
                Console.WriteLine("Select the number of your choice:");
                Console.WriteLine("1: Realease notes");
                Console.WriteLine("2: Faqs");
                Console.WriteLine("0: Exit");
                Console.Write("Enter the number of your choice: ");

                choice = Console.ReadLine();
                if (int.TryParse(choice, out num))
                {
                    value = int.Parse(choice);
                } else
                { 
                    break;
                }

                switch (value)
                {
                    case 1:
                        await ConvertReleaseNotesToMd();
                        break;
                    case 2:
                        Console.WriteLine("Faqs added later");
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Wrong choice");
                        break;
                }
            } while (value != 0);

            Console.ReadKey();
        }

        private async static Task ConvertReleaseNotesToMd()
        {
            FileService _fileService = new FileService();
            var data = await _fileService.GetDataToConvertFromUrl("https://qmadmin.qmbase.com/api/releaseNotes/");
            Console.WriteLine($"{data.Count} rows feteched from the API");
            int result = _fileService.ConvertDataToMdFile("ReleaseNotes", data);
            Console.WriteLine($"{result} .md files created successfully for ReleaseNotes");
            Console.WriteLine("=============");
        }
    }
}
