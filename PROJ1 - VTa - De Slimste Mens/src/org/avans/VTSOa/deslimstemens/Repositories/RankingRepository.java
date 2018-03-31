package org.avans.VTSOa.deslimstemens.Repositories;

import org.avans.VTSOa.deslimstemens.Helpers.Database.QueryBuilder;
import org.avans.VTSOa.deslimstemens.Helpers.Helper;
import org.avans.VTSOa.deslimstemens.Models.Database.GameRanking;
import org.avans.VTSOa.deslimstemens.Models.Database.Ranking;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class RankingRepository {

	public static List<Ranking> getAllRankings() throws SQLException {
		List<Ranking> rankings = new ArrayList<>();
	ResultSet rankingResult = new QueryBuilder("competitiestand")
				.selectAll()
				.orderBy("aantal_gewonnen_spellen", "DESC")
				.get();
		while(rankingResult.next()) {
			rankings.add(new Ranking(
					rankingResult.getString("speler"),
					rankingResult.getInt("aantal_gespeelde_spellen"),
					rankingResult.getInt("aantal_gewonnen_spellen"),
					rankingResult.getInt("aantal_verloren_spellen"),
					rankingResult.getInt("gemiddeld_aantal_seconden_over")
			));
		}
		return rankings;
	}

	public static Ranking getRanking(String user) throws SQLException {
		ResultSet rankingResult = new QueryBuilder("competitiestand")
				.selectAll()
				.where("speler").equals(Helper.stringWithQuotes(user))
				.first();
		if(rankingResult.next()) {
			return new Ranking(
					rankingResult.getString("speler"),
					rankingResult.getInt("aantal_gespeelde_spellen"),
					rankingResult.getInt("aantal_gewonnen_spellen"),
					rankingResult.getInt("aantal_verloren_spellen"),
					rankingResult.getInt("gemiddeld_aantal_seconden_over")
			);
		}
		return null;
	}

	public static GameRanking getGameRanking(int gameId) throws SQLException {
		ResultSet resultSet = new QueryBuilder("score")
				.select("speler1", "speler2", "seconden1", "seconden2")
				.where("spel_id").equals(String.valueOf(gameId)).get();
		if(resultSet.next()) {
			return new GameRanking(
					resultSet.getString("speler1"),
					resultSet.getInt("seconden1"),
					resultSet.getString("speler2"),
					resultSet.getInt("seconden2")
			);
		}
		return null;
	}
}
