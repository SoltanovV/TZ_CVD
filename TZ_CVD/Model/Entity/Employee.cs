namespace TZ_CVD.Model.Entity;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }    

    public int DepartmentId { get; set; }
    public Department Department { get; set; }

    public int? ChiefId { get; set; }
    public int Salary { get; set; }
}
