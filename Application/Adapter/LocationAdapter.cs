using Domain;

namespace Application.Adapter
{
    public class LocationAdapter
    {
        public static ModelDTO ToDTO(Model model)
        {
            return new ModelDTO()
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
        }


        public static Model ToDomain(ModelDTO modelDto)
        {
            return new Model()
            {
                LocationId = modelDto.LocationId,
                Identificador = modelDto.Identificador,
                Longitude = modelDto.Longitude,
                Latitude = modelDto.Latitude,
                Altitude = modelDto.Altitude,
                Accuracy = modelDto.Accuracy,
                Bearing = modelDto.Bearing,
                Speed = modelDto.Speed,
                Data = modelDto.Data
            };
        }
    }
}