using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.BLL.Interfaces;
using TMAS.DAL.DTO;
using TMAS.DAL.Interfaces;
using TMAS.DAL.Repositories;
using TMAS.DB.Models;

namespace TMAS.BLL.Services
{
    public class FileService:IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public FileService(IFileRepository fileRepository,IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
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
            var createResult= await _fileRepository.Create(newFile);
            return createResult;
        }

        public async Task<IEnumerable<FileViewDTO>> GetFiles(int cardId)
        {
            var result= await _fileRepository.GetFiles(cardId);
            var mapperResult =_mapper.Map<IEnumerable<File>,IEnumerable<FileViewDTO>>(result);
            return mapperResult;
        }
    }
}
