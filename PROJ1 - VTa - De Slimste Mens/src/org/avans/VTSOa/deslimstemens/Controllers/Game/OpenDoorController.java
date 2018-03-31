package org.avans.VTSOa.deslimstemens.Controllers.Game;

import javafx.application.Platform;
import org.avans.VTSOa.deslimstemens.Enums.GameRound;
import org.avans.VTSOa.deslimstemens.Enums.GameStatus;
import org.avans.VTSOa.deslimstemens.Enums.TurnStatus;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Models.Database.Turn.FramedOpenDoor;
import org.avans.VTSOa.deslimstemens.Models.Database.Turn.PlayerAnswer;
import org.avans.VTSOa.deslimstemens.Repositories.GameRepository;
import org.avans.VTSOa.deslimstemens.Repositories.QuestionRepository;
import org.avans.VTSOa.deslimstemens.Repositories.TurnRepository;
import org.avans.VTSOa.deslimstemens.Views.Game.OpenDoorView;

import java.sql.SQLException;

public class OpenDoorController extends BaseRoundController {

	private OpenDoorView view;

	private int playerNumber;
	private int ownTime;
	private int startScore;
	private int score;

	private int answeredQuestions = 0;

	private boolean forceNewQuestion = false;

	private boolean answerPageShown = false;

	private FramedOpenDoor turn;
	private FramedOpenDoor previousTurn;
	private boolean gameEnded;

	public OpenDoorController() {
		super();
		this.view = new OpenDoorView(this);
		this.view.setMenuBarTitle(this.getName());
	}

	@Override
	protected void createTurn() {
		this.turn = new FramedOpenDoor(false, this.game.getGameId(), this.game.getTurns() +1, 0, this.game.getCurrentPlayer(), TurnStatus.Going, 0);
		try {
			TurnRepository.addTurn(this.turn);
		} catch(SQLException e) {
			e.printStackTrace();
		}
	}

	@Override
	public void getData() {
		try {
			this.previousTurn = (FramedOpenDoor) TurnRepository.getPreviousTurn(this.game.getGameId(), this.game.getGameRound());
		} catch (SQLException e) {
			e.printStackTrace();
		}

		this.playerNumber = this.turn.getPlayer().equals(this.gameRanking.getPlayer(1).getKey()) ? 1 : 2;
		this.score = this.startScore = this.gameRanking.getPlayer(this.playerNumber).getValue();

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
			GameRepository.createRound(this.game.getGameId(), GameRound.Puzzle);
		} catch(SQLException e) {
			e.printStackTrace();
		}
		Platform.runLater(() -> this.answerPageShown = this.view.playerAnswerControl.setAnswerMessage(this.getName()+" is afgelopen", false));
		this.view.goBack();
	}

	@Override
	public void endTurn() {
		this.turn.setSecondsRewarded(this.score - this.startScore);
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
			this.score += 40;
			this.gameRanking.setScore(playerNumber, this.score);
			Platform.runLater(() -> {
				this.view.setRanking(this.gameRanking);
			});
			this.view.revealAnswer(correctAnswer);
		}

		this.turn.addAnswer(new PlayerAnswer(
				this.turn.getAnswerId(),
				answer,
				this.score
		));

		if(this.answeredQuestions == this.question.getAnswers().size()) {
			this.turn.setTurnStatus(TurnStatus.Correct);
			Platform.runLater(() -> this.answerPageShown = this.view.playerAnswerControl.setAnswerTurnStatus(TurnStatus.Correct, false));
		}
	}

	@Override
	public void passEvent() {
		this.turn.setTurnStatus(TurnStatus.Passed);
		Platform.runLater(() -> this.answerPageShown = this.view.playerAnswerControl.setAnswerTurnStatus(TurnStatus.Passed, false));
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
			if(this.ownTime + 3 < time) {
				Platform.runLater(() -> this.answerPageShown = this.view.playerAnswerControl.setAnswerMessage(null, false));
				this.nextTurn();
			}
		} else {
			this.score--;
			this.ownTime = time;
			this.gameRanking.setScore(this.playerNumber, this.score);
			Platform.runLater(() -> this.view.setRanking(this.gameRanking));
			if(this.score < 1) {
				this.turn.setTurnStatus(TurnStatus.Timeout);
				Platform.runLater(() -> this.answerPageShown = this.view.playerAnswerControl.setAnswerTurnStatus(TurnStatus.Timeout, true));
				this.gameEnded = true;
			}
		}
//		System.out.println("Ticktime: "+time+"\tTickOwnTime: "+this.ownTime+"\tStartscore: "+this.startScore+"\tTickscore: "+this.score+"\tAnswerscore: "+(this.score-this.startScore));
	}

	private void nextTurn() {
		if(this.gameEnded) {
			this.endGame(this.turn);
			this.view.goBack();
			return;
		}

		if(this.question.isFromPreviousPlayer()) {
			try {
				this.turn.setSecondsRewarded(this.score - this.startScore);
				this.startScore = this.score;

				TurnRepository.updateTurn(this.turn);

				if(GameRepository.isEndOpenDoor(this.game.getGameId())) {
					endRound();
					return;
				}

				this.previousTurn = this.turn;
				this.turn = new FramedOpenDoor(
						false,
						this.game.getGameId(),
						this.turn.getTurnId() + 1,
						0,
						this.game.getCurrentPlayer(),
						TurnStatus.Going,
						0
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
			this.turn.setSecondsRewarded(this.score - this.startScore);
			endTurn();
			if(GameRepository.isEndOpenDoor(this.game.getGameId()) && !(
					this.turn.getTurnStatus().equals(TurnStatus.Passed) ||
					this.turn.getTurnStatus().equals(TurnStatus.Timeout)
			)) {
				endRound();
			} else {
				this.view.goBack();
			}
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
		return "Open Deur Ronde";
	}
}

