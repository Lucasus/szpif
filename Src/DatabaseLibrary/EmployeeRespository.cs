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
            throw new NotImplementedException();
        }

        public void Update(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void Remove(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee GetById(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Employee GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public ICollection<Employee> GetByCategory(string category)
        {
            throw new NotImplementedException();
        }

    }
}
