package org.avans.VTSOa.deslimstemens.Controllers.Game;

import javafx.application.Platform;
import org.avans.VTSOa.deslimstemens.Enums.GameRound;
import org.avans.VTSOa.deslimstemens.Enums.TurnStatus;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Models.Database.Question;
import org.avans.VTSOa.deslimstemens.Models.Database.Turn.PlayerAnswer;
import org.avans.VTSOa.deslimstemens.Models.Database.Turn.Puzzle;
import org.avans.VTSOa.deslimstemens.Models.Database.Turn.PuzzlePartQuestion;
import org.avans.VTSOa.deslimstemens.Repositories.GameRepository;
import org.avans.VTSOa.deslimstemens.Repositories.QuestionRepository;
import org.avans.VTSOa.deslimstemens.Repositories.TurnRepository;
import org.avans.VTSOa.deslimstemens.Views.Game.PuzzleView;

import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.function.Consumer;

public class PuzzleController extends BaseRoundController {
	private PuzzleView view;

	private int playerNumber;

	private int startScore;
	private int score;

	private int answeredQuestions = 0;

	private List<Question> questions = new ArrayList<>(3);

	private Puzzle turn;
	private Puzzle previousTurn;

	private boolean answerPageShown;
	private boolean gameEnded;
	private int ownTime;
	private boolean forceNewQuestion;

	public PuzzleController() {
	    super();
	    this.view = new PuzzleView(this);
		this.view.setMenuBarTitle(this.getName());
    }

	@Override
	protected void createTurn() {
		this.turn = new Puzzle(this.game.getGameId(), this.game.getTurns() +1, this.game.getCurrentPlayer(), TurnStatus.Going, 0);
		try {
			TurnRepository.addTurn(this.turn);
		} catch(SQLException e) {
			e.printStackTrace();
		}
	}

    @Override
    public void getData() {
	    try {
		    this.previousTurn = (Puzzle) TurnRepository.getPreviousTurn(this.game.getGameId(), this.game.getGameRound());
	    } catch (SQLException e) {
		    e.printStackTrace();
	    }

	    this.playerNumber = this.turn.getPlayer().equals(this.gameRanking.getPlayer(1).getKey()) ? 1 : 2;
	    this.score = this.startScore = this.gameRanking.getPlayer(this.playerNumber).getValue();

	    this.getQuestion();
    }

    @Override
    public void render() {
	    super.render();
		this.view.setQuestions(this.questions);
	    this.view.setRanking(this.gameRanking);

	    if(this.previousTurn != null && this.question.isFromPreviousPlayer()) {
		    this.questions.forEach(question1 -> previousTurn.getAnswers().forEach(playerAnswer -> {
			    String correctAnswer = question1.checkPuzzleAnswer(playerAnswer.getAnswer());
			    if(question1.getQuestion().equals(correctAnswer)) {
				    answeredQuestions++;
				    Platform.runLater(() -> view.revealAnswer(correctAnswer));
			    }
		    }));
	    }
    }

    @Override
    public void endRound() {
	    try {
		    GameRepository.createRound(this.game.getGameId(), GameRound.Framed);
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
		this.questions.forEach(puzzleQuestion -> {
			String correctAnswer = puzzleQuestion.checkPuzzleAnswer(answer);
			if(correctAnswer != null) {
				this.turn.setTurnStatus(TurnStatus.Correct);
				this.answeredQuestions++;
				this.score += 60;
				this.gameRanking.setScore(playerNumber, this.score);
				Platform.runLater(() -> this.view.setRanking(this.gameRanking));
				this.view.revealAnswer(correctAnswer);
			}
		});

		this.turn.addAnswer(new PlayerAnswer(
				this.turn.getAnswerId(),
				answer,
				this.score
		));

		if(this.answeredQuestions == this.questions.size()) {
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
		this.questions.clear();
		for (int i = 0; i < 3; i++){
			try {
				if(this.previousTurn != null && !forceNewQuestion) {
					switch (previousTurn.getTurnStatus()) {
						case Wrong:
						case Timeout:
						case Passed:
							this.question = QuestionRepository.getQuestion(previousTurn.getQuestions().get(previousTurn.getQuestions().size() - i -1).getQuestionId());
							this.question.setFromPreviousPlayer(true);
							break;
						default:
							this.question = QuestionRepository.getRandomQuestion(this.game.getGameId(), this.game.getGameRound(), this.turn.getQuestionIdList());
							break;
					}
				} else {
					this.question = QuestionRepository.getRandomQuestion(this.game.getGameId(), this.game.getGameRound(), this.turn.getQuestionIdList());
				}
				this.question.setPuzzleNumber(i +1);
				addPartQuestion(this.question);
				this.questions.add(this.question);
			} catch (SQLException e) {
				e.printStackTrace();
			}
		}
	}

	private void addPartQuestion(Question question) {
		this.turn.addQuestion(new PuzzlePartQuestion(
				question.getPuzzleNumber(),
				question.getId()
		));
	}

	@Override
	protected void tick(int time) {
		if(answerPageShown){
			if(this.ownTime + 3 < time){
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

				if(GameRepository.isEndPuzzleRound(this.game.getGameId())) {
					endRound();
					return;
				}

				this.previousTurn = this.turn;
				this.turn = new Puzzle(
						this.game.getGameId(),
						this.turn.getTurnId() + 1,
						this.game.getCurrentPlayer(),
						TurnStatus.Going,
						0
				);

				TurnRepository.addTurn(this.turn);

				this.answeredQuestions = 0;
				this.forceNewQuestion = true;

				this.getQuestion();
				Platform.runLater(() -> this.view.setQuestions(this.questions));
			} catch(SQLException e) {
				e.printStackTrace();
			}
		} else {
			this.turn.setSecondsRewarded(this.score - this.startScore);
			endTurn();
			if(GameRepository.isEndPuzzleRound(this.game.getGameId()) && !(
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
        return "Puzzel Ronde";
    }
}

