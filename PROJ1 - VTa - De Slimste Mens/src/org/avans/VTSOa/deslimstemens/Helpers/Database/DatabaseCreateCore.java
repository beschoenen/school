package org.avans.VTSOa.deslimstemens.Helpers.Database;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class DatabaseCreateCore {

	private String table;
	private List<String> columns;
	private List<List<String>> values = new ArrayList<>();

	public DatabaseCreateCore(String table) {
		this.table = table;
	}

	public DatabaseCreateCore setColumns(String... columns) {
		this.columns = Arrays.asList(columns);
		return this;
	}

	public DatabaseCreateCore addValues(String... values) {
		this.values.add(Arrays.asList(values));
		return this;
	}

	public String returnQuery() {
		return String.format("INSERT INTO %1$s (%2$s) VALUES %3$s", this.table, String.join(", ", this.columns), createValueString());
	}

	private String createValueString() {
		String statement = "";
		for(List<String> record : this.values) {
			statement += String.format("(%s), ", String.join(", ", record));
		}
		return statement.trim().replaceAll(",$", "");
	}

	public ResultSet run(String query) throws SQLException {
		return this.execute(query);
	}

	public ResultSet run() throws SQLException {
		return this.run(this.returnQuery());
	}

	private ResultSet execute(String statement) throws SQLException {
		Connection connection = DatabaseConnector.getConnection();
		Statement query = connection.createStatement();
		query.executeUpdate(statement, Statement.RETURN_GENERATED_KEYS);
		return query.getGeneratedKeys();
	}
}
