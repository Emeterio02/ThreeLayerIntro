using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public int maxSalaries = 1000000;
        public int maxIndvidualSalary = 100000;
        public int maxEngineers = 5;
        public int maxManagers = 5;
        public int maxHR = 1;


        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        //Implement the method from the interface, and here I can add logic to whichever methods I want 
        //to add logic to
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
           
            return await _employeeRepository.GetEmployeesAsync();
        }

        //This employee being passed in is coming from my controller
        //...and I am writing logic on this employee before passing it to my DAL
        //..where the changes are being executed on my DbSet which goes to the Database
        public async Task AddEmployeeAsync(Employee employee)
        {
            var employees = await _employeeRepository.GetEmployeesAsync();

            int currentBudget = employees.Sum(u => u.Salary);
            int numOfEngineers = employees.Where(u => u.Position == "Engineer").Count();
            int numOfManagers = employees.Where(u => u.Position == "Manager").Count();
            int numOfHR = employees.Where(u => u.Position == "HR").Count();

            //Writing logic
            if (employee.Salary <= maxIndvidualSalary 
                && employee.Salary + currentBudget <= maxSalaries)
            {
                if (employee.Position == "Enineer" && numOfEngineers + 1 <= maxEngineers)
                {
                    await _employeeRepository.AddEmployeeAsync(employee);
                }
                if (employee.Position == "Manager" && numOfManagers + 1 <= maxManagers)
                {
                    await _employeeRepository.AddEmployeeAsync(employee);
                }
                if (employee.Position == "HR" && numOfHR + 1 <= maxHR)
                {
                    await _employeeRepository.AddEmployeeAsync(employee);
                }
                await _employeeRepository.AddEmployeeAsync(employee);
            }
            else
            {

            }
        }

        //This employee being passed in is coming from my controller
        //...and I am writing logic on this employee before passing it to my DAL
        //..where the changes are being executed on my DbSet which goes to the Database
        //going to the DAL, and getting the employee from the employees DbSet
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(id);
        }

        //This employee being passed in is coming from my controller
        //...and I am writing logic on this employee before passing it to my DAL
        //..where the changes are being executed on my DbSet which goes to the Database
        public async Task GetDetailsAsync(Employee employee)
        {
            await _employeeRepository.GetDetailsAsync(employee);
        }

        //This employee being passed in is coming from my controller
        //...and I am writing logic on this employee before passing it to my DAL
        //..where the changes are being executed on my DbSet which goes to the Database
        public async Task UpdateEmployeeAsync(Employee employee)
        {
                await _employeeRepository.UpdateEmployeeAsync(employee);
        }

    }
}
