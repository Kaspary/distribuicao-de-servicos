using System;
using System.Configuration;
using System.Web.Http;
using Ftec.DistribuicaoDeServicos.WebAPI.Models;
using Npgsql;

namespace Ftec.DistribuicaoDeServicos.WebAPI.Controllers
{
    public class LocationController : ApiController
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["conexao"].ToString();

        //injetando a dependencia do repositorio na aplicação


        public Guid Post(Model model)
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
                        
                        cmd.CommandText = @"INSERT Into public.location (location_id,latitude,longitude,altitude,speed,accuracy,bearing,date,identificador)values(@location_id,@latitude,@longitude,@altitude,@speed,@accuracy,@bearing,@date,@identificador)";
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