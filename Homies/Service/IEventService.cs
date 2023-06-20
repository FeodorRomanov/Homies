using Homies.ViewModels;

namespace Homies.Service
{
    public interface IEventService
    {
        public Task<IEnumerable<AllEventListViewModel>> GetAllEventListAsync();

        public Task<IEnumerable<JoinedEventListViewModel>> GetAllJoinedEventsAsync(string userId);

        public Task <AddEventViewModel> GetNewAddEventAsync();

        public Task<AddEventViewModel> GetNewEditEventAsync();

        public Task AddEventAsync(AddEventViewModel model,string id);

        public Task EditEventAsync(EditEventViewModel model, string id);

        Task<AddEventViewModel?> GetEventByIdAsync(int id);

        Task<EditEventViewModel?> GetEditEventByIdAsync(int id);
        Task AddEventToCollectionAsync(string userId, AddEventViewModel model);

        Task RemoveEventFromCollectionAsync(string userId, AddEventViewModel currEvent);

        Task EditBookAsync(AddEventViewModel viewModel,string userId); 
    }
}
