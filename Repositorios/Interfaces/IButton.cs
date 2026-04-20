using webAPI_ASPNET.Models;

namespace webAPI_ASPNET.Repositorios.Interfaces
{
    public interface IButton
    {
        Task<List<Button>> getAll();
        Task<Button> getById(int id);
        Task<Button> post(Button button);
        Task<Button> put(Button button, int id);
        Task<bool> delete(int id);

    }

    public interface IButtonRelation
    {
        Task<List<ButtonRelation>> getAll();
        Task<ButtonRelation> getById(int id);
        Task<ButtonRelation> getByCodUserAndCodButton(int idUser, int idButton);
        Task<List<UserWithButton>> getUserIButtonByCodUser(int idUSER);
        Task<ButtonRelation> post(ButtonRelation button);
        Task<ButtonRelation> put(ButtonRelation button, int id);
        Task<bool> delete(int id);
    }
}
