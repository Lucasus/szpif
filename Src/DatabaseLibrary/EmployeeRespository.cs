using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic;

namespace DatabaseLibrary
{
    /// <summary>
    /// Klasa odpowiedzialna za udostępnianie kolekcji pracowników
    /// zawartej w bazie danych.
    /// </summary>
    public class EmployeeRespository : IEmployeeRepository
    {
        /// <summary>
        /// Adds the specified employee to repository.
        /// </summary>
        /// <param name="employee">The employee.</param>
        public void Add(Employee employee)
        {
        }

        /// <summary>
        /// Updates the specified employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        public void Update(Employee employee)
        {
        }

        /// <summary>
        /// Removes the specified employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        public void Remove(Employee employee)
        {
        }

        public Employee GetById(Guid productId)
        {
            return null;
        }

        public Employee GetByName(string name)
        {
            return null;
        }

        public ICollection<Employee> GetByCategory(string category)
        {
            return null;
        }

    }
}
