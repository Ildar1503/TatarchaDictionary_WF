using Dictionary.Dal.Interfaces;
using DictionaryTatarcha.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Dal.Implementation
{
    public class NounDal : IDictionaryDal<Noun>
    {
        private readonly ApplicationDbContext _context;

        public NounDal(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Noun noun)
        {
            try
            {
                await _context.AddAsync(noun);
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
                var noun = await _context.Nouns.FirstOrDefaultAsync(n => n.Id == id);
                _context.Nouns.Remove(noun);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Noun>> GetAll()
        {
            var nouns = await _context.Nouns.ToListAsync();
            return nouns;
        }

        public async Task<bool> Update(Noun noun, int id)
        {
            try
            {
                var currentNoun = await _context.Nouns.FirstOrDefaultAsync(n => n.Id == id);
                currentNoun.NounWord = noun.NounWord;
                currentNoun.NounTranslate = noun.NounTranslate;
                _context.Nouns.Update(currentNoun);
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
