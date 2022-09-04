namespace TZ_CVD.Model.Entity;
/// <summary>
/// Департамент
/// </summary>
public class Department
{
    /// <summary>
    /// Id департамента
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название депортамента
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Навигационное свойство 
    /// </summary>
    public IEnumerable<Employee> Employee { get; set; }

}
