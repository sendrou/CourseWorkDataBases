using Lab02;
using System.Collections;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

using (Lab1Context db = new Lab1Context())
{
    while (true)
    {
        Console.WriteLine("МЕНЮ");
        Console.WriteLine("1. Вывод всех данных из таблицы Organizations");
        Console.WriteLine("2. Вывод данных из таблицы TransportationTariffs, где тариф на километры больше 90");
        Console.WriteLine("3. Вывод данных о количестве автомобилей каждой марки");
        Console.WriteLine("4. Вывод данных из таблиц Cars и CarBrands");
        Console.WriteLine("5. Вывод данных о перевозках, где оба тарифа больше 70.");
        Console.WriteLine("6. Вставка данных в таблицу Settlements");
        Console.WriteLine("7. Вставка данных в таблицу Cars");
        Console.WriteLine("8. Удаление данных из таблицы Settlements");
        Console.WriteLine("9. Удаление данных из таблицы Cars");
        Console.WriteLine("10. Обновить данные тарифов(увеличить на 10)");
        Console.WriteLine("0. ВЫХОД");
        int choiceMenu = Convert.ToInt32(Console.ReadLine());

        switch (choiceMenu)
        {
            case 1:
                PrintConsole(FirstTask(db));
                break;
            case 2:
                PrintConsole(SecondTask(db));
                break;
            case 3:
                PrintConsole(ThirdTask(db));
                break;
            case 4:
                PrintConsole(FourthTask(db));
                break;
            case 5:
                PrintConsole(FifthTask(db));
                break;
            case 6:
                SixthTask(db);
                break;
            case 7:
                SeventhTask(db);
                break;
            case 8:
                EightsTask(db);
                break;
            case 9:
                NinthTask(db);
                break;
            case 10:
                TenthTask(db);
                break;
            case 0:
                return;
        }

        Console.ReadKey();
    }
}

static void Print(IEnumerable items)
{
    Console.WriteLine("Результат: ");
    foreach (var item in items)
    {
        Console.WriteLine(item.ToString());
    }
    Console.WriteLine();
    Console.ReadKey();
}

static void PrintConsole(string text)
{
    Console.Write(text);
}

static string FirstTask(Lab1Context db)
{
    StringBuilder stringBuilder = new StringBuilder();

    stringBuilder.AppendLine("Organizations");

    var organizationsItems = db.Organizations.ToList();

    stringBuilder.AppendLine("Id, Name");

    foreach (var item in organizationsItems)
    {
        stringBuilder.AppendLine($"{item.OrganizationId}, {item.OrganizationName}");
    }

    return stringBuilder.ToString();
}

static string SecondTask(Lab1Context db)
{
    StringBuilder stringBuilder = new StringBuilder();

    stringBuilder.AppendLine("TransportationTariffs, TariffPerTKm>90");

    var tariffItems = db.TransportationTariffs.ToList().Where(t => t.TariffPerTKm > 90).OrderBy(t => t.TransportationTariffId);

    stringBuilder.AppendLine("Id, TariffPerTKm,TariffPerM3Km");

    foreach (var item in tariffItems)
    {
        stringBuilder.AppendLine($"{item.TransportationTariffId}, {item.TariffPerTKm},{item.TariffPerM3Km}");
    }

    return stringBuilder.ToString();
}

static string ThirdTask(Lab1Context db)
{
    StringBuilder stringBuilder = new StringBuilder();

    stringBuilder.AppendLine("Количество машин каждой марки.");

    var items = db.Cars.GroupBy(t => t.CarBrand).Select(g => new { Name = g.Key.BrandName, NumberOfCars = g.Count() });

    stringBuilder.AppendLine("Name, NumberOfCars");

    foreach (var item in items)
    {
        stringBuilder.AppendLine($"{item.Name} - {item.NumberOfCars}");
    }

    return stringBuilder.ToString();
}

static string FourthTask(Lab1Context db)
{

    StringBuilder stringBuilder = new StringBuilder();

    stringBuilder.AppendLine("Марка машины с регистрационным номером.");

    var items = db.Cars.Take(10).Select(it => new { CarBrand = it.CarBrand.BrandName, RegistrationNumber = it.RegistrationNumber });

    stringBuilder.AppendLine("CarBrand, RegistrationNumber");

    foreach (var item in items)
    {
        stringBuilder.AppendLine($"{item.CarBrand}, {item.RegistrationNumber}");
    }

    return stringBuilder.ToString();
}

static string FifthTask(Lab1Context db)
{
    StringBuilder stringBuilder = new StringBuilder();

    stringBuilder.AppendLine("Перевозки, где оба тарифа больше 70.");

    var items = db.CargoTransportations.Where(tran => tran.TransportationTariff.TariffPerM3Km > 70 && tran.TransportationTariff.TariffPerTKm > 70).Take(10).Select(item => new { DocumentId = item.DocumentId, TariffPerM3Km = item.TransportationTariff.TariffPerM3Km, TariffPerTKm = item.TransportationTariff.TariffPerTKm });

    stringBuilder.AppendLine("DocumentId, TariffPerM3Km, TariffPerTKm");

    foreach (var item in items)
    {
        stringBuilder.AppendLine($"{item.DocumentId}, {item.TariffPerM3Km}, {item.TariffPerTKm}");
    }

    return stringBuilder.ToString();

}

static void SixthTask(Lab1Context db)
{
    Console.WriteLine("Введите название населенного пункта.");
    string name = Console.ReadLine();

    Settlement newSettlement = new Settlement
    {
        SettlementName = name
    };

    db.Settlements.Add(newSettlement);
    db.SaveChanges();

    Console.WriteLine($"Новый населённый пункт {name} был успешно добавлен.");
}

static void SeventhTask(Lab1Context db)
{
    Console.WriteLine("Введите грузоподъёмность.");
    int liftingCapacity = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Введите объём кузова.");
    int bodyVolume = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Введите регистрационный номер.");
    string registrationNumber = Console.ReadLine();

    Console.WriteLine("Введите id марки машины.");
    int carBrandId = Convert.ToInt32(Console.ReadLine());

    Car newCar = new Car
    {
        CarBrandId = carBrandId,
        LiftingCapacity = liftingCapacity,
        BodyVolume = bodyVolume,
        RegistrationNumber = registrationNumber,
    };


    db.Cars.Add(newCar);
    db.SaveChanges();

    Console.WriteLine("Запись в таблицу Cars успешно добавлена.");
}

static void EightsTask(Lab1Context db)
{
    Console.WriteLine("Введите id населенного пункта для удаления.");
    int settlementId = Convert.ToInt32(Console.ReadLine());

    Settlement settlementToDelete = db.Settlements.FirstOrDefault(set => set.SettlementId == settlementId);

    if (settlementToDelete != null)
    {
        db.Settlements.Remove(settlementToDelete);
        db.SaveChanges();
    }

    Console.WriteLine($"Населённый пункт с индексом {settlementId} был успешно удален.");
}

static void NinthTask(Lab1Context db)
{
    Console.WriteLine("Введите id машины для удаления.");
    int carId = Convert.ToInt32(Console.ReadLine());

    Car carToDelete = db.Cars.FirstOrDefault(car => car.CarId == carId);

    if (carToDelete != null)
    {
        db.Cars.Remove(carToDelete);
        db.SaveChanges();
    }

    Console.WriteLine($"Машина с индексом {carId} была успешно удалёна.");
}

static void TenthTask(Lab1Context db)
{
    Console.WriteLine("Введите начальное значение id тарифа");
    int tariffStartId = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Введите конечное значение id тарифа");
    int tariffEndId = Convert.ToInt32(Console.ReadLine());

    var tariffsToUpdate = db.TransportationTariffs.Where(tar => tar.TransportationTariffId >= tariffStartId && tar.TransportationTariffId <= tariffEndId).ToList();

    foreach (var item in tariffsToUpdate)
    {
        item.TariffPerM3Km += 10;
        item.TariffPerTKm += 10;

    }

    db.SaveChanges();

    Console.WriteLine("Изменения были сохранены.");

}
