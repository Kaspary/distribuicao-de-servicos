using System;
using Application.Adapter;
using Domain;
using Domain.Interfaces;

namespace Application
{
    public class LocationApplication
    {
        ILocationRepository locationRepository;

        public LocationApplication(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        public Guid Insert(ModelDTO modelDto)
        {
            var model = LocationAdapter.ToDomain(modelDto);
            return locationRepository.Insert(model);
        }
    }
}