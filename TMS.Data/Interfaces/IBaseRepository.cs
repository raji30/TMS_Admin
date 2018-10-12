using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data.Interfaces
{
    interface IBaseRepository<T>
    {
        Guid Add(T t);
        bool Update(T t);
        IEnumerable<T> GetAll();
        T GetbyField(object t);
        bool Delete(T t);
        T GetbyId(Guid id);
    }
}
