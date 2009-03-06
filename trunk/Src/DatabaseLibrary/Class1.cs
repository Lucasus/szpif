using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic;

namespace DatabaseLibrary
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);
        void Update(Employee employee);
        void Remove(Employee employee);
        Employee GetById(Guid productId);
        Employee GetByName(string name);
        ICollection<Employee> GetByCategory(string category);
    }
}
