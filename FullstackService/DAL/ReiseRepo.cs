using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullstackService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FullstackService.DAL
{
    public class ReiseRepo: IReiseRepo
    {
        private readonly MyDBContext _db;
        private readonly ILogRepo _log;

        public ReiseRepo(MyDBContext db, ILogRepo log)
        {
            _db = db;
            _log = log;
        }


        public async Task<bool> AddOneReiseAsync(Reise reise)
        {
            if (_db.Bilder.Any((bilde => bilde.Id == reise.BildeLink.Id)))
            {
                reise.BildeLink = await _db.Bilder.FindAsync(reise.BildeLink.Id);
            }

            await _log.LogAsync($"Ny reise laget, rute: {reise.Strekning}");
            
            _db.Reiser.Add(reise);
            return (await _db.SaveChangesAsync()) > 0;
        }

        public async Task<List<Reise>> GetAllReiseAsync()
        {

            return await _db.Reiser.ToListAsync();
        }

        public async Task<Reise> GetReiseByIdAsync(int id)
        {
            //await _log.LogAsync($"Henter reise med id: {id}");
            
            return await _db.Reiser.FindAsync(id);
        }

        public async Task<Post> HentPoststedByPostnummer(string postnummer)
        {
            return await _db.PostSteder.FirstOrDefaultAsync(p => p.PostNummer == postnummer);
        }

        public async Task<Reise> UpdateReiseAsync(int reiseId, Reise reise)
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
            dbReise.BildeLink = await _db.Bilder.FindAsync(reise.BildeLink.Id);

            await _db.SaveChangesAsync();

            await _log.LogAsync($"Endrer reise med id: {dbReise.Id}");

            return dbReise;
        }

        public async Task<Reise> DeleteReiseAsync(int reiseId)
        {
            var dbReise = await _db.Reiser.FindAsync(reiseId);
            if (dbReise is null) return null;
            _db.Reiser.Remove(dbReise);
            await _db.SaveChangesAsync();

            await _log.LogAsync($"Slettet reise, streknomg: {dbReise.Strekning}");
                
            return dbReise;
        }

        public async Task<List<Bilde>> GetAlleBilder()
        {
            return await _db.Bilder.ToListAsync();
        }

        public async Task InsertBilde(Bilde bilde)
        {
            _db.Bilder.Add(bilde);
            await _db.SaveChangesAsync();
        }
    }
}