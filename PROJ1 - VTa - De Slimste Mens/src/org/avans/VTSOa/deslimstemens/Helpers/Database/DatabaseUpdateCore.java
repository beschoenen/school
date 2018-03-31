package org.avans.VTSOa.deslimstemens.Helpers.Database;

import org.avans.VTSOa.deslimstemens.Models.Database.Column;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

public class DatabaseUpdateCore {

	private String table;

	private List<Column> columns;

	private List<Where> wheres = new ArrayList<>();

	private class Where {
		public String first_column;
		public String operator;
		public String second_column;
		public String bool;

		public String raw;

		public Where(String first_column, String operator, String second_column, String bool) {
			this.first_column = first_column;
			this.operator = operator;
			this.second_column = second_column;
			this.bool = bool;
		}

		public Where(String raw) {
			this.raw = raw;
		}
	}

	public class WhereMaker {
		private final String bool;
		private final String first_column;
		private final DatabaseUpdateCore dbCore;

		public WhereMaker(String bool, String first_column, DatabaseUpdateCore dbCore) {
			this.bool = bool;
			this.first_column = first_column;
			this.dbCore = dbCore;
		}

		private DatabaseUpdateCore addWhere(String operator, String second_column) {
			this.dbCore.wheres.add(new Where(this.first_column, operator, second_column, this.bool));
			return this.dbCore;
		}

		public DatabaseUpdateCore equals(String second_column) {
			return this.addWhere("=", second_column);
		}

		public DatabaseUpdateCore greaterThan(String second_column) {
			return this.addWhere(">", second_column);
		}

		public DatabaseUpdateCore equalOrGreaterThan(String second_column) {
			return this.addWhere(">=", second_column);
		}

		public DatabaseUpdateCore smallerThan(String second_column) {
			return this.addWhere("<", second_column);
		}

		public DatabaseUpdateCore equalOrSmallerThan(String second_column) {
			return this.addWhere("<=", second_column);
		}

		public DatabaseUpdateCore isNot(String currentUser) {
			return this.addWhere("<>", currentUser);
		}
	}

	public DatabaseUpdateCore(String table) {
		this.table = table;
	}

	public DatabaseUpdateCore setColumns(List<Column> columns) {
		this.columns = columns;
		return this;
	}

	// Where statements
	public DatabaseUpdateCore where(String first_column, String operator, String second_column, String bool) {
		this.wheres.add(new Where(first_column, operator, second_column, bool));
		return this;
	}

	public WhereMaker where(String first_column) {
		return new WhereMaker("", first_column, this);
	}

	public WhereMaker andWhere(String first_column) {
		return new WhereMaker("and", first_column, this);
	}

	public WhereMaker orWhere(String first_column) {
		return new WhereMaker("or", first_column, this);
	}

	private ResultSet run(String query) throws SQLException {
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

	public String returnQuery() {
		return String.format("UPDATE %1$s %2$s %3$s", this.table, this.makeColumnStatement(), this.makeWhereStatement());
	}

	private String makeColumnStatement() {
		String statement = " SET ";
		for(Column column : this.columns) {
			statement += String.format("%1$s = %2$s, ", column.getColumn(), column.getValue());
		}
		return statement.trim().replaceAll(",$", "");
	}

	private String makeWhereStatement() {
		String statement = " WHERE ";
		int i = 0;
		for(Where where : this.wheres) {
			statement += String.format(((i > 0) ? " %4$s " : "") + "%1$s %2$s %3$s",
					where.first_column, where.operator, where.second_column, where.bool);
			i++;
		}
		return statement;
	}
}
