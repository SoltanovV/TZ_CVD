namespace TZ_CVD.Model.Entity;
/// <summary>
/// Сотрудник
/// </summary>
public class Employee
{
    /// <summary>
    /// Id сотрудника
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Имя сотрудника
    /// </summary>
    public string Name { get; set; }    
    
    /// <summary>
    /// Id руководителя
    /// </summary>
    public int? ChiefId { get; set; }

    /// <summary>
    /// Размер заработной платы
    /// </summary>
    public int Salary { get; set; }

    /// <summary>
    /// Первичный ключ
    /// </summary>
    public int DepartmentId { get; set; }

    /// <summary>
    /// Навигационное свойство 
    /// </summary>
    public Department Department { get; set; }
}
