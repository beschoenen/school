package org.avans.VTSOa.deslimstemens.Repositories;

import org.avans.VTSOa.deslimstemens.Enums.GameRound;
import org.avans.VTSOa.deslimstemens.Enums.GameStatus;
import org.avans.VTSOa.deslimstemens.Enums.TurnStatus;
import org.avans.VTSOa.deslimstemens.Helpers.Database.DatabaseSelectCore;
import org.avans.VTSOa.deslimstemens.Helpers.Database.QueryBuilder;
import org.avans.VTSOa.deslimstemens.Helpers.Helper;
import org.avans.VTSOa.deslimstemens.Models.Database.Column;
import org.avans.VTSOa.deslimstemens.Models.Database.Game;
import org.avans.VTSOa.deslimstemens.Models.Database.GameInfo;
import org.avans.VTSOa.deslimstemens.Models.Database.GameRanking;
import org.avans.VTSOa.deslimstemens.Models.Database.Turn.Finale;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class GameRepository {

	public static List<GameInfo> findByUser(String currentUser) throws SQLException {
		return gameInfo(true, currentUser, 1);
	}

	public static GameInfo findById(int gameId, String currentUser) throws SQLException {
		return gameInfo(false, currentUser, gameId).get(0);
	}

	private static List<GameInfo> gameInfo(boolean byUsername, String currentUser, int gameId) throws SQLException {
		List<GameInfo> games = new ArrayList<>();

		String query = String.format(
				"SELECT\n" +
					"spel.spel_id,\n" +
					"IF(spel.speler1 = %1$s, spel.speler2, spel.speler1) AS tegenstander,\n" +
					"IF(spel.toestand_type = \"uitgedaagd\", IF(IF(spel.speler1 = %1$s, spel.speler2, spel.speler1) = spel.speler2, \"uitgedaagd\", \"uitdaging_ontvangen\"), spel.toestand_type) AS toestand_type,\n" +
					"rondenaam.type AS ronde,\n" +
					"IF(beurt.beurten IS NULL, 0, beurt.beurten) AS beurten,\n" +
					"beurt.huidige_speler\n" +
				"FROM spel\n" +
				"LEFT JOIN (\n" +
					"SELECT\n" +
						"ronde.spel_id,\n" +
						"MAX(rondenaam.volgnr) AS ronde\n" +
					"FROM ronde\n" +
					"LEFT JOIN rondenaam\n" +
						"ON rondenaam.type = ronde.rondenaam\n" +
					"GROUP BY ronde.spel_id\n" +
					"ORDER BY ronde.spel_id\n" +
				") AS ronde\n" +
					"ON ronde.spel_id = spel.spel_id\n" +
				"LEFT JOIN rondenaam\n" +
					"ON rondenaam.volgnr = ronde.ronde\n" +
				"LEFT JOIN (\n" +
					"SELECT\n" +
						"beurt.spel_id,\n" +
						"beurt.rondenaam,\n" +
						"b.beurten,\n" +
						"IF(beurt.beurtstatus IN (\"bezig\"),\n" +
							"beurt.speler,\n" +
							"IF(beurt.speler = spel.speler1, speler2, speler1)\n" +
						") AS huidige_speler\n" +
					"FROM beurt\n" +
					"JOIN (\n" +
						"SELECT\n" +
							"spel_id,\n" +
							"rondenaam,\n" +
							"COUNT(beurt.beurt_id) AS beurten\n" +
						"FROM beurt\n" +
						"GROUP BY beurt.spel_id, beurt.rondenaam\n" +
					") AS b\n" +
						"ON b.spel_id = beurt.spel_id AND b.rondenaam = beurt.rondenaam AND b.beurten = beurt.beurt_id\n" +
					"JOIN spel\n" +
						"ON spel.spel_id = beurt.spel_id\n" +
					"GROUP BY beurt.spel_id, beurt.rondenaam\n" +
				") AS beurt\n" +
					"ON beurt.spel_id = spel.spel_id AND beurt.rondenaam = rondenaam.type\n"
				, Helper.stringWithQuotes(currentUser));
		if(byUsername) {
			query += String.format(
					"WHERE spel.speler1 = %1$s OR spel.speler2 = %1$s",
					Helper.stringWithQuotes(currentUser)
			);
		} else {
			query += String.format("WHERE spel.spel_id = %1$d", gameId);
		}
		ResultSet gameResults = new DatabaseSelectCore("spel").get(query);

		while(gameResults.next()) {
			GameInfo newGame = new GameInfo(
					gameResults.getInt("spel_id"),
					gameResults.getString("tegenstander"),
					GameStatus.fromString(gameResults.getString("toestand_type")),
					GameRound.fromString(gameResults.getString("ronde")),
					gameResults.getInt("beurten"),
					gameResults.getString("huidige_speler")
			);

			if(newGame.getGameRound() != null) {
				if(newGame.getCurrentPlayer() == null) {
					GameRanking gameRanking = RankingRepository.getGameRanking(newGame.getGameId());
					if(gameRanking != null) {
						switch(newGame.getGameRound()) {
							case R369:
								newGame.setCurrentPlayer(gameRanking.getPlayer(1).getKey());
								break;
							default:
								newGame.setCurrentPlayer(gameRanking.getPlayerWithLowestScore());
								break;
						}
					}
				} else if(newGame.getGameRound().equals(GameRound.Finale)) {
					GameRanking gameRanking = RankingRepository.getGameRanking(newGame.getGameId());
					if(gameRanking != null && !byUsername) {
						Finale turn = (Finale) TurnRepository.getOwnTurn(gameId, GameRound.Finale);
						if(turn != null && turn.getTurnStatus().equals(TurnStatus.Passed)) {
							newGame.setCurrentPlayer(turn.getPlayer().equals(gameRanking.getPlayer(2).getKey()) ?
								newGame.getCurrentPlayer() : newGame.getOpponent()
							);
						}
					}
				}
			}
			games.add(newGame);
		}
		return games;
	}

	public static Game createNewGame(String challenger, String challengee) throws SQLException { // Returns game_id
		ResultSet resultSet = new QueryBuilder("spel").create()
				.setColumns("speler1", "speler2", "toestand_type")
				.addValues(
						Helper.stringWithQuotes(challenger),
						Helper.stringWithQuotes(challengee),
						Helper.stringWithQuotes(GameStatus.Challenged.value)
				).run();
		if(resultSet.next()) {
			return new Game(resultSet.getInt(1), challenger, challengee, GameStatus.Challenged);
		}
		return null;
	}

	public static void updateStatus(int game_id, GameStatus status) throws SQLException {
		if(status == GameStatus.ChallengeReceived) {
			status = GameStatus.Challenged;
		} else if(status == GameStatus.NoGame) {
			status = GameStatus.Ended;
		}

		Column statusColumn = new Column("toestand_type", Helper.stringWithQuotes(status.value));

		new QueryBuilder("spel").update()
				.setColumns(Collections.singletonList(statusColumn))
				.where("spel_id").equals(String.valueOf(game_id)).run();
	}

	public static void createRound(int game_id, GameRound gameRound) throws SQLException {
		new QueryBuilder("ronde").create()
				.setColumns("spel_id", "rondenaam")
				.addValues(String.valueOf(game_id), Helper.stringWithQuotes(gameRound.value))
				.run();
	}

	public static List<Game> getGamesforObserver() throws SQLException {
		List<Game> games = new ArrayList<>();

		ResultSet gameResult = new QueryBuilder("spel")
				.selectAll()
				.where("toestand_type").equals(Helper.stringWithQuotes("afgelopen"))
				.get();

		while(gameResult.next()) {
			games.add(new Game(
					gameResult.getInt("spel_id"),
					gameResult.getString("speler1"),
					gameResult.getString("speler2"),
					GameStatus.fromString(gameResult.getString("toestand_type"))
			));
		}

		return games;
	}

	public static boolean isEndOpenDoor(int gameId) {
		try {
			ResultSet openDoorResult = new QueryBuilder("beurt")
					.select("COUNT(DISTINCT(vraag_id)) as number")
					.where("spel_id").equals(String.valueOf(gameId))
					.andWhere("rondenaam").equals(Helper.stringWithQuotes(GameRound.OpenDoor.value))
					.get();

			if(openDoorResult.next()) {
				return openDoorResult.getInt("number") > 1;
			}
		} catch(SQLException e) {
			e.printStackTrace();
		}
		return false;
	}

	public static boolean isEndPuzzleRound(int gameId) {
		try {
			ResultSet openDoorResult = new QueryBuilder("deelvraag")
					.select("COUNT(DISTINCT(vraag_id)) as number")
					.where("spel_id").equals(String.valueOf(gameId))
					.andWhere("rondenaam").equals(Helper.stringWithQuotes(GameRound.Puzzle.value))
					.get();

			if(openDoorResult.next()) {
				return openDoorResult.getInt("number") > 3;
			}
		} catch(SQLException e) {
			e.printStackTrace();
		}
		return false;
	}
}
