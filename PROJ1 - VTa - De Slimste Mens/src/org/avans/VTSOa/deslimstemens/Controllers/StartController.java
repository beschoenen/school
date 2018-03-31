package org.avans.VTSOa.deslimstemens.Controllers;

import com.jfoenix.controls.JFXButton;
import javafx.application.Platform;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.scene.control.Alert;
import javafx.scene.control.ButtonType;
import org.avans.VTSOa.deslimstemens.Enums.GameRound;
import org.avans.VTSOa.deslimstemens.Enums.GameStatus;
import org.avans.VTSOa.deslimstemens.Helpers.EventListener.GameListener;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.*;
import org.avans.VTSOa.deslimstemens.Models.Database.GameInfo;
import org.avans.VTSOa.deslimstemens.Models.Database.PlayerGameStatus;
import org.avans.VTSOa.deslimstemens.Models.Table.GameList;
import org.avans.VTSOa.deslimstemens.Models.Table.InviteList;
import org.avans.VTSOa.deslimstemens.Repositories.AccountRepository;
import org.avans.VTSOa.deslimstemens.Repositories.GameRepository;
import org.avans.VTSOa.deslimstemens.RunTimeSettings;
import org.avans.VTSOa.deslimstemens.Views.StartView;

import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

public class StartController extends SceneController {

    List<GameInfo> listGames = new ArrayList<>();
    List<PlayerGameStatus> listPlayers = new ArrayList<>();

    GameListener gameListener;

    String user;

    StartView view;

    public StartController(){
        this.user = RunTimeSettings.user.getName();

		this.view = new StartView(this);

        this.view.btRankingView.setOnAction(event -> {
            RankingController rankingController = new RankingController();
            RunTimeSettings.mainStager.addSceneController(rankingController);
            try {
                Navigator.show("MainStage", rankingController.getName());
            } catch (MissingException e) {
                e.printStackTrace();
            }
        });

        this.view.btAccount.setOnAction(event -> {
            AccountController accountController = new AccountController();
            RunTimeSettings.mainStager.addSceneController(accountController);
            try {
                Navigator.show("MainStage", accountController.getName());
            }catch (MissingException e){
                e.printStackTrace();
            }
        });

        new Thread(() -> {
            getPlayersList();
            getGamesList();

            Platform.runLater(() -> {
                this.view.tvGames.setItems(this.getGamesData());
                this.view.tvPlayers.setItems(this.getPlayersData());
            });
        }).start();
    }

    public void getGamesList() {
        try {
            this.listGames = GameRepository.findByUser(this.user);
        } catch (SQLException e) {
            System.out.println("Error retrieving games from user");
            e.printStackTrace();
        }
    }

    public void getPlayersList() {
        //Get the data or the right table
        try {
            this.listPlayers = AccountRepository.listUsersWithGameStatus(this.user);
        } catch (SQLException e) {
            System.out.println("Error retrieving all users");
            e.printStackTrace();
        }
    }

    public ObservableList<GameList> getGamesData() {
        ObservableList<GameList> gamesList = FXCollections.observableArrayList();

        getGamesList();

        for (GameInfo gi : listGames) {
            if(
                    gi.getGameStatus().equals(GameStatus.Going) ||
                    gi.getGameStatus().equals(GameStatus.Challenged) ||
                    gi.getGameStatus().equals(GameStatus.ChallengeReceived)
            ) {
                String currentPlayer = gi.getCurrentPlayer();
                if(gi.getCurrentPlayer() == null) currentPlayer = gi.getOpponent();

                JFXButton button = UI.Button("Ga naar lobby...");

                if(gi.getGameStatus().equals(GameStatus.Passed)) {
                    button.setText(String.format("%s heeft afgewezen", currentPlayer));
                } else if(gi.getGameStatus().equals(GameStatus.Challenged)) {
                    button.setText("Wachten op acceptatie");
                    button.setDisable(true);
                } else if(gi.getGameStatus().equals(GameStatus.ChallengeReceived)){
                    button.setText("Uitdaging accepteren?");

                    Platform.runLater(() -> {
                        Alert alert = new Alert(Alert.AlertType.CONFIRMATION);
                        alert.setTitle("Uitdaging");
                        alert.setHeaderText(String.format("Speler %s heeft u uitgedaagd!", gi.getOpponent()));
                        alert.setContentText("Uitdaging accepteren?");

                        ButtonType buttonTypeOne = new ButtonType("Ja");
                        ButtonType buttonTypeTwo = new ButtonType("Nee");

                        alert.getButtonTypes().setAll(buttonTypeOne, buttonTypeTwo, ButtonType.CANCEL);

                        button.setOnAction(event -> {
                            Optional<ButtonType> result = alert.showAndWait();
                            new Thread(() -> {
                                try {
                                    if (result.get() == buttonTypeOne) {
                                        GameRepository.updateStatus(gi.getGameId(), GameStatus.Going);

                                        GameRepository.createRound(gi.getGameId(), GameRound.R369);
                                    } else if(result.get() == buttonTypeTwo) {
                                        GameRepository.updateStatus(gi.getGameId(), GameStatus.Passed);
                                    }
                                } catch (SQLException e) {
                                    e.printStackTrace();
                                }
                                Platform.runLater(this::reloadTables);
                            }).start();
                        });
                    });
                } else {
                    button.setOnAction(event -> {
                        RunTimeSettings.gameId = gi.getGameId();

                        LobbyController lobbyController = new LobbyController();
                        RunTimeSettings.mainStager.addSceneController(lobbyController);
                        try {
                            Navigator.show("MainStage", lobbyController.getName());
                        } catch (MissingException e) {
                            e.printStackTrace();
                        }
                    });
                }
                gamesList.add(new GameList(gi.getOpponent(), gi.getGameStatus().value.replace("_", " "), button));
            }
        }
        return gamesList;
    }

    public ObservableList<InviteList> getPlayersData() {
        ObservableList<InviteList> inviteList = FXCollections.observableArrayList();

        getPlayersList();

        for (PlayerGameStatus pgs : listPlayers) {
            String opponent = pgs.getName();
            GameStatus gamestatus = pgs.getGameStatus();

            JFXButton inviteButton = UI.Button(String.format("Daag %s uit", opponent));

            if(gamestatus == null) {
                gamestatus = GameStatus.NoGame;
            }

            if(
                    gamestatus.equals(GameStatus.Challenged) ||
                    gamestatus.equals(GameStatus.ChallengeReceived) ||
                    gamestatus.equals(GameStatus.Going)
            ) {
                inviteButton.setText("Al een spel!");
                inviteButton.setDisable(true);
            } else {
                inviteButton.setDisable(false);
                inviteButton.setOnAction(event -> new Thread(() -> {
                    inviteButton.setDisable(true);
                    Platform.runLater(this::reloadPlayers);
                    try {
                        GameRepository.createNewGame(user, opponent);
                    } catch (SQLException e) {
                        e.printStackTrace();
                    }
                    inviteButton.setDisable(false);
                    Platform.runLater(this::reloadTables);
                }).start());
            }
            if(!inviteButton.isDisable()) {
                inviteList.add(0, new InviteList(opponent, inviteButton));
            } else {
                inviteList.add(new InviteList(opponent, inviteButton));
            }
        }
        return inviteList;
    }

    public void reloadGames() {
        new Thread(() -> {
            ObservableList<GameList> games = getGamesData();
            Platform.runLater(() -> {
                this.view.tvGames.setItems(games);
                this.view.tvGames.refresh();
            });
        }).start();
    }

    public void reloadPlayers() {
        new Thread(() -> {
            ObservableList<InviteList> players = getPlayersData();
            Platform.runLater(() -> {
                this.view.tvPlayers.setItems(players);
                this.view.tvPlayers.refresh();
            });
        }).start();
    }

    public void reloadTables() {
        reloadGames();
        reloadPlayers();
    }

	@Override
	public SceneView getView() {
		return view;
	}

	@Override
	public String getName() {
		return "Start";
	}

    @Override
	public void onEnter() {
	    this.gameListener = new GameListener(this.user) {
		    @Override
		    public void gameChanged(GameInfo game) {
			    Platform.runLater(() -> reloadTables());
		    }
	    };
    }

	@Override
	public void onLeave() {
		this.gameListener.stop();
	}
}
