using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public interface IRepository<T>
    {
        void Add(T employee);
        void Update(T employee);
        void Remove(T employee);
        T GetById(int productId);
        T GetByName(string name);
        ICollection<T> GetAll();
    }
    public interface IEmployeeRepository : IRepository<Employee> 
    {
        Employee GetByLogin(string login);
    }
}

