using TeleProgram.Models;

namespace TeleProgram.Interfaces
{
    public interface IBills
    {

        Task<List<Bills>> Index(string id);


        Task<Boolean> Create(string PhoneNumber );

        Task<Boolean> Delete(string phonenumber);
    }
}
