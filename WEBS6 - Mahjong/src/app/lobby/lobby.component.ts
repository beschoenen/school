import {Component, OnDestroy, OnInit, ViewChild, ViewContainerRef} from '@angular/core';
import {ActivatedRoute, Params} from '@angular/router';
import {Game} from '../models/game';
import {ToastsManager} from 'ng2-toastr';
import * as io from 'socket.io-client';
import {Tile} from '../models/tile';
import {User} from '../models/user';
import {GameBoardComponent} from '../game-board/game-board.component';
import {ApiService} from '../api.service';

@Component({
  selector: 'app-lobby',
  templateUrl: './lobby.component.html',
  styleUrls: ['./lobby.component.scss']
})
export class LobbyComponent implements OnInit, OnDestroy {

  private socket;
  private canPlay = false;
  private hintAvailable = true;

  game: Game;

  @ViewChild(GameBoardComponent) gameBoard;

  constructor(private api: ApiService, private route: ActivatedRoute, public toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  get canShowHistory() {
    return this.game.state === 'finished' && !this.canPlay;
  }

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.api.getGame(params.id).then(game => {
        this.game = game;
        this.openSocket();

        if (this.game.state === 'playing') {
          this.gameStarted(this.game);
        } else if (this.game.state === 'finished') {
          this.gameEnded(this.game);
        }
      });
    });
  }

  ngOnDestroy() {
    this.socket.disconnect();
  }

  private openSocket() {
    this.socket = io(`${this.api.host}?gameId=${this.game._id}`);

    this.socket.on('playerJoined', data => this.game.players.push(User.fromJson(data)));
    this.socket.on('match', data => this.matchFound(this.game, data.map(tile => Tile.fromJson(tile))));
    this.socket.on('start', () => this.gameStarted(this.game));
    this.socket.on('end', () => this.gameEnded(this.game));
  }

  /////////////////
  // Socket methods

  matchFound(game: Game, tiles: Tile[]) {
    let email;

    tiles.forEach(tile => {
      const found = game.tiles.find(_tile => _tile._id === tile._id);

      found.isMatched = true;
      found.match = tile.match;

      email = tile.match.foundBy;
    });

    const name = game.players.find(user => user._id === email);
    if (email && email !== this.api.email && name) {
      this.toastr.info('Matched Tiles!', name.name);
    } else {
      this.toastr.success('Tiles matched!', 'You');
    }
  }

  inGame() {
    return this.game.players.map(item => item._id).indexOf(this.api.email) > -1;
  }

  gameStarted(game: Game) {
    game.state = 'playing';
    this.api.gameTiles(game._id).then(tiles => game.tiles = tiles);
    this.canPlay = this.inGame();
  }

  gameEnded(game: Game) {
    game.state = 'finished';

    if (game.tiles.length < 1) {
      this.api.gameTiles(game._id).then(tiles => game.tiles = tiles);
    }

    this.socket.disconnect();
    this.canPlay = false;
  }

  hint() {
    const hint = this.gameBoard.hint();
    if (hint) {
      this.toastr.info(`${hint.tile.suit} ${hint.tile.name}`);
      this.hintAvailable = false;
      setInterval(() => this.hintAvailable = true, 15000);
    }
  }

  /////////////////
  // Event Handlers

  onMatched(tiles: Tile[]) {
    this.api.matchTiles(this.game._id, tiles[0]._id, tiles[1]._id).catch(error => {
      this.toastr.error(JSON.parse(error._body).message);
    });
  }

  usersChecked(users: User[]) {
    if (users.length === 0) {
      return this.game.tiles.forEach(tile => tile.forceShown = null);
    }

    this.game.tiles.forEach(tile => {
      tile.forceShown = tile.match && users.map(user => user._id).indexOf(tile.match.foundBy) > -1;
    });
  }

  showError(message: string) {
    this.toastr.error(message);
  }

}
