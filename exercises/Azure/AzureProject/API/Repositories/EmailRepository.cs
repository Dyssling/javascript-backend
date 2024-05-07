using API.Contexts;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace API.Repositories
{
    public class EmailRepository
    {
        private DataContext _context;

        public EmailRepository(DataContext context)
        {
            _context = context;
        }

        public virtual async Task<bool> AddEmailAsync(EmailEntity entity)
        {
            try
            {
                _context.Emails.Add(entity);
                await _context.SaveChangesAsync();

                return true; //Om det gick att lägga till entiteten så får man ut true
            }
            catch (Exception ex) //Annars får man felmeddelandet i debuggern, och man får false tillbaka. På så vis kan man göra lite kontroller sedan i servicen
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
