using Microsoft.EntityFrameworkCore;
using TZ_CVD.Model.Entity;

namespace TZ_CVD.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;

        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Department;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Заполнение данными Department
            var departament1 = new Department()
            {
                Id = 1,
                Name = "D1"

            };

            var departament2 = new Department()
            {
                Id = 2,
                Name = "D2",
            };

            var departament3 = new Department()
            {
                Id = 3,
                Name = "D3",
            };

            var departaments = new List<Department>() { departament1, departament2, departament3 };
            #endregion

            #region заполнение данными Employee
            var employees1 = new Employee()
            {
                Id = 1,
                DepartmentId = 1,
                ChiefId = 5,
                Name = "John",
                Salary = 100
            };

            var employees2 = new Employee()
            {
                Id = 2,
                DepartmentId = 1,
                ChiefId = 5,
                Name = "Misha",
                Salary = 600
            };

            var employees3 = new Employee()
            {
                Id = 3,
                DepartmentId = 2,
                ChiefId = 6,
                Name = "Eugen",
                Salary = 300
            };
            var employees4 = new Employee()
            {
                Id = 4,
                DepartmentId = 2,
                ChiefId = 6,
                Name = "Tolya",
                Salary = 400
            };
            var employees5 = new Employee()
            {
                Id = 5,
                DepartmentId = 3,
                ChiefId = 7,
                Name = "Stepan",
                Salary = 500
            };
            var employees6 = new Employee()
            {
                Id = 6,
                DepartmentId = 3,
                ChiefId = 7,
                Name = "Alex",
                Salary = 1000
            };
            var employees7 = new Employee()
            {
                Id = 7,
                DepartmentId = 3,
                ChiefId = null,
                Name = "Ivan",
                Salary = 1100
            };
            var employees = new List<Employee>() { employees1, employees2, employees3, employees4, employees5, employees6, employees7 };
            #endregion

            modelBuilder.Entity<Department>().HasData(departaments);
            modelBuilder.Entity<Employee>().HasData(employees);

            // Связь один ко многим 
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employee);
        }
    }
}
