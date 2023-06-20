using Homies.Data;
using Homies.Data.Models;
using Homies.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System.Security.Claims;

namespace Homies.Service
{
    public class EventService : IEventService
    {
        private readonly HomiesDbContext dbContext;

        public EventService(HomiesDbContext dbContext)
        {
            this.dbContext=dbContext;
        }

        public async Task AddEventAsync(AddEventViewModel model,string id)
        {

            Event currEvent = new Event()
            {
                Id=model.Id,
                Name=model.Name,
                Description=model.Description,
                Start=model.Start,
                End=model.End,
                TypeId=model.TypeId,
                OrganiserId=id
        };

            await dbContext.Events.AddAsync(currEvent);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddEventToCollectionAsync(string userId, AddEventViewModel model)
        {
            bool eventAlreadyAdded = await dbContext.Participants
                .AnyAsync(x => x.HelperId == userId && x.EventId == model.Id);

            if (!eventAlreadyAdded)
            {
                var result = new EventParticipant()
                {
                    HelperId=userId,
                    EventId=model.Id,
                };

                await dbContext.Participants.AddAsync(result);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task EditBookAsync(AddEventViewModel viewModel, string userId)
        {
            var currEvent = await dbContext.Events.FindAsync(userId);

            if (currEvent != null)
            {
                currEvent.Id = viewModel.Id;
                currEvent.Start = viewModel.Start;
                currEvent.Name = viewModel.Name;
                currEvent.Description = viewModel.Description;
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task EditEventAsync(EditEventViewModel model, string id)
        {
            var dbEvent = await dbContext.Events
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (dbEvent != null)
            {
                dbEvent.Start = DateTime.Parse(model.Start);
                dbEvent.Name = model.Name;
                dbEvent.Description=model.Description;
                dbEvent.End = DateTime.Parse(model.End);
                dbEvent.TypeId = model.TypeId;

                await dbContext.SaveChangesAsync();
            }


        }

        public async Task<IEnumerable<AllEventListViewModel>> GetAllEventListAsync()
        {
            return await dbContext.Events
                .Select(x => new AllEventListViewModel()
                {
                    Id = x.Id,
                    Name=x.Name,
                    Start=x.Start,
                    Type=x.Type.Name,
                    Organiser=x.Organiser.UserName
                }).ToListAsync();
        }

        public async Task<IEnumerable<JoinedEventListViewModel>> GetAllJoinedEventsAsync(string userId)
        {
            return await dbContext.Participants
                .Where(x => x.HelperId == userId)
                .Select(x => new JoinedEventListViewModel()
                {
                    Id = x.Event.Id,
                    Name = x.Event.Name,
                    Start = x.Event.Start,
                    Type = x.Event.Type.Name,
                    Organiser = x.Event.Organiser.UserName
                }).ToListAsync();
        }

        public async Task<EditEventViewModel?> GetEditEventByIdAsync(int id)
        {
            var types = await dbContext.Types
                .Select(x => new TypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

            return await dbContext.Events
                .Where(x => x.Id == id)
                .Select(x => new EditEventViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description=x.Description,
                    Start=x.Start.ToString(),
                    End=x.End.ToString(),
                    TypeId=x.TypeId,
                    Types=types

                }).FirstOrDefaultAsync();
        }

        public async Task<AddEventViewModel?> GetEventByIdAsync(int id)
        {

            return await dbContext.Events
                .Where(x => x.Id == id)
                .Select(x => new AddEventViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Start = x.Start,
                    End = x.End,
                    TypeId = x.TypeId
                }).FirstOrDefaultAsync();
        }

        public async Task<AddEventViewModel> GetNewAddEventAsync()
        {
            var types = await dbContext.Types
                .Select(x => new TypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

            var model = new AddEventViewModel
            {
                Types = types
            };

            return model;
        }

        public Task<AddEventViewModel> GetNewEditEventAsync()
        {
            throw new NotImplementedException();
        }

        public async Task RemoveEventFromCollectionAsync(string userId, AddEventViewModel currEvent)
        {
            var userEvent = await dbContext.Participants
                    .FirstOrDefaultAsync(x => x.EventId == currEvent.Id && x.HelperId == userId);

            if (userEvent != null)
            {
                dbContext.Participants.Remove(userEvent);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
