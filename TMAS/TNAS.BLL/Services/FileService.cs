using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DAL.Repositories;
using TMAS.DB.Models;

namespace TMAS.BLL.Services
{
    public class FileService
    {
        private readonly FileRepository _fileRepository;

        public FileService(FileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }
        public async Task<File> Create(int cardId,string path,string type,string name)
        {
            var newFile = new File
            {
                Name=name,
                Path=path,
                CardId=cardId,
                FileType=type
            };
            return await _fileRepository.Create(newFile);
        }

        public async Task<IEnumerable<File>> GetFiles(int id)
        {
            return await _fileRepository.GetFiles(id);
        }
    }
}
