using System.Collections.Generic;
using System.Threading.Tasks;

namespace product_accounting_frontend.dao
{
    public interface IGenericDAO<T>
    {
        Task<List<T>> executeGetQuery();
        Task<bool> executePostQuery(T Object);
        Task<bool> executePutQuery(int id, T Object);
        Task<bool> executeDeleteQuery(int id);       
    }
}
