using Html2Markdown;
using Newtonsoft.Json;
using QmToMdFileConverter.Classes;
using QmToMdFileConverter.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QmToMdFileConverter.Services
{
    public class FileService : IFileService
    {
        public async Task<List<ConvertedFileInfo>> GetDataToConvertFromUrl(string url)
        {
            HttpClient _client = new HttpClient();
            var data = await _client.GetStringAsync(url);
            var notes = JsonConvert.DeserializeObject<List<ConvertedFileInfo>>(data);
            return notes;
        }


        public int ConvertDataToMdFile(string folderName, List<ConvertedFileInfo> data)
        {
            int createdFiles = 0;
            foreach (var item in data)
            {
                string fileName = $"{item.Title}-{item.CreatedAt.ToString("dd-mm-yyyy")}";
                var converter = new Converter();
                var markdown = converter.Convert(item.Content);
                createdFiles += CreateMdFile(folderName, fileName, markdown);
            }
            return createdFiles;
        }

        private int CreateMdFile(string folderName, string fileName, string content)
        {
            int counter = 0;
            string path = @$"{Path.GetDirectoryName(Directory.GetCurrentDirectory())}\{folderName}";
            //string file = @$"{StartupPath}\Notes\{fileName}.md";
            try
            {
                DirectoryInfo di;
                // Determine whether the directory exists
                if (!Directory.Exists(path))
                {
                    di = Directory.CreateDirectory(path);
                }
                else
                {
                    di = new DirectoryInfo(path);
                }

                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create($"{di.FullName}/{fileName}.md"))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(content);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                counter = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return counter;
        }

    }
}
