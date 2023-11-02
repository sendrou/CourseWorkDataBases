using Lab02;
using Lab3.Services;
using Lab3.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Globalization;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Lab03
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Lab1;Trusted_Connection=True;";
            services.AddDbContext<Lab1Context>(options => options.UseSqlServer(connectionString));

            services.AddTransient<CachedCarBrandsService>();

            services.AddTransient<CachedCarsService>();

            services.AddTransient<CachedDriversService>();

            services.AddTransient<CachedTransportationTariffsService>();

            services.AddTransient<CachedSettlemensService>();

            services.AddTransient<CachedDistancesService>();

            services.AddTransient<CachedOrganizationsService>();

            services.AddTransient<CachedLoadsService>();

            services.AddTransient<CachedCargoTransportationsService>();

            services.AddDistributedMemoryCache();

            services.AddSession();

            services.AddMemoryCache();

            services.AddControllersWithViews();
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Map("/info", Info);
            app.Map("/brandCars", BrandCars);
            app.Map("/cars", Cars);
            app.Map("/drivers", Drivers);
            app.Map("/transportationTariffs", TransportationTariffs);
            app.Map("/settlements", Settlements);
            app.Map("/distances", Distances);
            app.Map("/organizations", Organizations);
            app.Map("/loads", Loads);
            app.Map("/cargoTransportations", CargoTransportations);

            app.Map("/searchform1", FirstForm);
            app.Map("/getResultsForm1", GetResultsForm1);
            app.UseSession();
            app.Map("/searchform2", SecondForm);
            app.Map("/getResultsForm2", GetResultsForm2);





            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
        private static void GetResultsForm2(IApplicationBuilder app)
        {
            app.Run((context) =>
            {

                var form = context.Request.Form;
                context.Session.SetInt32("DocumentId", Convert.ToInt32(form["DocumentId"]));
                context.Session.SetInt32("OrganizationId", Convert.ToInt32(form["organizations"]));

                CachedCargoTransportationsService cargoService = context.RequestServices.GetService<CachedCargoTransportationsService>();
                List<CargoTransportation> cargos = cargoService.GetCargoTransportationsAll().Where(cargo => cargo.DocumentId > context.Session.GetInt32("DocumentId") && cargo.OrganizationId == context.Session.GetInt32("OrganizationId")).ToList();
                if (cargos != null)
                {
                    StringBuilder stringBuilder = new StringBuilder();

                    foreach (var cargo in cargos)
                    {
                        string formattedDate = cargo.Date.HasValue ? $"{cargo.Date.Value.Day:00}.{cargo.Date.Value.Month:00}.{cargo.Date.Value.Year}" : string.Empty;
                        stringBuilder.Append($"DocumentId = {cargo.DocumentId}   Date = {formattedDate} OrganizationId = {cargo.OrganizationId} DistanceId = {cargo.DistanceId} DriverId = {cargo.DriverId} CarId = {cargo.CarId} LoadId = {cargo.LoadId} TransportationTariffId = {cargo.TransportationTariffId}" + Environment.NewLine);
                    }
                    return context.Response.WriteAsync(stringBuilder.ToString());
                }
                return context.Response.WriteAsync("Not found");


            });
        }

        private static void SecondForm(IApplicationBuilder app)
        {

            app.Run((context) =>
            {

                CachedOrganizationsService corganizationsService = context.RequestServices.GetService<CachedOrganizationsService>();
                List<Organization> organizations = corganizationsService.GetOrganizationsAll().ToList();
                StringBuilder sb = new StringBuilder();
                sb.Append("<html><meta charset=\"UTF-8\"><body>Searching in Cargo Transportation");
                if (context.Session.Keys.Contains("DocumentId") && context.Session.Keys.Contains("OrganizationId"))
                {
                    int? DocumentId = context.Session.GetInt32("DocumentId");
                    sb.Append("<br><form method=\"post\" action = \"getResultsForm2\" > Min DocumentID:<br><input type = 'number' name = 'DocumentId' value = " + DocumentId + ">");
                    sb.Append("<br>Организация<br>");
                    sb.Append("<select name='organizations'>");
                    foreach (var item in organizations)
                    {
                        if (item.OrganizationId == context.Session.GetInt32("OrganizationId"))
                        {
                            sb.Append($"<option value={item.OrganizationId} selected>{item.OrganizationName}</option>");
                        }
                        else
                        {
                            sb.Append($"<option value={item.OrganizationId}>{item.OrganizationName}</option>");
                        }
                    }

                    sb.Append("</select><br><input type = 'submit' value = 'Submit' ></form></body></html>");
                }
                else
                {
                    sb.Append("<br><form method=\"post\" action = \"getResultsForm2\" > Min DocumentID:<br><input type = 'number' name = 'DocumentId'" + ">");
                    sb.Append("<br>Организация<br>");
                    sb.Append("<select name='organizations'>");
                    foreach (var item in organizations)
                    {
                        sb.Append($"<option value={item.OrganizationId}>{item.OrganizationName}</option>");
                    }
                    sb.Append("</select><br><input type = 'submit' value = 'Submit' ></form></body></html>");
                }

                return context.Response.WriteAsync(sb.ToString());




            });
        }

        private static void GetResultsForm1(IApplicationBuilder app)
        {
            app.Run((context) =>
            {
                var form = context.Request.Form;
                context.Response.Cookies.Append("minId", form["Id"]);
                context.Response.Cookies.Append("Cost", form["Cost"]);

                int minId = Convert.ToInt32(form["Id"]);
                int cost = Convert.ToInt32(form["Cost"]);

                CachedTransportationTariffsService transportationTariffsService = context.RequestServices.GetService<CachedTransportationTariffsService>();
                List<TransportationTariff> transportationTariffs = transportationTariffsService.GetTransportationTariffsAll().Where(t => t.TransportationTariffId > minId && t.TariffPerTKm == cost ).ToList();
                if (transportationTariffs != null)
                {
                    StringBuilder stringBuilder = new StringBuilder();

                    foreach (var transportationTariff in transportationTariffs)
                    {
                        stringBuilder.Append($"TransportationTariffId = {transportationTariff.TransportationTariffId} TariffPerTKm = {transportationTariff.TariffPerTKm}  TariffPerM3Km = {transportationTariff.TariffPerM3Km}  " + Environment.NewLine);
                    }
                    return context.Response.WriteAsync(stringBuilder.ToString());
                }
                return context.Response.WriteAsync("Not found");

            });
        }

        private static void FirstForm(IApplicationBuilder app)
        {
            app.Run((context) =>
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<html><meta charset=\"UTF-8\"><body>Searching in TransportationTariffs");

                CachedTransportationTariffsService transportationTariffsService = context.RequestServices.GetService<CachedTransportationTariffsService>();
                List<TransportationTariff> transportationTariffs = transportationTariffsService.GetTransportationTariffsAll().ToList();

                if (context.Request.Cookies.ContainsKey("minId") && context.Request.Cookies.ContainsKey("Cost"))
                {
                    int minId = Convert.ToInt32(context.Request.Cookies["minId"]);
                    int minCost = Convert.ToInt32(context.Request.Cookies["Cost"]);

                    sb.Append("<br><form method=\"post\" action=\"getResultsForm1\">");
                    sb.Append("Min ID:<br><input type='number' name='Id' value='" + minId + "'>");
                    sb.Append("<br>Cost:<br><select name='Cost'>");

                    for (int cost = 1; cost <= 100; cost++)
                    {
                        if (cost == minCost)
                        {
                            sb.Append($"<option value='{cost}' selected>{cost}</option>");
                        }
                        else
                        {
                            sb.Append($"<option value='{cost}'>{cost}</option>");
                        }
                    }

                    sb.Append("</select><br><input type='submit' value='Submit'></form></body></html>");
                }
                else
                {
                    sb.Append("<br><form method=\"post\" action=\"getResultsForm1\">");
                    sb.Append("Min ID:<br><input type='number' name='Id'>");
                    sb.Append("<br>Cost:<br><select name='Cost'>");

                    for (int cost = 1; cost <= 100; cost++)
                    {
                        sb.Append($"<option value='{cost}'>{cost}</option>");
                    }

                    sb.Append("</select><br><input type='submit' value='Submit'></form></body></html>");
                }

                return context.Response.WriteAsync(sb.ToString());
            });
        }

        private static void CargoTransportations(IApplicationBuilder app)
            {
                app.Run((context) =>
                {
                    CachedCargoTransportationsService cargoTransportationsService = context.RequestServices.GetService<CachedCargoTransportationsService>();
                    List<CargoTransportation> cargoTransportations = cargoTransportationsService.GetCargoTransportations().ToList();
                    if (cargoTransportations != null)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        foreach (var cargoTransportation in cargoTransportations)
                        {
                            string formattedDate = cargoTransportation.Date.HasValue ? $"{cargoTransportation.Date.Value.Day:00}.{cargoTransportation.Date.Value.Month:00}.{cargoTransportation.Date.Value.Year}" : string.Empty;
                            stringBuilder.Append($"DocumentId = {cargoTransportation.DocumentId}   Date = {formattedDate} OrganizationId = {cargoTransportation.OrganizationId} DistanceId = {cargoTransportation.DistanceId} DriverId = {cargoTransportation.DriverId} CarId = {cargoTransportation.CarId} LoadId = {cargoTransportation.LoadId} TransportationTariffId = {cargoTransportation.TransportationTariffId}" + Environment.NewLine);
                        }
                        string responseText = stringBuilder.ToString();
                        byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);

                        context.Response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                        return context.Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);
                    }
                    return context.Response.WriteAsync("Results not found");
                });
            }
        private static void Loads(IApplicationBuilder app)
            {
                app.Run((context) =>
                {
                    CachedLoadsService loadsService = context.RequestServices.GetService<CachedLoadsService>();
                    List<Load> loads = loadsService.GetLoads().ToList();
                    if (loads != null)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        foreach (var load in loads)
                        {
                            stringBuilder.Append($"LoadId = {load.LoadId}   LoadName = {load.LoadName} Volume = {load.Volume} Weight = {load.Weight}" + Environment.NewLine);
                        }
                        string responseText = stringBuilder.ToString();
                        byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);

                        context.Response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                        return context.Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);
                    }
                    return context.Response.WriteAsync("Results not found");
                });
            }
        private static void Organizations(IApplicationBuilder app)
            {
                app.Run((context) =>
                {
                    CachedOrganizationsService organizationsService = context.RequestServices.GetService<CachedOrganizationsService>();
                    List<Organization> organizations = organizationsService.GetOrganizations().ToList();
                    if (organizations != null)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        foreach (var organization in organizations)
                        {
                            stringBuilder.Append($"OrganizationId = {organization.OrganizationId}   OrganizationName = {organization.OrganizationName}" + Environment.NewLine);
                        }
                        string responseText = stringBuilder.ToString();
                        byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);

                        context.Response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                        return context.Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);
                    }
                    return context.Response.WriteAsync("Results not found");
                });
            }

        private static void Distances(IApplicationBuilder app)
            {
                app.Run((context) =>
                {
                    CachedDistancesService distancesService = context.RequestServices.GetService<CachedDistancesService>();
                    List<Distance> distances = distancesService.GetSettlements().ToList();
                    if (distances != null)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        foreach (var distance in distances)
                        {
                            stringBuilder.Append($"DistanceId = {distance.DistanceId}   DeparturesSettlementId = {distance.DeparturesSettlementId} ArrivalSettlementId = {distance.ArrivalSettlementId} Distance = {distance.Distance1} " + Environment.NewLine);
                        }
                        string responseText = stringBuilder.ToString();
                        byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);

                        context.Response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                        return context.Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);
                    }
                    return context.Response.WriteAsync("Results not found");
                });
            }

        private static void Settlements(IApplicationBuilder app)
            {
                app.Run((context) =>
                {
                    CachedSettlemensService settlementsService = context.RequestServices.GetService<CachedSettlemensService>();
                    List<Settlement> settlements = settlementsService.GetSettlements().ToList();
                    if (settlements != null)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        foreach (var settlement in settlements)
                        {
                            stringBuilder.Append($"SettlementId = {settlement.SettlementId}   SettlementName = {settlement.SettlementName} " + Environment.NewLine);
                        }
                        string responseText = stringBuilder.ToString();
                        byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);

                        context.Response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                        return context.Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);
                    }
                    return context.Response.WriteAsync("Results not found");
                });
            }

        private static void TransportationTariffs(IApplicationBuilder app)
            {
                app.Run((context) =>
                {
                    CachedTransportationTariffsService transportationTariffsService = context.RequestServices.GetService<CachedTransportationTariffsService>();
                    List<TransportationTariff> transportationTariffs = transportationTariffsService.GetTransportationTariffs().ToList();
                    if (transportationTariffs != null)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        foreach (var transportationTariff in transportationTariffs)
                        {
                            stringBuilder.Append($"TransportationTariffId = {transportationTariff.TransportationTariffId}   TariffPer(m^3*km) = {transportationTariff.TariffPerM3Km} TariffPer(t*km) = {transportationTariff.TariffPerTKm} " + Environment.NewLine);
                        }
                        string responseText = stringBuilder.ToString();
                        byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);

                        context.Response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                        return context.Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);
                    }
                    return context.Response.WriteAsync("Results not found");
                });
            }

        private static void Drivers(IApplicationBuilder app)
            {
                app.Run((context) =>
                {
                    CachedDriversService driversService = context.RequestServices.GetService<CachedDriversService>();
                    List<Driver> drivers = driversService.GetDrivers().ToList();
                    if (drivers != null)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        foreach (var driver in drivers)
                        {
                            stringBuilder.Append($"DriverId = {driver.DriverId}   FullName = {driver.FullName} PassportDetails = {driver.PassportDetails} " + Environment.NewLine);
                        }
                        string responseText = stringBuilder.ToString();
                        byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);

                        context.Response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                        return context.Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);
                    }
                    return context.Response.WriteAsync("Results not found");
                });
            }

        private static void BrandCars(IApplicationBuilder app)
            {
                app.Run((context) =>
                {
                    CachedCarBrandsService brandCarsService = context.RequestServices.GetService<CachedCarBrandsService>();
                    List<CarBrand> brandsCar = brandCarsService.GetCarBrands().ToList();
                    if (brandsCar != null)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        foreach (var brandCar in brandsCar)
                        {
                            stringBuilder.Append($"BrandId = {brandCar.CarBrandId}   BrandName = {brandCar.BrandName}  " + Environment.NewLine);
                        }
                        string responseText = stringBuilder.ToString();
                        byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);

                        context.Response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                        return context.Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);
                    }
                    return context.Response.WriteAsync("Results not found");
                });
            }

        private static void Cars(IApplicationBuilder app)
            {
                app.Run((context) =>
                {
                    CachedCarsService carsService = context.RequestServices.GetService<CachedCarsService>();
                    List<Car> cars = carsService.GetCars().ToList();
                    if (cars != null)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        foreach (var car in cars)
                        {
                            stringBuilder.Append($"CarId = {car.CarId}   CarBrandId = {car.CarBrandId} LiftingCapacity = {car.LiftingCapacity} BodyVolume = {car.BodyVolume} RegistrationNumber = {car.RegistrationNumber}" + Environment.NewLine);
                        }
                       return context.Response.WriteAsync(stringBuilder.ToString());

                    }
                    return context.Response.WriteAsync("Results not found");
                });
            }
            private static void Info(IApplicationBuilder app)
            {
                app.Run((context) =>
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("UserName: " + Environment.UserName);
                    stringBuilder.AppendLine("OS Version: " + Environment.OSVersion);
                    stringBuilder.AppendLine("ProcessorCount: " + Environment.ProcessorCount);
                    return context.Response.WriteAsync(stringBuilder.ToString());
                });
            }
        }
    }

