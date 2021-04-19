using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Context;
using TMAS.DB.Models;

namespace TMAS.DAL.Repositories
{
    public class FileRepository
    {
        private readonly AppDbContext db;
        public FileRepository(AppDbContext context)
        {
            db = context;
        }
        public async Task<File> Create(File file)
        {
            db.Files.Add(file);
            await db.SaveChangesAsync();
            return file;
        }
        public async Task<IEnumerable<File>> GetFiles(int id)
        {
            return await db.Files.Where(x => x.CardId == id).ToListAsync();
        }
    }
}
