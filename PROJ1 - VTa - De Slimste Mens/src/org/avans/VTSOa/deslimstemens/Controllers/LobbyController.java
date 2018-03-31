package org.avans.VTSOa.deslimstemens.Controllers;

import javafx.application.Platform;
import org.avans.VTSOa.deslimstemens.Controllers.Game.GameController;
import org.avans.VTSOa.deslimstemens.Enums.GameStatus;
import org.avans.VTSOa.deslimstemens.Enums.TurnStatus;
import org.avans.VTSOa.deslimstemens.Helpers.EventListener.ChatListener;
import org.avans.VTSOa.deslimstemens.Helpers.EventListener.GameListener;
import org.avans.VTSOa.deslimstemens.Helpers.EventListener.TurnListener;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls.ChatControl;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneController;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Models.Database.ChatMessage;
import org.avans.VTSOa.deslimstemens.Models.Database.GameInfo;
import org.avans.VTSOa.deslimstemens.Models.Database.PlayerTurn;
import org.avans.VTSOa.deslimstemens.Repositories.ChatRepository;
import org.avans.VTSOa.deslimstemens.Repositories.GameRepository;
import org.avans.VTSOa.deslimstemens.RunTimeSettings;
import org.avans.VTSOa.deslimstemens.Views.LobbyView;

import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class LobbyController extends SceneController {

    List<ChatMessage> chatList = new ArrayList<>();

    List<PlayerTurn> gameDetailsList = new ArrayList<>();

    GameInfo gameInfo = null;

    ChatControl chatControl;

    ChatListener chatListener;
    GameListener gameListener;
    TurnListener turnListener;

    int gameid;
    String user;

    LobbyView view;

    public LobbyController() {
        this.gameid = RunTimeSettings.gameId;
        this.user = RunTimeSettings.user.getName();

        this.view = new LobbyView(this);

        this.view.btGame.setOnAction(event -> {
            GameController gameMain = new GameController(RunTimeSettings.gameId);
            try {
                gameMain.playRound();
            } catch (Exception e) {
                e.printStackTrace();
            }
        });

        this.chatControl = new ChatControl() {
            @Override
            public void onSend(String text) {
                postChatMessage(text, new Date());
            }
        };
    }

    private void setGameButtonStatus() {
        if (this.gameInfo.getGameStatus().equals(GameStatus.Ended)) {
            this.view.btGame.setDisable(true);
            this.view.btGame.setText("Het spel is afgelopen");
        } else if (this.gameInfo.getCurrentPlayer().equals(this.user)) {
            this.view.btGame.setDisable(false);
            this.view.btGame.setText("Ga verder met spel...");
        } else {
            this.view.btGame.setDisable(true);
            this.view.btGame.setText(String.format("Wachten op %s...", this.gameInfo.getOpponent()));
        }
    }

    // Data managers
    private String getGameDetailsItem(PlayerTurn playerTurn) {
        String message = "";
        switch(playerTurn.getGameRound()) {
            case R369:
                message = String.format("%d%s seconden verdient",
                        playerTurn.getSecondsRewarded(),
                        playerTurn.getTurnStatus().equals(TurnStatus.Bonus) ? " bonus" : ""
                );
                break;
            case OpenDoor:
            case Puzzle:
            case Framed:
                if(playerTurn.getSecondsRewarded() > 0) {
                    message = String.format("%d seconden verdient", playerTurn.getSecondsRewarded());
                } else {
                    message = String.format("%d seconden verloren", Math.abs(playerTurn.getSecondsRewarded()));
                }
                break;
            case Finale:
                message = String.format("%d seconden verloren en %d seconden van %s afgespeeld",
                        Math.abs(playerTurn.getSecondsRewarded()),
                        Math.abs(playerTurn.getSecondsTaken()),
                        playerTurn.getPlayer().equals(gameInfo.getCurrentPlayer()) ? RunTimeSettings.user.getName() : gameInfo.getCurrentPlayer()
                );
                break;
        }
        return String.format("%1$02d. %2$s | %3$s heeft %4$s",
                playerTurn.getId(),
                playerTurn.getGameRound().value,
                playerTurn.getPlayer(),
                message
        ).trim();
    }

    private String getChatItem(ChatMessage chatMessage) {
        String message = String.format("%s zegt: %s", chatMessage.getName(), chatMessage.getMessage());

        if(chatMessage.getName().equals(user)) {
            message = String.format("Ik: %s", chatMessage.getMessage());
        }
        return message;
    }

    // Database calls
    private GameInfo getGameInfo() {
        try {
            return GameRepository.findById(this.gameid, this.user);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    private void postChatMessage(String message, Date date) {
        if(!message.equals("")) {
            new Thread(() -> {
                try {
                    ChatRepository.postMessage(this.gameid, this.user, message, date);
                } catch(SQLException e) {
                    System.out.println("Couldn't save message!");
                    e.printStackTrace();
                }
            }).start();
        }
    }

    // Getters
    public ChatControl getChatControl() {
        return chatControl;
    }

    @Override
    public SceneView getView() {
        return view;
    }

    @Override
    public String getName() {
        return "Lobby";
    }

    @Override
    public void onEnter() {
        this.chatListener = new ChatListener(this.gameid) {
            @Override
            public void newMessage(ChatMessage message) {
                chatList.add(message);
                Platform.runLater(() -> view.listViewChat.getItems().add(getChatItem(message)));
            }
        };

        this.gameListener = new GameListener(this.gameid, this.user) {
            @Override
            public void gameChanged(GameInfo game) {
                gameInfo = game;
                Platform.runLater(() -> setGameButtonStatus());
            }
        };

        this.turnListener = new TurnListener(this.gameid) {
            @Override
            public void newTurn(PlayerTurn turn) {
                PlayerTurn lastTurn;
                try {
                    lastTurn = gameDetailsList.get(gameDetailsList.size() -1);
                } catch(Exception e) { lastTurn = null; }
                if(lastTurn != null) {
                    if(!turn.getGameRound().equals(lastTurn.getGameRound())) {
                        Platform.runLater(() -> view.listViewGame.getItems().add(String.format("Nu begint de %s ronde", turn.getGameRound().value)));
                    }
                } else {
                    Platform.runLater(() -> {
                        view.listViewGame.getItems().add("Het spel is begonnen!");
                        view.listViewGame.getItems().add("Nu begint de 369 ronde");
                    });
                }
                gameDetailsList.add(turn);
                Platform.runLater(() -> view.listViewGame.getItems().add(getGameDetailsItem(turn)));
                if(gameInfo != null && gameInfo.getGameStatus().equals(GameStatus.Ended)) {
                    Platform.runLater(() -> view.listViewGame.getItems().add("Het spel is afgelopen!"));
                    // TODO show winner
                }
            }
        };

        new Thread(() -> {
            this.gameInfo = getGameInfo();
            Platform.runLater(this::setGameButtonStatus);
        }).start();
    }

    @Override
    public void onLeave() {
        this.gameDetailsList.clear();
        this.view.listViewGame.getItems().clear();

        this.chatList.clear();
        this.view.listViewChat.getItems().clear();

        this.chatListener.stop();
        this.gameListener.stop();
        this.turnListener.stop();
    }
}
