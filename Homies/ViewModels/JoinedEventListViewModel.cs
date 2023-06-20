﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System;

namespace Homies.ViewModels
{
    public class JoinedEventListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Start { get; set; }
        public string Type { get; set; } = null!;
        public string Organiser { get; set; } = null!;
    }
}

/*
< div class= "text-center row" >
    @foreach(var e in Model)
    {
        < div class= "card col-4" style = "width: 20rem; " >
            < div class= "card-body" >
                < h5 class= "card-title mt-1" > @e.Name </ h5 >
                < p class= "mb-0" >< span class= "fw-bold" > Starting time: </ span > @e.Start </ p >
                < p class= "mb-0" >< span class= "fw-bold" > Type: </ span > @e.Type </ p >
            </ div >

            @if(User.Identity.Name == e.Organiser)
            {
                < a asp - controller = "Event" asp - action = "Details" asp - route - id = "@e.Id" class= "btn btn-warning mb-2 w-100 p-3 fw-bold" > View Details </ a >
                < a asp - controller = "Event" asp - action = "Edit" asp - route - id = "@e.Id" class= "btn btn-warning mb-2 w-100 p-3 fw-bold" > Edit </ a >
            }
            else
{
                < form class= "input-group-sm " asp - controller = "Event" asp - action = "Leave" asp - route - id = "@e.Id" >
                    < input type = "submit" value = "Leave the Event" class= "fs-6 btn btn-warning mb-3 w-100 p-3 fw-bold" />
                </ form >
                < a asp - controller = "Event" asp - action = "Details" asp - route - id = "@e.Id" class= "btn btn-warning mb-2 w-100 p-3 fw-bold" > View Details </ a >
            }
        </ div >
    }
</ div >*/