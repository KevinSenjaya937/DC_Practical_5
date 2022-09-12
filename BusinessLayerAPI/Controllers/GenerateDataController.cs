using DC_Practical_5.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusinessLayerAPI.Controllers
{
    [RoutePrefix("api/data")]
    public class GenerateDataController : ApiController
    {
        private static string URL = "http://localhost:56653/";
        private static RestClient client = new RestClient(URL);
        Random rand = new Random();
        private string[] firstnames = { "Jack", "Chris", "Matthew", "Ryan", "Michael", "Steven" };
        private string[] lastnames = { "Love", "Brown", "Green", "McCarter", "O'Brian", "Griffith" };
        private uint[] pins = { 1111, 2222, 3333, 4444, 5555, 6666 };
        private uint[] acctNos = { 000001, 000002, 000003, 000004, 000005, 000006 };
        private int[] balances = { -100, 0, 2000, 3000, 50000, 100000 };

        [Route("generate")]
        public void GenerateData()
        {
            RestRequest request = new RestRequest("api/customer", Method.Post);
            Customer customer = new Customer();

            for (int i = 0; i < 100; i++)
            {
                customer = GetNextAccount(customer);
                request.AddJsonBody(JsonConvert.SerializeObject(customer));
                RestResponse response = client.Execute(request);
            }
        }

        public Customer GetNextAccount(Customer customer)
        {
            customer.Pin_Number = pins[GenerateRandNum()].ToString();
            customer.Account_Number = (int)acctNos[GenerateRandNum()];
            customer.First_Name = firstnames[GenerateRandNum()];
            customer.Last_Name = lastnames[GenerateRandNum()];
            customer.Balance = balances[GenerateRandNum()];
            return customer;
        }

        private int GenerateRandNum()
        {
            return rand.Next(0, 5);
        }
    }
}
