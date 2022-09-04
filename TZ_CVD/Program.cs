using Microsoft.EntityFrameworkCore;
using System.Linq;
using TZ_CVD.Model;
using TZ_CVD.Model.Entity;

using (ApplicationContext db = new ApplicationContext())
{
    // Полчучение списка всех сотрудников
    var empo = db.Employees.Include(e => e.Department).ToList();

    var s = new string('-', 20);

    // Получение списка Id Руководителей
    var empChiefId = db.Employees.Where(e => e.ChiefId != null).Select(e => e.ChiefId).Distinct().ToList();

    // Получение руководителей
    var сhief = viewIdChiefList(empo, empChiefId);

    // Получение сотрудников
    var employee = viewIdEmployeeList(empo, empChiefId);

    Console.WriteLine(s);
    Console.WriteLine("Суммарную зарплату в разрезе департаментов (без руководителей и с руководителями)");
    Console.WriteLine("Без руководителей");

    // Группировка по депараменту и выборка
    var salaryEmp = employee
        .GroupBy(e => e.Department)
        .Select(d => new
        {
            Name = d.Key.Name,
            Salary = d.Select(e => e.Salary),
        });

    foreach (var item in salaryEmp)
    {
        Console.WriteLine($" {item.Name} | {item.Salary.Sum()}  |");
    }

    // Группировка по депараменту и выборка
    var salaryChief = empo
        .GroupBy(e => e.Department)
        .Select(d => new
        {
            Name = d.Key.Name,
            Salary = d.Select(e => e.Salary),
        });

    Console.WriteLine("\nC руководителями");
    foreach (var item in salaryChief)
    {
        Console.WriteLine($" {item.Name} | {item.Salary.Sum()}  |");
    }


    Console.WriteLine(s);
    Console.WriteLine("Департамент, в котором у сотрудника зарплата максимальна");

    // Получение максимальной ЗП
    var max = employee.Max(e => e.Salary);

    // Выборка по максимальной Зп
    var resultMax = db.Employees.Where(e => e.Salary == max).Select(e => e.Department.Name);
    foreach (var item in resultMax)
    {
        Console.WriteLine(item);
    }

    Console.WriteLine(s);
    Console.WriteLine("Зарплаты руководителей департаментов (по убыванию)");
    var res = viewIdList(empo, сhief);
    res.Reverse();

    foreach (var item in res)
    {
        Console.WriteLine($"{item.Name} - {item.Salary}");
    }

    // Вспомогательные функции 
    // Функция для нахождения id руководителей
    List<Employee> viewIdChiefList (List<Employee> oneList, List<int?> twoList)
    {
        List<Employee> retirnIdList = new List<Employee>();
        for (int i = 0; i < oneList.Count; i++)
        {
            for (int j = 0; j < twoList.Count; j++)
            {
                if (oneList[i].Id == twoList[j])
                {
                    retirnIdList.Add(oneList[i]);
                }
            }
        }
        return retirnIdList;
    }

    // Функция для нахождения id сотрудников
    List<Employee> viewIdEmployeeList(List<Employee> oneList, List<int?> twoList)
    {
        List<Employee> retirnIdList = new List<Employee>();
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
                        retirnIdList.Add(oneList[i]);
                    }
                }
            }
            count = 0;
        }
        return retirnIdList;
    }

    List<Employee> viewIdList(List<Employee> oneList, List<Employee> twoList)
    {
        List<Employee> retirnIdList = new List<Employee>();
        for (int i = 0; i < oneList.Count; i++)
        {
            for (int j = 0; j < twoList.Count; j++)
            {
                if (oneList[i].Id == twoList[j].Id)
                {
                    retirnIdList.Add(oneList[i]);
                }
            }
        }
        return retirnIdList;
    }
}

