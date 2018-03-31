import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Game} from '../models/game';
import {ApiService} from '../api.service';

@Component({
  selector: 'app-game-button',
  templateUrl: './game-button.component.html'
})
export class GameButtonComponent {

  @Input() game: Game;

  @Output() error: EventEmitter<string> = new EventEmitter();

  constructor(private api: ApiService) {
  }

  get buttonText() {
    if (!this.inGame()) {
      if (this.game.maxPlayers <= this.game.players.length) {
        return null;
      }

      return 'Join Game!';
    }

    if (!this.ownerOfGame()) {
      return 'Leave Game!';
    }

    return 'Start Game!';
  }

  doAction() {
    if (!this.inGame()) {
      if (this.game.maxPlayers <= this.game.players.length) {
        return;
      }

      return this.joinGame();
    }

    if (!this.ownerOfGame()) {
      return this.leaveGame();
    }

    return this.startGame();
  }

  private inGame() {
    return this.game.players.map(item => item._id).indexOf(this.api.email) > -1;
  }

  private ownerOfGame() {
    return this.game.createdBy._id === this.api.email;
  }

  private leaveGame() {
    this.api.leaveGame(this.game).catch(error => {
      this.error.emit(JSON.parse(error._body).message);
    });
  }

  private joinGame() {
    this.api.joinGame(this.game).catch(error => {
      this.error.emit(JSON.parse(error._body).message);
    });
  }

  private startGame() {
    this.api.startGame(this.game).catch(error => {
      this.error.emit(JSON.parse(error._body).message);
    });
  }

}
