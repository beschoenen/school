package org.avans.VTSOa.deslimstemens.Models.Database;

import org.avans.VTSOa.deslimstemens.Enums.PlayerRole;

import java.util.ArrayList;
import java.util.List;

public class Account {
	private String name = null;
	private String password = null;
	private List<PlayerRole> roles = new ArrayList<>();

	public Account(String name, String password, List<PlayerRole> roles) {
		this.name = name;
		this.password = password;
		this.roles = roles;
	}

	public Account(String name, String password) {
		this.name = name;
		this.password = password;
	}

	public Account() {}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}

	public List<PlayerRole> getRoles() {
		return roles;
	}

	public void setRoles(List<PlayerRole> roles) {
		this.roles = roles;
	}
}
