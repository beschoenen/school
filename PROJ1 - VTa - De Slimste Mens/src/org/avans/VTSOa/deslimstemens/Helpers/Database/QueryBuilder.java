package org.avans.VTSOa.deslimstemens.Helpers.Database;

public class QueryBuilder {

    private String table;

    public QueryBuilder(String table) {
        this.table = table;
    }

    public DatabaseSelectCore selectAll() {
        return new DatabaseSelectCore(this.table).selectAll();
    }

    public DatabaseSelectCore select(String... columns) {
        return new DatabaseSelectCore(this.table).select(columns);
    }

    public DatabaseCreateCore create() {
        return new DatabaseCreateCore(this.table);
    }

    public DatabaseUpdateCore update() {
        return new DatabaseUpdateCore(this.table);
    }
}
