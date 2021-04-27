using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAS.DB.Models;

namespace TMAS.BLL.Interfaces
{
    public interface IColumnsSortService
    {
        Task<Column> ReduceAfterDeleteAsync(int id);
        Task SwitchColumns(int prevPosition, Column column);

    }
}
