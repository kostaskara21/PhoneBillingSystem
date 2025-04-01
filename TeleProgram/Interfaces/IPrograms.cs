using TeleProgram.Models;

namespace TeleProgram.Interfaces
{
    public interface IPrograms

    {

        Task<List<Programs>> Index();

        Task<Programs?> Details(string id);

        Task<Boolean> Create(Programs programs);

        Task<Programs?> Edit(string id,string desc,decimal charg);

        Task<Programs?> Delete(string id);


    }
}
