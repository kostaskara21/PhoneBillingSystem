using TeleProgram.Models;

namespace TeleProgram.Interfaces
{
    public interface IPrograms

    {

        Task<List<Programs>> Index();

        Task<Programs?> Details(string id);

        Task<Programs> Create(Programs programs);

        Task<Programs?> Edit(string id, Programs programs);

        Task<Programs?> Delete(string id);


    }
}
