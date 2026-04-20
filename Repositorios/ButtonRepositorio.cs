using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using webAPI_ASPNET.Data;
using webAPI_ASPNET.Models;
using webAPI_ASPNET.Repositorios.Interfaces;

namespace webAPI_ASPNET.Repositorios
{
    public class ButtonRepositorio : IButton
    {
        private readonly AppDbContext _dbContext;
        public ButtonRepositorio(AppDbContext AppDbContext)
        {
            _dbContext = AppDbContext;
        }

        public async Task<List<Button>> getAll()
        {
            return await _dbContext.Buttons.ToListAsync();
        }

        public async Task<Button> getById(int id)
        {
            return await _dbContext.Buttons.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Button> post(Button button)
        {
            await _dbContext.Buttons.AddAsync(button);
            await _dbContext.SaveChangesAsync();

            return button;
        }

        public async Task<Button> put(Button button, int id)
        {
            Button buttonById = await getById(id);
            if (buttonById == null)
            {
                throw new Exception($"Button didn't found");
            }

            buttonById.BUTTONNAME = button.BUTTONNAME;
            buttonById.DESCRIPTION = button.DESCRIPTION;

            _dbContext.Buttons.Update(buttonById);
            await _dbContext.SaveChangesAsync();

            return buttonById;
        }

        public async Task<bool> delete(int id)
        {
            Button buttonById = await getById(id);
            if (buttonById == null) {
                throw new Exception($"Button didn't found");
            }

            _dbContext.Buttons.Remove(buttonById);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public class ButtonRelationRepositorio : IButtonRelation
        {
            private readonly AppDbContext _dbContext;

            public ButtonRelationRepositorio(AppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<List<ButtonRelation>> getAll()
            {
                return await _dbContext.ButtonRelation.ToListAsync();
            }

            public async Task<ButtonRelation> getById(int id)
            {
                return await _dbContext.ButtonRelation.FirstOrDefaultAsync(x => x.ID == id);
            }
            public async Task<ButtonRelation> getByCodUserAndCodButton(int idUser, int idButton)
            {
                return await _dbContext.ButtonRelation.FirstOrDefaultAsync(br => br.IDUSER == idUser && br.IDBUTTON == idButton);
            }

            public async Task<List<UserWithButton>> getUserIButtonByCodUser(int idUSER)
            {
                return await (from u in _dbContext.User
                              join ud in _dbContext.ButtonRelation on u.ID equals ud.IDUSER into userButtons
                              from ud in userButtons.DefaultIfEmpty() 
                              join d in _dbContext.Buttons on ud.IDBUTTON equals d.ID into buttons
                              from d in buttons.DefaultIfEmpty() 
                              where ud.IDUSER == idUSER
                              select new UserWithButton 
                              {
                                  User = u,
                                  ButtonRelation = ud, 
                                  Button = d 
                              }).ToListAsync();
            }

            public async Task<ButtonRelation> post(ButtonRelation button)
            {
                await _dbContext.AddAsync(button);
                await _dbContext.SaveChangesAsync();
                return button;
            }

            public async Task<ButtonRelation> put(ButtonRelation button, int id)
            {
                ButtonRelation buttonRelationById = await getById(id);
                if (buttonRelationById == null)
                {
                    throw new Exception("Button relation not found");
                }

                buttonRelationById.IDBUTTON = button.IDBUTTON;
                buttonRelationById.IDUSER = button.IDUSER;

                _dbContext.ButtonRelation.Update(buttonRelationById);
                await _dbContext.SaveChangesAsync();

                return buttonRelationById;

            }
            public async Task<bool> delete(int id)
            {
                ButtonRelation buttonRelationById = await getById(id);
                if (buttonRelationById == null)
                {
                    throw new Exception("Button relation not found");
                }

                _dbContext.ButtonRelation.Remove(buttonRelationById);
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
