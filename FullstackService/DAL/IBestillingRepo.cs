using System.Collections.Generic;
using System.Threading.Tasks;
using FullstackService.Models;

namespace FullstackService.DAL
{
    public interface IBestillingRepo
    {

        Task<List<Bestilling>> HentAlle();

        Task<Bestilling> HentEn(int id);

        Task<Bestilling> HentEnByRef(string referanse);

        Task<Bestilling> LeggTil(Bestilling bestilling);

        Task Endre(int id, Kunde kunde);
        
        Task<int> Slett(int id);

        Task<int> Lagre();

    }
}