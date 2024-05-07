using API.Entities;
using API.Repositories;

namespace API.Services
{
    public class EmailService
    {
        private readonly EmailRepository _repo;

        public EmailService(EmailRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> AddNewEmailAsync (string email)
        {
            try
            {
                var entity = new EmailEntity() { Email = email };

                var result = await _repo.AddEmailAsync(entity);

                if (result)
                {
                    return true;
                }
            }

            catch { }

            return false;
        }
    }
}
