using Homies.Service;
using Homies.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Homies.Controllers
{
    public class EventController : BaseController
    {

        private readonly IEventService eventService;

        public EventController(IEventService service)
        {
            this.eventService= service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var model=await eventService.GetAllEventListAsync();

            return View(model);
        }

        public async Task<IActionResult> Joined()
        {
            var currUser = GetUserId();

            var model = await eventService.GetAllJoinedEventsAsync(currUser);

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model=await eventService.GetNewAddEventAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEventViewModel model)
        {
            var userId= GetUserId();

            await eventService.AddEventAsync(model,userId);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Join(int id)
        {
            var currEvent = await eventService.GetEventByIdAsync(id);

            if (currEvent == null)
            {
                return RedirectToAction(nameof(All));
            }

            var userId = GetUserId();

            await eventService.AddEventToCollectionAsync(userId, currEvent);

            return RedirectToAction(nameof(Joined));
        }

        public async Task<IActionResult> Leave(int id)
        {
            var curr = await eventService.GetEventByIdAsync(id);

            var userId = GetUserId();

            await eventService.RemoveEventFromCollectionAsync(userId, curr);

            return RedirectToAction(nameof(All));

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await eventService.GetEditEventByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEventViewModel model)
        {
            var userId = GetUserId();

            await eventService.EditEventAsync(model, userId);

            return RedirectToAction(nameof(All));
        }
    }
}
