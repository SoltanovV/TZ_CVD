namespace TZ_CVD.Model.Entity;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Employee> Employee { get; set; }

}
