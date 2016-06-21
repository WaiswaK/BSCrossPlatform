using SQLite.Net;
using System.Threading.Tasks;

namespace BSCrossPlatform.Interfaces
{
    public interface IDatabase
    {
        Task InitializeDatabase();
        SQLiteConnection GetConnection();
    }
}
