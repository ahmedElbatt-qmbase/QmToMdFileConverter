using QmToMdFileConverter.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QmToMdFileConverter.Interfaces
{
    public interface IFileService
    {
        Task<List<ConvertedFileInfo>> GetDataToConvertFromUrl(string url);

        int ConvertDataToMdFile(string folderName, List<ConvertedFileInfo> data);
    }
}
