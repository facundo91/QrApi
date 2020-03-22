using MediatR;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Contracts.v1.Responses;

namespace qrAPI.Commands.Pets.ControllerCommands
{
    public class CreatePetCommand : IRequest<PetResponse>
    {
        public CreatePetRequest CreatePetRequest { get; }

        public CreatePetCommand(CreatePetRequest createPetRequest)
        {
            CreatePetRequest = createPetRequest;
        }
    }
}