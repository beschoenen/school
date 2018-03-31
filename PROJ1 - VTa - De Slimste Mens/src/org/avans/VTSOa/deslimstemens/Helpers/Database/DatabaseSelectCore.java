package org.avans.VTSOa.deslimstemens.Helpers.Database;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.Collections;

public class DatabaseSelectCore {

    public String table;

    private ArrayList<String> columns = new ArrayList<>();

    private ArrayList<Where> wheres = new ArrayList<>();

    private ArrayList<Join> joins = new ArrayList<>();

    private ArrayList<OrderBy> orderBy = new ArrayList<>();

    private Having having;

    private String groupBy;

    private int limit = 0;

    public int perPage = 15;

    private int page = 0;

    // Classes
    private class Join {
        public String type;
        public String table;
        public String column;
        public String operator;
        public String second_column;
        private String raw = null;

        public Join(String type, String table, String column, String operator, String second_column) {
            this.type = type;
            this.table = table;
            this.column = column;
            this.operator = operator;
            this.second_column = second_column;
        }

        public Join(String type, String table, String raw) {
            this.type = type;
            this.table = table;
            this.raw = raw;
        }
    }

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

    private class OrderBy {
        public String column;
        public String type;

        public OrderBy(String column, String type) {
            this.column = column;
            this.type = type;
        }
    }

    private class Having {
        public String column;
        public String operator;
        public String value;

        public Having(String column, String operator, String value) {
            this.column = column;
            this.operator = operator;
            this.value = value;
        }
    }

    public class JoinMaker {
        private String type;
        private String table;
        private DatabaseSelectCore dbCore;

        public JoinMaker(String type, String table, DatabaseSelectCore dbCore) {
            this.type = type;
            this.table = table;
            this.dbCore = dbCore;
        }

        private DatabaseSelectCore addJoin(String first_column, String operator, String second_column) {
            dbCore.joins.add(new Join(this.type, this.table, first_column, operator, second_column));
            return this.dbCore;
        }

        public DatabaseSelectCore on(String first_column, String operator, String second_column) {
            return this.addJoin(first_column, operator, second_column);
        }

        public DatabaseSelectCore onEquals(String first_column, String second_column) {
            return this.addJoin(first_column, "=", second_column);
        }

        public DatabaseSelectCore onGreaterThan(String first_column, String second_column) {
            return this.addJoin(first_column, ">", second_column);
        }

        public DatabaseSelectCore onEqualOrGreaterThan(String first_column, String second_column) {
            return this.addJoin(first_column, ">=", second_column);
        }

        public DatabaseSelectCore onSmallerThan(String first_column, String second_column) {
            return this.addJoin(first_column, "<", second_column);
        }

        public DatabaseSelectCore onEqualOrSmallerThan(String first_column, String second_column) {
            return this.addJoin(first_column, "<=", second_column);
        }

        public DatabaseSelectCore onRaw(String raw) {
            dbCore.joins.add(new Join(this.type, this.table, raw));
            return this.dbCore;
        }
    }

    public class WhereMaker {
        private final String bool;
        private final String first_column;
        private final DatabaseSelectCore dbCore;

        public WhereMaker(String bool, String first_column, DatabaseSelectCore dbCore) {
            this.bool = bool;
            this.first_column = first_column;
            this.dbCore = dbCore;
        }

        private DatabaseSelectCore addWhere(String operator, String second_column) {
            this.dbCore.wheres.add(new Where(this.first_column, operator, second_column, this.bool));
            return this.dbCore;
        }

        public DatabaseSelectCore equals(String second_column) {
            return this.addWhere("=", second_column);
        }

        public DatabaseSelectCore greaterThan(String second_column) {
            return this.addWhere(">", second_column);
        }

        public DatabaseSelectCore equalOrGreaterThan(String second_column) {
            return this.addWhere(">=", second_column);
        }

        public DatabaseSelectCore smallerThan(String second_column) {
            return this.addWhere("<", second_column);
        }

        public DatabaseSelectCore equalOrSmallerThan(String second_column) {
            return this.addWhere("<=", second_column);
        }

        public DatabaseSelectCore isNot(String currentUser) {
            return this.addWhere("<>", currentUser);
        }
    }

    public DatabaseSelectCore(String table) {
        this.table = table;
    }

    // Methods
    public DatabaseSelectCore select(String... columns) {
        Collections.addAll(this.columns, columns);
        return this;
    }

    public DatabaseSelectCore selectAll() {
        return this.select("*");
    }


    private JoinMaker join(String type, String table) {
        return new JoinMaker(type, table, this);
    }

    public JoinMaker join(String table) {
        return this.join("join", table);
    }

    public JoinMaker leftJoin(String table) {
        return this.join("left join", table);
    }

    public JoinMaker rightJoin(String table) {
        return this.join("right join", table);
    }

    public JoinMaker fullJoin(String table) {
        return this.join("full join", table);
    }


    // Where statements
    public DatabaseSelectCore where(String first_column, String operator, String second_column, String bool) {
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

    public DatabaseSelectCore whereRaw(String rawWhere) {
        this.wheres.add(new Where(rawWhere));
        return this;
    }


    public DatabaseSelectCore having(String first_column, String operator, String value) {
        this.having = new Having(first_column, operator, value);
        return this;
    }


    public DatabaseSelectCore orderBy(String column, String type) {
        this.orderBy.add(new OrderBy(column, type));
        return this;
    }

    public DatabaseSelectCore orderBy(String column) {
        return this.orderBy(column, "DESC");
    }


    public DatabaseSelectCore page(int page) {
        this.page = page;
        return this;
    }


    public DatabaseSelectCore limit(int limit) {
        this.limit = limit;
        return this;
    }

    public ResultSet get(String query) throws SQLException {
        return this.execute(query);
    }

    public ResultSet get() throws SQLException {
        return this.get(this.returnQuery());
    }

	public ResultSet first() throws SQLException {
		this.limit(1);
		return this.get();
	}


    private ResultSet execute(String statement) throws SQLException {
        Connection connection = DatabaseConnector.getConnection();
        Statement query = connection.createStatement();
        return query.executeQuery(statement);
    }

    public String returnQuery() {
        String statement = String.format("%1$S %2$s FROM %3$s", "select", String.join( ", ", this.columns), this.table);
        if(this.joins.size() > 0) {
            statement += String.format(" %s", this.makeJoinStatement());
        }
        if(this.wheres.size() > 0) {
            statement += this.makeWhereStatement();
        }
        if(this.groupBy != null) {
            statement += String.format(" GROUP BY %s", this.groupBy);
        }
        if(this.having != null) {
            statement += String.format(" HAVING %1$s %2$s %3$s", this.having.column, this.having.operator, this.having.value);
        }
        if(this.orderBy.size() > 0) {
            statement += this.makeOrderByStatement();
        }
        if(this.limit > 0) {
            statement += String.format(" LIMIT %d", this.limit);
        } else if(this.page > 0) {
            statement += String.format(" LIMIT %2$d, %1$d", this.perPage, this.perPage * this.page);
        }
        return statement;
    }

    private String makeWhereStatement() {
        String statement = " WHERE ";
        int i = 0;
        for(Where where : this.wheres) {
            statement += String.format(((i > 0) ? " %4$S " : "") + "%1$s %2$s %3$s",
                    where.first_column, where.operator, where.second_column, where.bool);
            i++;
        }
        return statement;
    }

    private String makeJoinStatement() {
        String statement = "";
        for(Join join : this.joins) {
            if(join.raw == null) {
                String first_table = join.column.contains(".") ? join.column.split("\\.")[0] : join.table;
                String first_column = join.column.contains(".") ? join.column.split("\\.")[1] : join.column;
                String second_table = join.second_column.contains(".") ? join.second_column.split("\\.")[0] : this.table;
                String second_column = join.second_column.contains(".") ? join.second_column.split("\\.")[1] : join.second_column;

                statement += String.format("%1$S %2$s ON %3$s.%4$s %5$s %6$s.%7$s ",
                        join.type, join.table, first_table, first_column, join.operator, second_table, second_column);
            } else {
                statement += String.format("%1$S %2$s ON %3$s ", join.type, join.table, join.raw);
            }
        }
        return statement.trim();
    }

    private String makeOrderByStatement() {
        ArrayList<String> orderBys = new ArrayList<>();
        for(OrderBy orderBy : this.orderBy) {
            String column = (!orderBy.column.contains(".") ? this.table + "." : "") + orderBy.column;
            orderBys.add(String.format("%1$s %2$S", column, orderBy.type));
        }
        return " ORDER BY " + String.join(", ", orderBys).trim().replaceAll(",$", "");
    }
}
