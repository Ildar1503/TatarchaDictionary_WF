using Dictionary.Dal.Interfaces;
using DictionaryTatarcha.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Dal.Implementation
{
    public class AdjectiveDal : IDictionaryDal<Adjective>
    {
        private readonly ApplicationDbContext _context;

        public AdjectiveDal(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Adjective adjective)
        {
            try
            {
                await _context.AddAsync(adjective);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var adjective = await _context.Adjectivies.FirstOrDefaultAsync(ad => ad.Id == id);
                _context.Adjectivies.Remove(adjective);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Adjective>> GetAll()
        {
            var adjectives = await _context.Adjectivies.ToListAsync();
            return adjectives;
        }

        public async Task<bool> Update(Adjective adjective, int id)
        {
            try
            {
                var currentAdjective = await _context.Adjectivies.FirstOrDefaultAsync(ad => ad.Id == id);
                currentAdjective.AdjectiveWord = adjective.AdjectiveWord;
                currentAdjective.AdjectiveTranslate = adjective.AdjectiveTranslate;
                _context.Adjectivies.Update(currentAdjective);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
