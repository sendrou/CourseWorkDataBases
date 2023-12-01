using Cargo.Models;
using System.Text;
using System;
using System.Linq;

namespace Cargo
{
    public class DbInitilializer
    {
        public static void Initialize(CargoContext db)
        {
            db.Database.EnsureCreated();

            if (db.CargoTransportations.Any())
            {
                return;
            }

            Random randomObj = new Random(1);
            DateTime startDate = new DateTime(2020, 1, 1);
            DateTime endDate = new DateTime(2023, 12, 31);
            //Организации

            int organizationNumber = 500;
            string organizationName;

            int organizationId;

            for (organizationId = 1; organizationId <= organizationNumber; organizationId++)
            {
                organizationName = "Организация_" + organizationId.ToString();
                db.Organizations.Add(new Organization { OrganizationName = organizationName });
            }
            db.SaveChanges();

            //Населённые пункты

            int settlementNumber = 500;
            string settlementName;

            int settlementId;

            for (settlementId = 1; settlementId <= settlementNumber; settlementId++)
            {
                settlementName = "Населенныйпункт_" + settlementId.ToString();
                db.Settlements.Add(new Settlement { SettlementName = settlementName });
            }
            db.SaveChanges();

            //Водители

            int driverNumber = 500;
            string driverName, driverPassport;

            int driverId;


            for (driverId = 1; driverId <= driverNumber; driverId++)
            {
                driverName = "Водитель_" + driverId.ToString();
                driverPassport = "HB";

                for (int i = 0; i < 7; i++)
                {
                    int randomNumber = randomObj.Next(10);
                    driverPassport += randomNumber;
                }

                db.Drivers.Add(new Driver { FullName = driverName, PassportDetails = driverPassport });
            }
            db.SaveChanges();


            //Тарифы

            int tariffNumber = 500;
            int tariffPerT, tariffPerM3;

            int tariffId;


            for (tariffId = 1; tariffId <= tariffNumber; tariffId++)
            {
                tariffPerT = randomObj.Next(1, 100);
                tariffPerM3 = randomObj.Next(1, 100);
                db.TransportationTariffs.Add(new TransportationTariff { TariffPerTKm = tariffPerT, TariffPerM3Km = tariffPerM3 });
            }
            db.SaveChanges();



            //Марки

            int carBrandNumber = 500;
            string carBrandName;

            int carBrandId;


            for (carBrandId = 1; carBrandId <= carBrandNumber; carBrandId++)
            {
                carBrandName = "Марка_" + carBrandId.ToString();
                db.CarBrands.Add(new CarBrand { BrandName = carBrandName });
            }
            db.SaveChanges();


            //Груз

            int loadNumber = 500;
            string loadName;
            int loadVolume, loadWeight;

            int loadId;


            for (loadId = 1; loadId <= loadNumber; loadId++)
            {
                loadName = "Груз_" + loadId.ToString();
                loadVolume = randomObj.Next(100, 1000);
                loadWeight = randomObj.Next(100, 1000);
                db.Loads.Add(new Load { LoadName = loadName, Volume = loadVolume, Weight = loadWeight });
            }
            db.SaveChanges();

            //Автомобили

            int carNumber = 20000;
            int carBrandID, liftingCapacity, bodyVolume;
            string registrationNumber;

            int carId;


            for (carId = 1; carId <= carNumber; carId++)
            {
                carBrandID = randomObj.Next(1, carBrandId);
                liftingCapacity = randomObj.Next(100, 1000);
                bodyVolume = randomObj.Next(100, 1000);
                registrationNumber = "";
                for (int i = 0; i < 7; i++)
                {
                    int randomNumber = randomObj.Next(10);
                    registrationNumber += randomNumber;
                }
                db.Cars.Add(new Car { CarBrandId = carBrandID, LiftingCapacity = liftingCapacity, BodyVolume = bodyVolume, RegistrationNumber = registrationNumber });
            }
            db.SaveChanges();


            //Расстояния

            int distanceNumber = 20000;
            int departuresSettlementID, arrivalSettlementID, distance;

            int distanceId;


            for (distanceId = 1; distanceId <= distanceNumber; distanceId++)
            {
                departuresSettlementID = randomObj.Next(1, settlementId);
                arrivalSettlementID = randomObj.Next(1, settlementId);
                distance = randomObj.Next(100, 10000);
                db.Distances.Add(new Distance { DeparturesSettlementId = departuresSettlementID, ArrivalSettlementId = arrivalSettlementID, Distance1 = distance });
            }
            db.SaveChanges();


            //Перевозка

            int cargoNumber = 20000;
            int organizationID, distanceID, driverID, carID, loadID, transportationTariffID;
            DateTime date;
            int cargoId;
            int randomDays;

            for (cargoId = 1; cargoId <= cargoNumber; cargoId++)
            {
                organizationID = randomObj.Next(1, organizationId);
                distanceID = randomObj.Next(1, distanceId);
                driverID = randomObj.Next(1, driverId);
                carID = randomObj.Next(1, carId);
                loadID = randomObj.Next(1, loadId);
                transportationTariffID = randomObj.Next(1, tariffId);
                randomDays = randomObj.Next((int)(endDate - startDate).TotalDays);
                date = startDate.AddDays(randomDays);
                db.CargoTransportations.Add(new CargoTransportation { OrganizationId = organizationID, DistanceId = distanceID, DriverId = driverID, CarId = carID, LoadId = loadID, TransportationTariffId = transportationTariffID, Date = date });
            }
            db.SaveChanges();





        }
    }
}
