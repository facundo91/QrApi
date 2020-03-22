using MediatR;
using qrAPI.Domain;

namespace qrAPI.Commands.Pets.ServiceCommands
{
    public class CreatePetAsyncCommand : IRequest<Pet>
    {
        public Pet PetToCreate { get; }

        public CreatePetAsyncCommand(Pet petToCreate)
        {
            PetToCreate = petToCreate;
        }
    }
}