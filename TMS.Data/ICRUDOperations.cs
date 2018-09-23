using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
    public interface ICRUDOperations<T>
    {
        T Add(T entity);
        T Update(T entity);
        T Remove(T entity);
        T FindById(long i);
        IEnumerable<T> GetAll();
        
    }
}
