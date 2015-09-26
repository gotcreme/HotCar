namespace HotCar.Repositories.Sql
{
    public abstract class Repository
    {
        #region Protected fields

        protected readonly string connectionString;

        #endregion

        #region Constructor

        protected Repository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #endregion Constructor
    }
}
