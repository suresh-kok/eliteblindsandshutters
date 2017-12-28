using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteBlindsAPI.Business
{
    public abstract class IDataMapper<T>
    {
        /// <summary>
        /// A .Net database connection (SQL Server, MySql, Oracle, etc.... )
        /// </summary>
        public MySqlConnection Connection { get; set; }

        /// <summary>
        /// Reads configuration from the app.config and initializes the data mapper
        /// </summary>
        /// <param name="connection">A .net connection that implements IDbConnection</param>
        public IDataMapper()
        {
            string server;
            string database;
            string uid;
            string password;

            server = "localhost";
            database = "eliteblinds";
            uid = "root";
            password = "Epicor@1";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            Connection = new MySqlConnection(connectionString);

            if (Connection == null)
                throw new ArgumentNullException("A valid database connection is required");
            
        }

        /// <summary>
        /// Default select method for type T
        /// </summary>
        /// <param name="exError">Out exception object</param>
        /// <returns>List of type T</returns>
        public abstract List<T> Select(out Exception exError);

        /// <summary>
        /// Default create method for type T
        /// </summary>
        /// <param name="instance">The instance to create</param>
        /// <param name="exError">Out exception object</param>
        /// <returns>Boolean success/failure</returns>
        public abstract bool Create(T instance, out Exception exError);

        /// <summary>
        /// Default read method for type T 
        /// </summary>
        /// <param name="ID">ID of instance to read</param>
        /// <param name="exError">Out exception object</param>
        /// <returns>Instance of type T</returns>
        public abstract T Read(int ID, out Exception exError);

        /// <summary>
        /// Default update method for type T
        /// </summary>
        /// <param name="instance">Object of instance to update</param>
        /// <param name="exError">Out exception object</param>
        /// <returns>Instance of type T</returns>
        public abstract bool Update(T instance, out Exception exError);

        /// <summary>
        /// Default delete method for type T 
        /// </summary>
        /// <param name="ID">ID of instance to delete</param>
        /// <param name="exError">Out exception object</param>
        /// <returns>Boolean success/failure</returns>
        public abstract bool Delete(int ID, out Exception exError);
        
    }
}
