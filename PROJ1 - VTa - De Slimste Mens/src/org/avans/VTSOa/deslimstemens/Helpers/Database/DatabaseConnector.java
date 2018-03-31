package org.avans.VTSOa.deslimstemens.Helpers.Database;

import org.avans.VTSOa.deslimstemens.Config.database;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class DatabaseConnector {

    private static Connection connection = null;

    public static void connect() throws SQLException {
        try {
            Class.forName("com.mysql.jdbc.Driver");
            connection = DriverManager.getConnection(String.format("jdbc:mysql://%1$s:%2$s/%3$s?user=%4$s&password=%5$s",
                    database.host, database.port, database.database, database.username, database.password));

        } catch (ClassNotFoundException e) {
            System.out.println("JDBC Driver not found");
            System.exit(1);
        }
    }

    public static Connection getConnection() throws SQLException {
        if(connection == null) connect();
        return connection;
    }

}
