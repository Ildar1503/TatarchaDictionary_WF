using Dictionary.Dal.Interfaces;
using DictionaryTatarcha.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Dal.Implementation
{
    public class VerbDal : IDictionaryDal<Verb>
    {
        private readonly ApplicationDbContext _context;

        public VerbDal(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Verb verb)
        {
            try
            {
                await _context.Verbs.AddAsync(verb);
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
                var verb = await _context.Verbs.FirstOrDefaultAsync(v => v.Id == id);
                _context.Verbs.Remove(verb);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Verb>> GetAll()
        {
            var verbs = await _context.Verbs.ToListAsync();
            return verbs;
        }

        public async Task<bool> Update(Verb verb, int id)
        {
            try
            { 
                var currentVerb = await _context.Verbs.FirstOrDefaultAsync(v => v.Id == id);
                currentVerb.VerbTranslate = verb.VerbTranslate;
                currentVerb.VerbWord = verb.VerbWord;
                _context.Verbs.Update(currentVerb);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
