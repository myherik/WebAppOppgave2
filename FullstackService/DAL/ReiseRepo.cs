using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullstackService.Models;
using Microsoft.EntityFrameworkCore;

namespace FullstackService.DAL
{
    public class ReiseRepo: IReiseRepo
    {
        private readonly MyDBContext _db;
        
        public ReiseRepo(MyDBContext db)
        {
            _db = db;
        }


        public async Task<bool> AddOne(Reise reise)
        {
            _db.Reiser.Add(reise);
            return (await _db.SaveChangesAsync()) > 0;
        }

        public async Task<List<Reise>> GetAll()
        {

            return await _db.Reiser.ToListAsync();
        }

        public async Task<Reise> GetOneById(int id)
        {
            return await _db.Reiser.FindAsync(id);
        }

        public async Task<List<Lugar>> HentLugerByReise(int reiseId)
        {
            return await _db.Lugarer.Where(l => l.Reise.Id == reiseId).ToListAsync();
        }

        public async Task<Post> HentPoststedByPostnummer(string postnummer)
        {
            return await _db.PostSteder.FirstOrDefaultAsync(p => p.PostNummer == postnummer);
        }

        public async Task<Reise> UpdateReise(int reiseId, Reise reise)
        {
            var dbReise = await _db.Reiser.FindAsync(reiseId);
            if (dbReise is null)
            {
                return null;
            }

            dbReise.Strekning = reise.Strekning;
            dbReise.Info = reise.Info;
            dbReise.MaLugar = reise.MaLugar;
            dbReise.PrisPerGjest = reise.PrisPerGjest;
            dbReise.PrisBil = reise.PrisBil;
            dbReise.BildeLink = reise.BildeLink;

            await _db.SaveChangesAsync();

            return dbReise;
        }
    }
}