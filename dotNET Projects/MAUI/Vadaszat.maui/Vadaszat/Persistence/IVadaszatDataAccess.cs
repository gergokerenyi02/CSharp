using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Vadaszat.Persistence
{
    public interface IVadaszatDataAccess
    {
        Task<GameTable> LoadAsync(String path);

        Task SaveAsync(String path, GameTable table);

    }
}
