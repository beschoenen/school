package org.avans.VTSOa.deslimstemens.Controllers.Game;

import javafx.application.Platform;
import org.avans.VTSOa.deslimstemens.Enums.GameRound;
import org.avans.VTSOa.deslimstemens.Enums.TurnStatus;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Models.Database.Turn.R369;
import org.avans.VTSOa.deslimstemens.Models.Database.Turn.R369PartQuestion;
import org.avans.VTSOa.deslimstemens.Repositories.GameRepository;
import org.avans.VTSOa.deslimstemens.Repositories.QuestionRepository;
import org.avans.VTSOa.deslimstemens.Repositories.TurnRepository;
import org.avans.VTSOa.deslimstemens.Views.Game.R369View;

import java.sql.SQLException;

public class R369Controller extends BaseRoundController {
    private R369View view;

    private R369 turn;
    private R369 previousTurn;

	private int followNumber = 1;

    private int time;
	private int lastMoment;

	private boolean answerPageShown = false;

    public R369Controller() {
        super();
        this.view = new R369View(this);
	    this.view.setMenuBarTitle(this.getName());
    }

	@Override
	protected void createTurn() {
		this.turn = new R369(this.game.getGameId(), this.game.getTurns() +1, this.game.getCurrentPlayer(), TurnStatus.Going, 0);
		try {
			TurnRepository.addTurn(this.turn);
		} catch(SQLException e) {
			e.printStackTrace();
		}
	}

    public void getData() {
        try {
            this.previousTurn = (R369) TurnRepository.getPreviousTurn(this.game.getGameId(), this.game.getGameRound());
        } catch(SQLException e) {
            e.printStackTrace();
        }
        this.getQuestion();
	    this.followNumber = getFollowNumber();
    }

    @Override
    public void render() {
        super.render();
        this.view.setRanking(this.gameRanking);
        this.view.setQuestion(this.question);
    }

    @Override
    protected void getQuestion() {
        try {
            if(this.previousTurn != null && this.turn.getQuestions().size() < 1) {
	            switch (previousTurn.getTurnStatus()) {
		            case Wrong:
		            case Timeout:
		            case Passed:
			            this.question = QuestionRepository.getQuestion(previousTurn.getQuestions().get(previousTurn.getQuestions().size() - 1).getQuestionId());
			            this.question.setFromPreviousPlayer(true);
			            break;
		            default:
			            this.question = QuestionRepository.getRandomQuestion(this.game.getGameId(), this.game.getGameRound(), this.turn.getQuestionIdList());
			            break;
	            }
            } else {
	            this.question = QuestionRepository.getRandomQuestion(this.game.getGameId(), this.game.getGameRound(), this.turn.getQuestionIdList());
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    @Override
    protected void tick(int time) {
	    if(answerPageShown){
		    if(this.lastMoment + this.time + 3 < time) {
			    this.lastMoment = time;
			    this.time = 0;
			    Platform.runLater(() -> this.answerPageShown = this.view.playerAnswerControl.setAnswerMessage(null, false));
			    this.handleAnswer();
		    }
	    } else {
		    this.time++;
		    Platform.runLater(() -> this.view.playerScoreControl.setTime(this.time));
		    if(this.time % 25 == 0) {
			    this.turn.setTurnStatus(TurnStatus.Timeout);
			    Platform.runLater(() -> this.answerPageShown = this.view.playerAnswerControl.setAnswerTurnStatus(TurnStatus.Timeout, !hasNextQuestion()));
		    }
	    }
    }

	@Override
	public int getQuestionNumber() {
		return this.followNumber;
	}

	public void endRound() {
        try {
            GameRepository.createRound(this.game.getGameId(), GameRound.OpenDoor);
        } catch(SQLException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void endTurn() {
        try {
            TurnRepository.updateTurn(this.turn);
        } catch(SQLException e) {
            e.printStackTrace();
        }
        this.view.goBack();
    }

    @Override
    public SceneView getView() {
        return this.view;
    }

    @Override
    public String getName() {
        return "3-6-9 Ronde";
    }

    @Override
    public void answerEvent(String answer) {
	    R369PartQuestion question = new R369PartQuestion(
			    this.followNumber,
			    this.question.getId(),
			    answer
	    );
	    this.turn.addQuestion(question);

	    if(this.question.checkGivenAnswer(answer)){
		    this.turn.setTurnStatus(TurnStatus.Correct);
		    Platform.runLater(() -> this.answerPageShown = this.view.playerAnswerControl.setAnswerTurnStatus(TurnStatus.Correct, !hasNextQuestion()));
	    } else {
		    this.turn.setTurnStatus(TurnStatus.Wrong);
		    Platform.runLater(() -> this.answerPageShown = this.view.playerAnswerControl.setAnswerTurnStatus(TurnStatus.Wrong, !hasNextQuestion()));
	    }
    }

	@Override
	public void passEvent() {
		this.turn.setTurnStatus(TurnStatus.Passed);
		Platform.runLater(() -> this.answerPageShown = this.view.playerAnswerControl.setAnswerTurnStatus(TurnStatus.Passed, !hasNextQuestion()));
	}

	private void handleAnswer() {
		switch (this.turn.getTurnStatus()){
			case Correct:
				if(this.followNumber % 3 == 0) {
					this.turn.setSecondsRewarded(this.turn.getSecondsRewarded() + 20);
					Platform.runLater(() -> this.view.playerScoreControl.addScore(
							this.turn.getPlayer().equals(this.gameRanking.getPlayer(1).getKey()) ? 1 : 2,
							20
					));
				}
				prepareNextQuestion();
				break;
			case Wrong:
				if (this.question.isFromPreviousPlayer()){
					prepareNextQuestion();
					break;
				}
				nextTurn();
				break;
			case Passed:
				this.turn.addQuestion(new R369PartQuestion(
						this.followNumber,
						this.question.getId(),
						null
				));
				if (this.question.isFromPreviousPlayer()){
					prepareNextQuestion();
					break;
				}
				nextTurn();
				break;
			case Timeout:
				this.turn.addQuestion(new R369PartQuestion(
						this.followNumber,
						this.question.getId(),
						null
				));
				endTurn();
				break;
		}
		this.followNumber++;
	}

	private int getFollowNumber() {
		int followNumber = 0;
		if(this.previousTurn != null && this.previousTurn.getCurrentFollowNumber() > 0) {
			followNumber = this.previousTurn.getCurrentFollowNumber();
		}
		if(this.turn.getCurrentFollowNumber() > 0) {
			followNumber = this.turn.getCurrentFollowNumber();
		}
		followNumber = followNumber + (this.question.isFromPreviousPlayer() ? 0 : 1);
		return followNumber;
	}

    private boolean hasNextQuestion() {
        return this.followNumber < 9 || (!this.question.isFromPreviousPlayer() && (
				        this.turn.getTurnStatus().equals(TurnStatus.Passed) ||
				        this.turn.getTurnStatus().equals(TurnStatus.Timeout) ||
		                this.turn.getTurnStatus().equals(TurnStatus.Wrong)
		        ));
    }

	private void prepareNextQuestion() {
		if(hasNextQuestion()) {
			getQuestion();
			Platform.runLater(() -> {
				this.view.setQuestion(this.question);
				this.view.setRanking(this.gameRanking);
			});
		} else {
			nextTurn();
		}
	}

    private void nextTurn() {
        if(!hasNextQuestion()) {
	        try {
		        TurnRepository.addTurn(new R369(this.game.getGameId(), this.turn.getTurnId() + 1, this.game.getCurrentPlayer(), TurnStatus.Bonus, 60));
		        TurnRepository.addTurn(new R369(this.game.getGameId(), this.turn.getTurnId() + 2, this.game.getOpponent(), TurnStatus.Bonus, 60));
	        } catch(SQLException e) {
		        e.printStackTrace();
	        }
            endRound();
        }
        endTurn();
    }
}

