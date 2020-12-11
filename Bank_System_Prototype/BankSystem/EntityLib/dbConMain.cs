using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
namespace EntityLib
{
    public class dbConMain
    {
        ClientsDBContext contextDb = new ClientsDBContext();
    }
}
//Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer

//Data Source=(localDB)\MSSQLLocalDB;Initial Catalog=ClientsDB;Integrated Security=True;Pooling=False
//ClientsDB
//(localDB)\MSSQLLocalDB
//Scaffold-DbContext "Server=(localDB)\MSSQLLocalDB;Database=ClientsDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer