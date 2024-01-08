using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class BaseDatos
{
    public static BaseDatos instance;
    //Se pone la URI para la base de datos
    private string dbName = "URI=file:DataBase.db";
    
    //Metodo para crear la tabla en la base de datos
    public void CreateTable()
    {
        //Nos conectamos a la base de datos
        using (var connection = new SqliteConnection(dbName))
        {
            //Abrimos la conexion
            connection.Open();

            //Creamos los comandos para crear la tabla
            using (var command = connection.CreateCommand())
            {
                string sqlcreation="";
                sqlcreation += "CREATE TABLE IF NOT EXISTS user(";
                sqlcreation += "id INTEGER NOT NULL ";
                sqlcreation += "PRIMARY KEY AUTOINCREMENT,";
                sqlcreation += "points    VARCHAR(50) NOT NULL,";
                sqlcreation += "deaths VARCHAR(50) NOT NULL,";
                sqlcreation += "level VARCHAR(50) NOT NULL";
                sqlcreation += ");";
                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    //Metodo para añadir sentencias a la base datos
    public void Query(string q)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = q;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Se leen los datos
                        Debug.Log("id: " + reader["id"] + " points: " + reader["points"] + " level: " + reader["level"]);
                    }
                }
            }
            connection.Close();
        }
    }

    //Metodo para borrar los datos
    public void Delete()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using(var command = connection.CreateCommand())
            {
               string sqlcreation="";
               sqlcreation+="DELETE FROM user";
               command.CommandText = sqlcreation;
               command.ExecuteNonQuery();
            }
             connection.Close();
        }
    }
}
