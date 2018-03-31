package org.avans.VTSOa.deslimstemens.Repositories;

import org.avans.VTSOa.deslimstemens.Enums.GameStatus;
import org.avans.VTSOa.deslimstemens.Enums.PlayerRole;
import org.avans.VTSOa.deslimstemens.Helpers.Database.DatabaseCreateCore;
import org.avans.VTSOa.deslimstemens.Helpers.Database.QueryBuilder;
import org.avans.VTSOa.deslimstemens.Helpers.Helper;
import org.avans.VTSOa.deslimstemens.Models.Database.Account;
import org.avans.VTSOa.deslimstemens.Models.Database.Column;
import org.avans.VTSOa.deslimstemens.Models.Database.PlayerGameStatus;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class AccountRepository {

	public static Account find(String name) throws SQLException {
		Account account = new Account();
		List<PlayerRole> playerRoles = new ArrayList<>();
		Integer counter = 0;

		ResultSet accountResult = new QueryBuilder("account")
				.select("account.naam", "account.wachtwoord", "accountrol.rol_type")
				.join("accountrol").onEquals("account_naam", "naam")
				.where("naam").equals(Helper.stringWithQuotes(name))
				.get();

		if(!accountResult.next()) {
			return null;
		} else accountResult.previous();

		while(accountResult.next()) {
			if(counter == 0) {
				account.setName(accountResult.getString("naam"));
				account.setPassword(accountResult.getString("wachtwoord"));
			}
			playerRoles.add(PlayerRole.fromString(accountResult.getString("rol_type")));
			counter++;
		}
		account.setRoles(playerRoles);
		return account;
	}

	public static List<PlayerGameStatus> listUsersWithGameStatus(String currentUser) throws SQLException {
		List<PlayerGameStatus> players = new ArrayList<>();
		ResultSet playersList = new QueryBuilder("account")
				.select("account.naam",
						"spel.spel_id AS id",
						String.format("IF(spel.toestand_type = \"uitgedaagd\", IF(\"%1$s\" = spel.speler1, \"uitgedaagd\", \"uitdaging_ontvangen\"), IF(spel.toestand_type = \"bezig\", \"bezig\", \"geen_spel\")) AS type", currentUser)
				)
				.leftJoin("spel").onRaw(String.format("(account.naam = spel.speler1 OR account.naam = spel.speler2) AND (%1$s = spel.speler1 OR %1$s = spel.speler2) AND (spel.toestand_type = \"bezig\" OR spel.toestand_type = \"uitgedaagd\")", Helper.stringWithQuotes(currentUser)))
				.where("account.naam")
				.isNot(Helper.stringWithQuotes(currentUser))
				.get();

		while(playersList.next()) {
			GameStatus gameStatus = GameStatus.fromString(playersList.getString("type"));
			players.add(new PlayerGameStatus(playersList.getString("naam"), playersList.getInt("id"), gameStatus));
		}
		return players;
	}

	public static void create(Account account) throws SQLException {
		new QueryBuilder("account").create()
				.setColumns("naam", "wachtwoord")
				.addValues(Helper.stringWithQuotes(account.getName()), Helper.stringWithQuotes(account.getPassword()))
				.run();

		DatabaseCreateCore userRoles = new QueryBuilder("accountrol").create().setColumns("account_naam", "rol_type");

		for(PlayerRole role : account.getRoles()) {
			userRoles.addValues(
					Helper.stringWithQuotes(account.getName()),
					Helper.stringWithQuotes(role.value)
			);
		}
		userRoles.run();
	}

	public static void update(Account account) throws SQLException{
		new QueryBuilder("account")
				.update()
				.setColumns(new ArrayList<Column>(){{
					add(new Column("wachtwoord", account.getPassword()));
				}})
				.where("naam").equals(Helper.stringWithQuotes(account.getName()))
				.run();
	}
}
