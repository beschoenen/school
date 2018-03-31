package org.avans.VTSOa.deslimstemens.Repositories;

import org.avans.VTSOa.deslimstemens.Enums.GameRound;
import org.avans.VTSOa.deslimstemens.Enums.TurnStatus;
import org.avans.VTSOa.deslimstemens.Helpers.Database.DatabaseCreateCore;
import org.avans.VTSOa.deslimstemens.Helpers.Database.QueryBuilder;
import org.avans.VTSOa.deslimstemens.Helpers.Helper;
import org.avans.VTSOa.deslimstemens.Models.Database.Column;
import org.avans.VTSOa.deslimstemens.Models.Database.PlayerTurn;
import org.avans.VTSOa.deslimstemens.Models.Database.Turn.*;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class TurnRepository {

	public static List<PlayerTurn> getTurnsByGameRound(int gameId, GameRound gameRound) throws SQLException {
		List<PlayerTurn> playerTurns = new ArrayList<>();
		ResultSet turns = new QueryBuilder("beurt")
				.select("spel_id", "rondenaam", "beurt_id", "speler", "beurtstatus", "sec_verdiend", "sec_finale_af")
				.where("spel_id").equals(String.valueOf(gameId))
				.andWhere("rondenaam").equals(Helper.stringWithQuotes(gameRound.value))
				.orderBy("spel_id").orderBy("rondenaam").orderBy("beurt_id")
				.get();
		while(turns.next()) {
			playerTurns.add(new PlayerTurn(
					turns.getInt("beurt_id"),
					turns.getInt("spel_id"),
					GameRound.fromString(turns.getString("rondenaam")),
					turns.getString("speler"),
					TurnStatus.fromString(turns.getString("beurtstatus")),
					turns.getInt("sec_verdiend"),
					turns.getInt("sec_finale_af")
			));
		}
		return playerTurns;
	}

	public static List<PlayerTurn> getTurnsByGame(Integer gameId) throws SQLException {
		List<PlayerTurn> playerTurns = new ArrayList<>();
		ResultSet turns = new QueryBuilder("beurt")
				.select("spel_id", "rondenaam", "beurt_id", "speler", "beurtstatus", "sec_verdiend", "sec_finale_af")
				.join("rondenaam").onEquals("beurt.rondenaam", "rondenaam.type")
				.where("spel_id").equals(String.valueOf(gameId))
				.andWhere("beurt.beurtstatus").isNot(Helper.stringWithQuotes("bezig"))
				.orderBy("rondenaam.volgnr", "ASC").orderBy("beurt.beurt_id", "ASC")
				.get();
		while(turns.next()) {
			playerTurns.add(new PlayerTurn(
					turns.getInt("beurt_id"),
					turns.getInt("spel_id"),
					GameRound.fromString(turns.getString("rondenaam")),
					turns.getString("speler"),
					TurnStatus.fromString(turns.getString("beurtstatus")),
					turns.getInt("sec_verdiend"),
					turns.getInt("sec_finale_af")
			));
		}
		return playerTurns;
	}

	public static PlayerTurn getLastTurn(Integer gameId) throws SQLException {
		ResultSet turns = new QueryBuilder("beurt")
				.selectAll()
				.join("rondenaam").onEquals("rondenaam.type", "beurt.rondenaam")
				.where("beurt.spel_id").equals(String.valueOf(gameId))
				.andWhere("beurt.beurtstatus").isNot(Helper.stringWithQuotes("bezig"))
				.orderBy("rondenaam.volgnr").orderBy("beurt_id")
				.first();
		if(turns.next()) {
			return new PlayerTurn(
					turns.getInt("beurt_id"),
					turns.getInt("spel_id"),
					GameRound.fromString(turns.getString("rondenaam")),
					turns.getString("speler"),
					TurnStatus.fromString(turns.getString("beurtstatus")),
					turns.getInt("sec_verdiend"),
					turns.getInt("sec_finale_af")
			);
		}
		return null;
	}

	public static void addTurn(SaveTurn turn) throws SQLException {
		switch(turn.getGameRound()) {
			case R369:
				createR369((R369) turn);
				break;
			case Puzzle:
				createPuzzle((Puzzle) turn);
				break;
			case Framed:
			case OpenDoor:
				createFramedOpenDoor((FramedOpenDoor) turn);
				break;
			case Finale:
				createFinale((Finale) turn);
				break;
		}
	}

	public static void updateTurn(SaveTurn turn) throws SQLException {
		switch(turn.getGameRound()) {
			case R369:
				updateR369((R369) turn);
				break;
			case Puzzle:
				updatePuzzle((Puzzle) turn);
				break;
			case Framed:
			case OpenDoor:
				updateFramedOpenDoor((FramedOpenDoor) turn);
				break;
			case Finale:
				updateFinale((Finale) turn);
				break;
		}
	}

	public static SaveTurn getOwnTurn(Integer gameId, GameRound gameRound) throws SQLException {
		return getTurn(gameId, gameRound, 0);
	}

	public static SaveTurn getPreviousTurn(Integer gameId, GameRound gameRound) throws SQLException {
		return getTurn(gameId, gameRound, 1);
	}

	private static SaveTurn getTurn(Integer gameId, GameRound gameRound, int offset) throws SQLException {
		ResultSet turnIdSet = new QueryBuilder("beurt")
				.select("MAX(beurt_id) AS id")
				.where("spel_id").equals(String.valueOf(gameId))
				.andWhere("rondenaam").equals(Helper.stringWithQuotes(gameRound.value)).get();
		if(turnIdSet.next()) {
			Integer turnId = turnIdSet.getInt("id") - offset;
			if(turnId < 1) return null;

			switch(gameRound) {
				case R369:
					return getR369Turn(gameId, turnId);
				case Puzzle:
					return getPuzzleTurn(gameId, turnId);
				case Framed:
					return getFramedOpendoorTurn(gameId, turnId, GameRound.Framed);
				case OpenDoor:
					return getFramedOpendoorTurn(gameId, turnId, GameRound.OpenDoor);
				case Finale:
					return getFinaleTurn(gameId, turnId);
			}
		}
		return null;
	}

	///////
	// R369
	///////

	private static void createR369(R369 turn) throws SQLException {
		new QueryBuilder("beurt").create()
				.setColumns("spel_id", "rondenaam", "beurt_id", "speler", "beurtstatus", "sec_verdiend")
				.addValues(String.valueOf(turn.getGameId()),
						Helper.stringWithQuotes(turn.getGameRound().value),
						String.valueOf(turn.getTurnId()),
						Helper.stringWithQuotes(turn.getPlayer()),
						Helper.stringWithQuotes(turn.getTurnStatus().value),
						String.valueOf(turn.getSecondsRewarded())
				).run();

		saveR369Questions(turn.getGameId(), turn.getTurnId(), turn.getGameRound(), turn.getQuestions());
	}

	private static void updateR369(R369 turn) throws SQLException {
		new QueryBuilder("beurt").update()
				.setColumns(new ArrayList<Column>() {{
					add(new Column("beurtstatus", Helper.stringWithQuotes(turn.getTurnStatus().value)));
					add(new Column("sec_verdiend", String.valueOf(turn.getSecondsRewarded())));
				}})
				.where("spel_id").equals(String.valueOf(turn.getGameId()))
				.andWhere("rondenaam").equals(Helper.stringWithQuotes(turn.getGameRound().value))
				.andWhere("beurt_id").equals(String.valueOf(turn.getTurnId()))
				.run();

		saveR369Questions(turn.getGameId(), turn.getTurnId(), turn.getGameRound(), turn.getQuestions());
	}

	private static void saveR369Questions(int gameId, int turnId, GameRound gameRound, List<R369PartQuestion> questions) throws SQLException {
		if(questions != null && questions.size() > 0) {
			DatabaseCreateCore questionsQuery = new QueryBuilder("deelvraag").create()
					.setColumns("spel_id", "rondenaam", "beurt_id", "volgnummer", "vraag_id", "antwoord");

			for(R369PartQuestion question : questions) {
				questionsQuery = questionsQuery.addValues(
						String.valueOf(gameId),
						Helper.stringWithQuotes(gameRound.value),
						String.valueOf(turnId),
						String.valueOf(question.getFollowNumber()),
						String.valueOf(question.getQuestionId()),
						Helper.stringWithQuotes(question.getAnswer())
				);
			}
			questionsQuery.run();
		}
	}

	private static R369 getR369Turn(Integer gameId, Integer turnId) throws SQLException {
		ResultSet turn = new QueryBuilder("beurt")
				.selectAll()
				.where("spel_id").equals(String.valueOf(gameId))
				.andWhere("beurt_id").equals(String.valueOf(turnId))
				.andWhere("rondenaam").equals(Helper.stringWithQuotes(GameRound.R369.value))
				.first();

		if(turn.next()) {
			List<R369PartQuestion> partQuestions = new ArrayList<>();

			ResultSet partQuestionsResult = new QueryBuilder("deelvraag")
					.selectAll()
					.where("spel_id").equals(String.valueOf(gameId))
					.andWhere("rondenaam").equals(GameRound.R369.value)
					.andWhere("beurt_id").equals(String.valueOf(turnId))
					.get();

			while(partQuestionsResult.next()) {
				partQuestions.add(new R369PartQuestion(
						partQuestionsResult.getInt("volgnummer"),
						partQuestionsResult.getInt("vraag_id"),
						partQuestionsResult.getString("antwoord")
				));
			}

			return new R369(
					turn.getInt("spel_id"),
					turn.getInt("beurt_id"),
					turn.getString("speler"),
					TurnStatus.fromString(turn.getString("beurtstatus")),
					turn.getInt("sec_verdiend"),
					partQuestions
			);
		}
		return null;
	}

	/////////
	// Puzzle
	/////////

	private static void createPuzzle(Puzzle turn) throws SQLException {
		new QueryBuilder("beurt").create()
				.setColumns("spel_id", "rondenaam", "beurt_id", "speler", "beurtstatus", "sec_verdiend")
				.addValues(String.valueOf(turn.getGameId()),
						Helper.stringWithQuotes(turn.getGameRound().value),
						String.valueOf(turn.getTurnId()),
						Helper.stringWithQuotes(turn.getPlayer()),
						Helper.stringWithQuotes(turn.getTurnStatus().value),
						String.valueOf(turn.getSecondsRewarded())
				).run();

		savePuzzleQuestions(turn.getGameId(), turn.getTurnId(), turn.getGameRound(), turn.getQuestions());
		saveAnswers(turn.getGameId(), turn.getTurnId(), turn.getGameRound(), turn.getAnswers());
	}

	private static void updatePuzzle(Puzzle turn) throws SQLException {
		new QueryBuilder("beurt").update()
				.setColumns(new ArrayList<Column>() {{
					add(new Column("beurtstatus", Helper.stringWithQuotes(turn.getTurnStatus().value)));
					add(new Column("sec_verdiend", String.valueOf(turn.getSecondsRewarded())));
				}})
				.where("spel_id").equals(String.valueOf(turn.getGameId()))
				.andWhere("rondenaam").equals(Helper.stringWithQuotes(turn.getGameRound().value))
				.andWhere("beurt_id").equals(String.valueOf(turn.getTurnId()))
				.run();

		savePuzzleQuestions(turn.getGameId(), turn.getTurnId(), turn.getGameRound(), turn.getQuestions());
		saveAnswers(turn.getGameId(), turn.getTurnId(), turn.getGameRound(), turn.getAnswers());
	}

	private static void savePuzzleQuestions(int gameId, int turnId, GameRound gameRound, List<PuzzlePartQuestion> questions) throws SQLException {
		if(questions != null && questions.size() > 0) {
			DatabaseCreateCore questionsQuery = new QueryBuilder("deelvraag").create()
					.setColumns("spel_id", "rondenaam", "beurt_id", "volgnummer", "vraag_id");

			for(PuzzlePartQuestion question : questions) {
				questionsQuery = questionsQuery.addValues(
						String.valueOf(gameId),
						Helper.stringWithQuotes(gameRound.value),
						String.valueOf(turnId),
						String.valueOf(question.getFollowNumber()),
						String.valueOf(question.getQuestionId())
				);
			}
			questionsQuery.run();
		}
	}

	private static Puzzle getPuzzleTurn(Integer gameId, Integer turnId) throws SQLException {
		ResultSet turn = new QueryBuilder("beurt")
				.selectAll()
				.where("spel_id").equals(String.valueOf(gameId))
				.andWhere("beurt_id").equals(String.valueOf(turnId))
				.andWhere("rondenaam").equals(Helper.stringWithQuotes(GameRound.Puzzle.value))
				.first();

		if(turn.next()) {
			List<PuzzlePartQuestion> partQuestions = new ArrayList<>();

			// Get part question
			ResultSet partQuestionsResult = new QueryBuilder("deelvraag")
					.selectAll()
					.where("spel_id").equals(String.valueOf(gameId))
					.andWhere("rondenaam").equals(Helper.stringWithQuotes(GameRound.Puzzle.value))
					.andWhere("beurt_id").equals(String.valueOf(turnId))
					.get();

			while(partQuestionsResult.next()) {
				partQuestions.add(new PuzzlePartQuestion(
						partQuestionsResult.getInt("volgnummer"),
						partQuestionsResult.getInt("vraag_id")
				));
			}

			return new Puzzle(
					turn.getInt("spel_id"),
					turn.getInt("beurt_id"),
					turn.getString("speler"),
					TurnStatus.fromString(turn.getString("beurtstatus")),
					turn.getInt("sec_verdiend"),
					partQuestions,
					getAnswers(gameId, turnId, GameRound.Puzzle)
			);
		}
		return null;
	}

	////////////////////
	// Framed / OpenDoor
	////////////////////

	private static void createFramedOpenDoor(FramedOpenDoor turn) throws SQLException {
		new QueryBuilder("beurt").create()
				.setColumns("spel_id", "rondenaam", "beurt_id", "vraag_id", "speler", "beurtstatus", "sec_verdiend")
				.addValues(String.valueOf(turn.getGameId()),
						Helper.stringWithQuotes(turn.getGameRound().value),
						String.valueOf(turn.getTurnId()),
						turn.getQuestionId() > 0 ? String.valueOf(turn.getQuestionId()) : null,
						Helper.stringWithQuotes(turn.getPlayer()),
						Helper.stringWithQuotes(turn.getTurnStatus().value),
						String.valueOf(turn.getSecondsRewarded())
				).run();

		saveAnswers(turn.getGameId(), turn.getTurnId(), turn.getGameRound(), turn.getAnswers());
	}

	private static void updateFramedOpenDoor(FramedOpenDoor turn) throws SQLException {
		new QueryBuilder("beurt").update()
				.setColumns(new ArrayList<Column>() {{
					add(new Column("vraag_id", String.valueOf(turn.getQuestionId())));
					add(new Column("beurtstatus", Helper.stringWithQuotes(turn.getTurnStatus().value)));
					add(new Column("sec_verdiend", String.valueOf(turn.getSecondsRewarded())));
				}})
				.where("spel_id").equals(String.valueOf(turn.getGameId()))
				.andWhere("rondenaam").equals(Helper.stringWithQuotes(turn.getGameRound().value))
				.andWhere("beurt_id").equals(String.valueOf(turn.getTurnId()))
				.run();

		saveAnswers(turn.getGameId(), turn.getTurnId(), turn.getGameRound(), turn.getAnswers());
	}

	private static FramedOpenDoor getFramedOpendoorTurn(Integer gameId, Integer turnId, GameRound gameRound) throws SQLException {
		ResultSet turn = new QueryBuilder("beurt")
				.selectAll()
				.where("spel_id").equals(String.valueOf(gameId))
				.andWhere("beurt_id").equals(String.valueOf(turnId))
				.andWhere("rondenaam").equals(Helper.stringWithQuotes(gameRound.value))
				.first();

		if(turn.next()) {
			return new FramedOpenDoor(
					gameRound.equals(GameRound.Framed),
					turn.getInt("spel_id"),
					turn.getInt("beurt_id"),
					turn.getInt("vraag_id"),
					turn.getString("speler"),
					TurnStatus.fromString(turn.getString("beurtstatus")),
					turn.getInt("sec_verdiend"),
					getAnswers(gameId, turnId, gameRound)
			);
		}
		return null;
	}

	/////////
	// Finale
	/////////

	private static void createFinale(Finale turn) throws SQLException {
		new QueryBuilder("beurt").create()
				.setColumns("spel_id", "rondenaam", "beurt_id", "vraag_id", "speler", "beurtstatus", "sec_verdiend", "sec_finale_af")
				.addValues(String.valueOf(turn.getGameId()),
						Helper.stringWithQuotes(turn.getGameRound().value),
						String.valueOf(turn.getTurnId()),
						turn.getQuestionId() > 0 ? String.valueOf(turn.getQuestionId()) : null,
						Helper.stringWithQuotes(turn.getPlayer()),
						Helper.stringWithQuotes(turn.getTurnStatus().value),
						String.valueOf(turn.getSecondsRewarded()),
						String.valueOf(turn.getSecondsLost())
				).run();

		saveAnswers(turn.getGameId(), turn.getTurnId(), turn.getGameRound(), turn.getAnswers());
	}

	private static void updateFinale(Finale turn) throws SQLException {
		new QueryBuilder("beurt").update()
				.setColumns(new ArrayList<Column>() {{
					add(new Column("vraag_id", String.valueOf(turn.getQuestionId())));
					add(new Column("beurtstatus", Helper.stringWithQuotes(turn.getTurnStatus().value)));
					add(new Column("sec_verdiend", String.valueOf(turn.getSecondsRewarded())));
					add(new Column("sec_finale_af", String.valueOf(turn.getSecondsLost())));
				}})
				.where("spel_id").equals(String.valueOf(turn.getGameId()))
				.andWhere("rondenaam").equals(Helper.stringWithQuotes(turn.getGameRound().value))
				.andWhere("beurt_id").equals(String.valueOf(turn.getTurnId()))
				.run();

		saveAnswers(turn.getGameId(), turn.getTurnId(), turn.getGameRound(), turn.getAnswers());
	}

	private static Finale getFinaleTurn(Integer gameId, Integer turnId) throws SQLException {
		ResultSet turn = new QueryBuilder("beurt")
				.selectAll()
				.where("spel_id").equals(String.valueOf(gameId))
				.andWhere("beurt_id").equals(String.valueOf(turnId))
				.andWhere("rondenaam").equals(Helper.stringWithQuotes(GameRound.Finale.value))
				.first();

		if(turn.next()) {
			return new Finale(
					turn.getInt("spel_id"),
					turn.getInt("beurt_id"),
					turn.getString("speler"),
					TurnStatus.fromString(turn.getString("beurtstatus")),
					turn.getInt("sec_verdiend"),
					turn.getInt("vraag_id"),
					turn.getInt("sec_finale_af"),
					getAnswers(gameId, turnId, GameRound.Finale)
			);
		}
		return null;
	}

	//////////
	// Answers
	//////////

	private static void saveAnswers(int game_id, int turn_id, GameRound gameRound, List<PlayerAnswer> answers) throws SQLException {
		if(answers != null && answers.size() > 0) {
			DatabaseCreateCore answerQuery = new QueryBuilder("spelerantwoord").create()
					.setColumns("spel_id", "rondenaam", "beurt_id", "antwoord_id", "antwoord", "moment");

			for(PlayerAnswer answer : answers) {
				answerQuery = answerQuery.addValues(
						String.valueOf(game_id),
						Helper.stringWithQuotes(gameRound.value),
						String.valueOf(turn_id),
						String.valueOf(answer.getId()),
						Helper.stringWithQuotes(answer.getAnswer()),
						String.valueOf(answer.getMoment())
				);
			}
			answerQuery.run();
		}
	}

	private static List<PlayerAnswer> getAnswers(int gameId, int turnId, GameRound gameRound) throws SQLException {
		List<PlayerAnswer> answers = new ArrayList<>();

		ResultSet answersResult = new QueryBuilder("spelerantwoord")
				.selectAll()
				.where("spel_id").equals(String.valueOf(gameId))
				.andWhere("beurt_id").equals(String.valueOf(turnId))
				.andWhere("rondenaam").equals(Helper.stringWithQuotes(gameRound.value))
				.get();

		while(answersResult.next()) {
			answers.add(new PlayerAnswer(
					answersResult.getInt("antwoord_id"),
					answersResult.getString("antwoord"),
					answersResult.getInt("moment")
			));
		}
		return answers;
	}
}
