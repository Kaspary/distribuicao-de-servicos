using System;
using Domain;
using Domain.Interfaces;
using Npgsql;

namespace Repository
{
    public class LocationRepository : ILocationRepository
    {
        private string connectionString;

        public LocationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Guid Insert(Model model)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                //Inicia a transação

                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        model.LocationId = Guid.NewGuid();

                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;

                        cmd.CommandText =
                            @"INSERT Into public.location (location_id,latitude,longitude,altitude,speed,accuracy,bearing,date,identificador)values(@location_id,@latitude,@longitude,@altitude,@speed,@accuracy,@bearing,@date,@identificador)";
                        cmd.Parameters.AddWithValue("location_id", model.LocationId);
                        cmd.Parameters.AddWithValue("latitude", model.Latitude);
                        cmd.Parameters.AddWithValue("longitude", model.Longitude);
                        cmd.Parameters.AddWithValue("altitude", model.Altitude);
                        cmd.Parameters.AddWithValue("speed", model.Speed);
                        cmd.Parameters.AddWithValue("accuracy", model.Accuracy);
                        cmd.Parameters.AddWithValue("bearing", model.Bearing);
                        cmd.Parameters.AddWithValue("date", model.Data);
                        cmd.Parameters.AddWithValue("identificador", model.Identificador);
                        cmd.ExecuteNonQuery();
                        trans.Commit();
                        return model.LocationId;
                    }
                    catch (Exception exception)
                    {
                        trans.Rollback();
                        throw exception;
                    }
                }
            }
        }
    }
}