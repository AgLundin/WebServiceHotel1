using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebServiceHotel1
{
    public class DBHotelClient
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hotel;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void Start()
        {
            Console.WriteLine("List all facilities:");
            ListFacilities();
            Console.WriteLine();

            Console.WriteLine($"List facility #{3}: ");
            ListFacility(3);
            Console.WriteLine();

            int nextFacilityID = GetMaxFacilityID();
            Console.WriteLine($"Create facility with #{nextFacilityID}:");
            CreateFacility(10, 1, 8, 21, "Poledance");

            Console.WriteLine($"List facility #{nextFacilityID}:");
            ListFacility(nextFacilityID);
            Console.WriteLine();

            Console.WriteLine($"Update facility with #{nextFacilityID}:");
            UpdateFacility(4, 4, 11, 19, "Stripklub");
            Console.WriteLine();

            Console.WriteLine($"List facility #{5}:");
            ListFacility(5);
            Console.WriteLine();

            Console.WriteLine($"Delete facility with #{4}:");
            DeleteFacility(4);
            Console.WriteLine();

            Console.WriteLine("List all facilities:");
            ListFacilities();
            Console.WriteLine();
        }

        public void ListFacility(int facilityID)
        {
            ListFacilities($"DemoFacilities.Facility_ID = {facilityID}");
        }

        public void ListFacilities(string whereClause = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT DemoFacilities.Facility_ID, DemoFacilities.Hotel_No, DemoFacilities.TimeSlot_From, DemoFacilities.TimeSlot_To, FacilitiesDescription.Facility_ID, FacilitiesDescription.Description FROM DemoFacilities INNER JOIN FacilitiesDescription ON DemoFacilities.Facility_ID = FacilitiesDescription.Facility_ID";

                if (whereClause != null)
                {
                    queryString += " WHERE " + whereClause;
                }
                Console.WriteLine($"SQL: {queryString}");
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int Facility_ID = reader.GetInt32(0);

                    int Id = reader.GetInt32(4);

                    string Description = reader.GetString(5);

                    int Hotel_No = reader.GetInt32(1);

                    decimal TimeSlot_From = reader.GetDecimal(2);

                    decimal TimeSlot_To = reader.GetDecimal(3);

                    Console.WriteLine($" Facility_ID: {Facility_ID} Hotel_No: {Hotel_No} TimeSlot_From: {TimeSlot_From} TimeSlot_To: {TimeSlot_To} Description: {Description} ");
                }
                command.Connection.Close();
            }
        }

        public void ExecuteNonQueryFacility(string queryString)
        {
            Console.WriteLine($"SQL: {queryString}");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
        }

        public void CreateFacility(int facilityID, int hotelNo, decimal timeSlotFrom, decimal timeSlotTo, string description)
        {
            ExecuteNonQueryFacility($"INSERT INTO DemoFacilities VALUES ({facilityID}, {hotelNo}, {timeSlotFrom}, {timeSlotTo})");
            ExecuteNonQueryFacility($"INSERT INTO FacilitiesDescription VALUES ({facilityID}, '{description}')");
        }

        public void UpdateFacility(int facilityID, int hotelNo, decimal timeSlotFrom, decimal timeSlotTo, string description)
        {
            ExecuteNonQueryFacility($"UPDATE DemoFacilities SET Hotel_No={hotelNo}, TimeSlot_From={timeSlotFrom}, TimeSlot_To={timeSlotTo} WHERE Facility_ID={facilityID}");
            ExecuteNonQueryFacility($"UPDATE FacilitiesDescription SET Description='{description}' WHERE Facility_ID={facilityID}");
        }

        public void DeleteFacility(int facilityID)
        {
            ExecuteNonQueryFacility($"DELETE FROM DemoFacilities WHERE Facility_ID={facilityID}");
            ExecuteNonQueryFacility($"DELETE FROM FacilitiesDescription WHERE Facility_ID={facilityID}");
        }

        public int GetMaxFacilityID()
        {
            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT MAX(Facility_ID) FROM DemoFacilities";
                Console.WriteLine($"SQL: {queryString}");

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = reader.GetInt32(0);
                }

                command.Connection.Close();
            }
            return result;
        }
    }
}