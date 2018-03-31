package org.avans.VTSOa.deslimstemens.Controllers.Game;

import javafx.application.Platform;
import org.avans.VTSOa.deslimstemens.Enums.GameStatus;
import org.avans.VTSOa.deslimstemens.Enums.TurnStatus;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Models.Database.Turn.Finale;
import org.avans.VTSOa.deslimstemens.Models.Database.Turn.PlayerAnswer;
import org.avans.VTSOa.deslimstemens.Repositories.GameRepository;
import org.avans.VTSOa.deslimstemens.Repositories.QuestionRepository;
import org.avans.VTSOa.deslimstemens.Repositories.TurnRepository;
import org.avans.VTSOa.deslimstemens.Views.Game.FinaleView;

import java.sql.SQLException;

public class FinaleController extends BaseRoundController {

	private FinaleView view;

	private int answeredQuestions;

	private int playerNumber;
	private int scoreCurrentPlayer;
	private int startScoreCurrentPlayer;

	private int opponentNumber;
	private int secondsLost;
	private int startScoreOpponent;

	private boolean forceNewQuestion = false;

	private Finale turn;
	private Finale previousTurn;

	private boolean answerPageShown;
	private int ownTime;

	public FinaleController() {
		super();
		this.view = new FinaleView(this);
		this.view.setMenuBarTitle(this.getName());
	}

	@Override
	protected void createTurn() {
		this.turn = new Finale(this.game.getGameId(), this.game.getTurns() +1, this.game.getCurrentPlayer(), TurnStatus.Going, 0, 0, 0);
		try {
			TurnRepository.addTurn(this.turn);
		} catch(SQLException e) {
			e.printStackTrace();
		}
	}

	@Override
	public void getData() {
		try {
			this.previousTurn = (Finale) TurnRepository.getPreviousTurn(this.game.getGameId(), this.game.getGameRound());
		} catch (SQLException e) {
			e.printStackTrace();
		}

		this.playerNumber = this.turn.getPlayer().equals(this.gameRanking.getPlayer(1).getKey()) ? 1 : 2;
		this.opponentNumber = (this.playerNumber == 1 ? 2 : 1);

		this.scoreCurrentPlayer = this.startScoreCurrentPlayer = this.gameRanking.getPlayer(this.playerNumber).getValue();
		this.startScoreOpponent = this.gameRanking.getPlayer(this.opponentNumber).getValue();

		startTurn();
	}

	private void startTurn() {
		this.getQuestion();
		this.turn.setQuestionId(this.question.getId());
	}

	@Override
	public void render() {
		super.render();
		this.view.setQuestion(this.question);
		this.view.setRanking(this.gameRanking);

		if(this.previousTurn != null && this.question.isFromPreviousPlayer()) {
			this.previousTurn.getAnswers().forEach(playerAnswer -> {
				String correctAnswer = question.getGivenAnswer(playerAnswer.getAnswer());
				if(correctAnswer != null) {
					this.answeredQuestions++;
					Platform.runLater(() -> view.revealAnswer(correctAnswer));
				}
			});
		}
	}

	@Override
	public void endRound() {
		try {
			GameRepository.updateStatus(this.game.getGameId(), GameStatus.Ended);
		} catch(SQLException e) {
			e.printStackTrace();
		}
		// TODO end round message; timeout
		this.view.goBack();
	}

	@Override
	public void endTurn() {
		try {
			TurnRepository.updateTurn(this.turn);
		} catch(SQLException e) {
			e.printStackTrace();
		}
	}

	@Override
	public void answerEvent(String answer) {
		String correctAnswer = this.question.getGivenAnswer(answer);
		if(correctAnswer != null) {
			this.turn.setTurnStatus(TurnStatus.Correct);
			this.answeredQuestions++;
			this.secondsLost -= 30;
			this.gameRanking.setScore(this.opponentNumber, this.startScoreOpponent + this.secondsLost);
			Platform.runLater(() -> this.view.setRanking(this.gameRanking));
			this.view.revealAnswer(correctAnswer);
		}

		this.turn.addAnswer(new PlayerAnswer(
				this.turn.getAnswerId(),
				answer,
				this.scoreCurrentPlayer
		));

		if(startScoreOpponent + this.secondsLost < 1) {
			this.turn.setTurnStatus(TurnStatus.Correct);
			Platform.runLater(() -> this.answerPageShown = this.view.playerAnswerControl.setAnswerMessage("De tegenstander heeft geen punten meer", true));
			return;
		}

		if(this.answeredQuestions == this.question.getAnswers().size()) {
			this.turn.setTurnStatus(TurnStatus.Correct);
			Platform.runLater(() -> this.answerPageShown = this.view.playerAnswerControl.setAnswerTurnStatus(TurnStatus.Passed, false));
		}
	}

	@Override
	public void passEvent() {
		this.turn.setTurnStatus(TurnStatus.Passed);
		Platform.runLater(() -> this.answerPageShown = this.view.playerAnswerControl.setAnswerTurnStatus(TurnStatus.Correct, false));
	}

	@Override
	protected void getQuestion() {
		try {
			if(this.previousTurn != null && !forceNewQuestion) {
				switch (previousTurn.getTurnStatus()) {
					case Wrong:
					case Timeout:
					case Passed:
						this.question = QuestionRepository.getQuestion(this.previousTurn.getQuestionId());
						this.question.setFromPreviousPlayer(true);
						break;
					default:
						this.question = QuestionRepository.getRandomQuestion(this.game.getGameId(), this.game.getGameRound());
						break;
				}
			} else {
				this.question = QuestionRepository.getRandomQuestion(this.game.getGameId(), this.game.getGameRound());
			}
		} catch (SQLException e) {
			e.printStackTrace();
		}
	}

	@Override
	protected void tick(int time) {
		if(answerPageShown){
			if(this.ownTime + 3 < time){
				Platform.runLater(() -> this.answerPageShown = this.view.playerAnswerControl.setAnswerMessage(null, false));
				this.nextTurn();
			}
		} else {
			this.scoreCurrentPlayer--;
			this.ownTime = time;
			this.gameRanking.setScore(this.playerNumber, this.scoreCurrentPlayer);
			Platform.runLater(() -> this.view.setRanking(this.gameRanking));
			if(this.scoreCurrentPlayer < 1) {
				this.turn.setTurnStatus(TurnStatus.Timeout);
				Platform.runLater(() -> this.answerPageShown = this.view.playerAnswerControl.setAnswerTurnStatus(TurnStatus.Timeout, false));
			}
		}
	}

	private void nextTurn() {
		this.turn.setSecondsRewarded(this.scoreCurrentPlayer - this.startScoreCurrentPlayer);
		this.turn.setSecondsLost(this.secondsLost);
		if(this.scoreCurrentPlayer < 1 || startScoreOpponent + this.secondsLost < 1) {
			endTurn();
			endRound();
		} else if(this.game.getCurrentPlayer().equals(this.gameRanking.getPlayerWithLowestScore()) && !this.turn.getTurnStatus().equals(TurnStatus.Passed)) {
			try {
				this.startScoreCurrentPlayer = this.scoreCurrentPlayer;

				this.startScoreOpponent += this.secondsLost;
				this.secondsLost = 0;

				TurnRepository.updateTurn(this.turn);

				this.previousTurn = this.turn;
				this.turn = new Finale(
						this.game.getGameId(),
						this.turn.getTurnId() +1,
						this.turn.getPlayer(),
						TurnStatus.Going,
						0, 0, 0
				);

				TurnRepository.addTurn(this.turn);

				this.answeredQuestions = 0;

				this.forceNewQuestion = true;
				startTurn();

				Platform.runLater(() -> this.view.setQuestion(this.question));
			} catch(SQLException e) {
				e.printStackTrace();
			}
		} else {
			endTurn();
			this.view.goBack();
		}
	}

	@Override
	public int getQuestionNumber() {
		return 0;
	}

	@Override
	public SceneView getView() {
		return view;
	}

	@Override
	public String getName() {
		return "Finale Ronde";
	}
}

