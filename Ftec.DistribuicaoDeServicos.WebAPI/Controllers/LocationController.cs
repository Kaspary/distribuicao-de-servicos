using System;
using System.Configuration;
using System.Web.Http;
using Application;
using Domain;
using Domain.Interfaces;
using Repository;
using Model = Ftec.DistribuicaoDeServicos.WebAPI.Models.Model;

namespace Ftec.DistribuicaoDeServicos.WebAPI.Controllers
{
    public class LocationController : ApiController
    {
        private ILocationRepository locationRepository;
        private LocationApplication locationApplication;

        public LocationController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexao"].ToString();
            locationRepository = new LocationRepository(connectionString);
            locationApplication = new LocationApplication(locationRepository);
        }

        public Guid Post(Model model)
        {
            try
            {
                Guid id = Insert(model);
                return id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private Guid Insert(Model model)
        {
            ModelDTO modelDto = new ModelDTO()
            {
                LocationId = model.LocationId,
                Identificador = model.Identificador,
                Longitude = model.Longitude,
                Latitude = model.Latitude,
                Altitude = model.Altitude,
                Accuracy = model.Accuracy,
                Bearing = model.Bearing,
                Speed = model.Speed,
                Data = model.Data
            };
            return locationApplication.Insert(modelDto);
        }
    }
}