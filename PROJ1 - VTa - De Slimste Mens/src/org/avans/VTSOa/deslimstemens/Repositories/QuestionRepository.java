package org.avans.VTSOa.deslimstemens.Repositories;

import org.avans.VTSOa.deslimstemens.Enums.GameRound;
import org.avans.VTSOa.deslimstemens.Helpers.Database.DatabaseSelectCore;
import org.avans.VTSOa.deslimstemens.Helpers.Helper;
import org.avans.VTSOa.deslimstemens.Models.Database.Answer;
import org.avans.VTSOa.deslimstemens.Models.Database.Question;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

public class QuestionRepository {

	public static Question getRandomQuestion(int gameId, GameRound gameRound) throws SQLException {
		return getRandomQuestion(gameId, gameRound, new ArrayList<>());
	}

	public static Question getRandomQuestion(int gameId, GameRound gameRound, List<Integer> ints) throws SQLException {
		List<String> stringList = new ArrayList<>();
		stringList.addAll(ints.stream().map(String::valueOf).collect(Collectors.toList()));

		ResultSet questionResult = new DatabaseSelectCore("vraag").get(
				String.format(
					"SELECT\n" +
						"vraag_id AS id\n" +
					"FROM vraag\n" +
					"WHERE vraag_id NOT IN (\n" +
						"SELECT\n" +
							"beurt.vraag_id\n" +
						"FROM beurt\n" +
						"WHERE beurt.spel_id = %1$s\n" +
						"AND beurt.rondenaam = %2$s\n" +
						"AND beurt.vraag_id IS NOT NULL\n" +
						"UNION\n" +
						"SELECT\n" +
							"deelvraag.vraag_id\n" +
						"FROM deelvraag\n" +
						"WHERE deelvraag.spel_id = %1$s\n" +
						"AND deelvraag.rondenaam = %2$s\n" +
						"AND deelvraag.vraag_id IS NOT NULL\n" +
						(ints.size() > 0 ? "AND deelvraag.vraag_id NOT IN (%3$s)\n" : "") +
					")\n" +
					"AND vraag.rondenaam = %2$s\n" +
					"ORDER BY RAND()\n" +
					"LIMIT 1",
					gameId,
					Helper.stringWithQuotes(gameRound.value),
					String.join(", ", stringList)
				));

		if(questionResult.next()) {
			return getQuestion(questionResult.getInt("id"));
		}
		return null;
	}

	public static Question getQuestion(int questionId) throws SQLException {
		ResultSet questionResult = new DatabaseSelectCore("vraag")
				.select("vraag.*", "vraag.vraagtekst", "sleutel.antwoord", "alternatief.synoniem")
				.leftJoin("sleutel").onEquals("vraag.vraag_id", "sleutel.vraag_id")
				.leftJoin("alternatief").onRaw("alternatief.vraag_id = vraag.vraag_id AND sleutel.antwoord = alternatief.antwoord")
				.where("vraag.vraag_id").equals(String.valueOf(questionId)).get();
		return makeQuestion(questionResult);
	}

	private static Question makeQuestion(ResultSet questionResult) throws SQLException {
		Question question = null;

		Answer answer = null;

		List<Answer> answers = new ArrayList<>();

		while(questionResult.next()) {
			if(question == null) {
				question = new Question(
						questionResult.getInt("vraag_id"),
						GameRound.fromString(questionResult.getString("rondenaam")),
						questionResult.getString("vraagtekst")
				);
			}
			Answer newAnswer = new Answer(questionResult.getString("antwoord"));
			String newAlternative = questionResult.getString("synoniem");

			if(answer == null) answer = newAnswer;

			boolean sameAnswer = answer.getAnswer().equals(newAnswer.getAnswer());

			if(sameAnswer) {
				answer.addAlterantive(newAlternative);
			} else {
				answers.add(answer);
				answer = newAnswer;
			}

			if(questionResult.isLast()) {
				if(sameAnswer) {
					answer.addAlterantive(newAlternative);
				}
				answers.add(answer);
				question.setAnswers(answers);
			}
		}
		return question;
	}
}
