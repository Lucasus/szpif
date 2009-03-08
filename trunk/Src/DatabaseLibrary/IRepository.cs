using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseLibrary
{
    public interface IRepository<T>
    {
        void Add(T employee);
        void Update(T employee);
        void Remove(T employee);
        T GetById(Guid productId);
        T GetByName(string name);
        ICollection<T> GetByCategory(string category);
    }
    public interface IEmployeeRepository : IRepository<Employee> 
    {
        Employee GetByLogin(string login);
    }
}

