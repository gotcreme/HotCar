using System.Data.SqlClient;

namespace HotCar.Repositories.Abstract
{
    public interface IRepository<out T> where T: class
    {
        T SelectInformation(SqlDataReader reader);
    }
}
