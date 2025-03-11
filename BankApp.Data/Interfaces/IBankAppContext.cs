using Microsoft.Data.SqlClient;

namespace BankApp.Data.Interfaces
{
    public interface IBankAppContext
    {
        //metoder som ska användas för interaktion med databasen
        //i en contextklass läggs enbart statiska komponenter som inte utför någon operation i programmet, mer än att tillföra de statiska komponenter
        //som behövs för att ex kunna interagera med databasen

        public SqlConnection GetConnection();
    }
}
