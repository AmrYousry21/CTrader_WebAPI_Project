﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Web_API_Demo.Models;

namespace CTraderWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ZonesController : ControllerBase
    {

        string connectionString = "Server=DESKTOP-2HTGD7R;Database=CTrader;Trusted_Connection=True";

        [HttpGet]
        public IActionResult GetALLZones()
        {
            List<ZonesViewModel> zones = new List<ZonesViewModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Indecision_Candles", conn);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Check if the DataReader has any row.
                if (reader.HasRows)
                {
                    // Obtain a row from the query result.
                    while (reader.Read())
                    {
                        var zone = new ZonesViewModel
                        {
                            TimeS = reader.GetDateTime(0),
                            Zonetype = reader.GetString(1),
                            ZoneStatus = reader.GetBoolean(2)
                        };

                        zones.Add(zone);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                // Always call the Close method when you have finished using the DataReader object.
                reader.Close();

            }

            return Ok(zones);
        }

        [HttpGet("{id}")]
        public string GetZoneStatus(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM indecision_candles WHERE ZoneStatus = " + id, connectionString);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    return JsonConvert.SerializeObject(dt);
                    conn.Close();
                }

                else
                {
                    return "No data found";
                    conn.Close();
                }

            }
        }

        [HttpPut]
        public void AddNewZone()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "AddNewZone",
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }

        [HttpDelete("{id}")]

        public string Delete(int id)
        {
            return "";
        }

    }
}
