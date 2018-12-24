
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using FunctionApp2.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;



namespace FunctionApp2
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");


            //TblEmployee tbl = new TblEmployee();

            var datacon = new EmployeeDBContext();
            

                var a = datacon.TblEmployee.Where(b => b.Name == "chandra").Single(); //(from e in datacon.TblEmployee where e.name = "tck" select employee).firstordefaul;
            var c = datacon.TblEmployee.ToList().GroupBy(p=>p.Name);
                string name = a.Name.ToString() + req.Query["name"];
            
            
            

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello Mr, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }



    }

  
    }
