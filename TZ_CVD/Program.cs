using Microsoft.EntityFrameworkCore;
using TZ_CVD.Model.Entity;
using TZ_CVD.Model;

using (ApplicationContext db = new ApplicationContext())
{
    // Полчучение списка всех сотрудников
    var empo = db.Employees.Include(e => e.Department).ToList();

    // Получение списка Id Руководителей
    var chiefId = db.Employees
        .Where(e => e.ChiefId != null)
        .Select(e => e.ChiefId)
        .Distinct()
        .ToList();

    List<Employee> chief = new List<Employee>();
    List<Employee> employee = new List<Employee>();

    viewEmployeeList(empo, chiefId, out chief, out employee);

    var str = new string('-', 20);

    Console.WriteLine(str);
    Console.WriteLine("Суммарную зарплату в разрезе департаментов (без руководителей и с руководителями)\n");
    Console.WriteLine("Без руководителей");

    // Группировка и выборка по депараменту без руководителей
    var salaryEmp = employee
        .GroupBy(e => e.Department)
        .Select(d => new
        {
            Name = d.Key.Name,
            Salary = d
            .Select(e => e.Salary),
        });

    foreach (var item in salaryEmp)
    {
        Console.WriteLine($" {item.Name} | {item.Salary.Sum()}  |");
    }

    // Группировка и выборка по депараменту с руководителями
    var salaryChief = empo
        .GroupBy(e => e.Department)
        .Select(d => new
        {
            Name = d.Key.Name,
            Salary = d
            .Select(e => e.Salary),
        });

    Console.WriteLine("\nC руководителями");
    foreach (var item in salaryChief)
    {
        Console.WriteLine($" {item.Name} | {item.Salary.Sum()}  |");
    }


    Console.WriteLine(str);
    Console.WriteLine("Департамент, в котором у сотрудника зарплата максимальна");

    // Получение максимальной ЗП
    var max = empo.Max(e => e.Salary);

    // Выборка по максимальной Зп
    var resultMax = db.Employees
        .Where(e => e.Salary == max)
        .Select(e => new 
    {
        Dep = e.Department.Name,
        Name = e.Name
    });

    foreach (var item in resultMax)
    {
        Console.WriteLine($"{item.Dep} {item.Name}");
    }

    Console.WriteLine(str);
    Console.WriteLine("Зарплаты руководителей департаментов (по убыванию)");
    //var result = viewListEmpl(empo, сhief);
    chief.Reverse();

    foreach (var item in chief)
    {
        Console.WriteLine($"{item.Name} - {item.Salary}");
    }

    // Функция для получения руководителей и подчиненых
    void viewEmployeeList(List<Employee> oneList, List<int?> twoList, out List<Employee> outOneList, out List<Employee> outTwoList)
    {
        List<Employee> returnChiefList = new List<Employee>();
        List<Employee> returnEmplList = new List<Employee>();
        int count = 0;
        for (int i = 0; i < oneList.Count; i++)
        {
            for (int j = 0; j < twoList.Count; j++)
            {
                if (oneList[i].Id != twoList[j])
                {
                    count++;

                    if (count == twoList.Count)
                    {
                        returnChiefList.Add(oneList[i]);
                    }
                }
                else if (oneList[i].Id == twoList[j])
                {
                    returnEmplList.Add(oneList[i]);
                }
            }
            count = 0;
        }

        outOneList = returnEmplList;
        outTwoList = returnChiefList;
    }
}

