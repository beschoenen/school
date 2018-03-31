import {Inject, Injectable} from '@angular/core';
import {DOCUMENT} from '@angular/platform-browser';
import {Headers, Http, Response} from '@angular/http';
import {Game} from './models/game';
import {GameTemplate} from './models/game-template';
import {Tile} from './models/tile';
import 'rxjs/add/operator/toPromise';
import {Api} from './api.interface';

@Injectable()
export class ApiService implements Api {

  public host = 'http://mahjongmayhem.herokuapp.com';

  public games: Game[] = [];

  public gameTemplates: GameTemplate[] = [];

  constructor(@Inject(DOCUMENT) private document: any, private http: Http) {
  }

  get email(): string {
    return localStorage.getItem('user-email');
  }

  get token(): string {
    return localStorage.getItem('user-token');
  }

  get isLoggedIn(): boolean {
    return !!this.email && !!this.token;
  }

  public logout() {
    localStorage.clear();
  }

  get loginUrl(): string {
    return this.url('auth/avans') + `/?callbackUrl=${this.document.location.origin}/login/callback`;
  }

  private url(route: string): string {
    return `${this.host}/${route}`;
  }

  private headers(): Headers {
    const headers = new Headers();
    headers.set('x-token', this.token);
    headers.set('x-username', this.email);

    return headers;
  }

  private get(route: string): Promise<Response> {
    return this.http.get(this.url(route)).toPromise();
  }

  private post(route: string, body: any = {}): Promise<Response> {
    return this.http.post(this.url(route), body, {
      headers: this.headers()
    }).toPromise();
  }

  private put(route: string, body: any = {}): Promise<Response> {
    return this.http.put(this.url(route), body, {
      headers: this.headers()
    }).toPromise();
  }

  private delete(route: string): Promise<Response> {
    return this.http.delete(this.url(route), {
      headers: this.headers()
    }).toPromise();
  }

  ////////////
  // Templates

  public getTemplates(): Promise<GameTemplate[]> {
    return new Promise((resolve, reject) => {
      if (this.gameTemplates.length > 0) {
        return resolve(this.gameTemplates);
      }

      this.get('gameTemplates').then(response => {
        response.json().forEach(object => this.gameTemplates.push(GameTemplate.fromJson(object)));
        resolve(this.gameTemplates);
      }).catch(reject);
    });
  }

  ////////
  // Games

  public getGames(): Promise<Game[]> {
    return new Promise((resolve, reject) => {
      if (this.games.length > 0) {
        return resolve(this.games);
      }

      this.get('games').then(response => {
        response.json().forEach(object => this.games.push(Game.fromJson(object)));
        resolve(this.games);
      }).catch(reject);
    });
  }

  public createGame(template: string, minPlayers: number, maxPlayers: number): Promise<Game> {
    return new Promise((resolve, reject) => {
      this.post('games', {
        'templateName': template,
        'minPlayers': minPlayers,
        'maxPlayers': maxPlayers
      }).then(response => {
        const game = Game.fromJson(response.json());
        this.games.push(game);

        resolve(game);
      }).catch(reject);
    });
  }

  public getGame(gameId: string): Promise<Game> {
    return new Promise((resolve, reject) => {
      const temp = this.games.find(game => game._id === gameId);

      if (temp) {
        return resolve(temp);
      }

      this.get(`games/${gameId}`).then(response => {
        return resolve(Game.fromJson(response.json()));
      }).catch(reject);
    });
  }

  public deleteGame(game: Game): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.delete(`games/${game._id}`).then(response => {
        console.log(response);
        for (let i = 0; i < this.games.length; i++) {
          if (this.games[i]._id === game._id) {
            this.games.splice(i, 1);
            break;
          }
        }

        resolve();
      }).catch(reject);
    });
  }

  public startGame(game: Game): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.post(`games/${game._id}/start`).then(() => {
        game.state = 'playing';
        return resolve();
      }).catch(reject);
    });
  }

  public joinGame(game: Game): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.post(`games/${game._id}/players`).then(response => {
        this.games.find(_game => _game._id === game._id).updateFromJson(response.json());
        resolve();
      }).catch(reject);
    });
  }

  public leaveGame(game: Game): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.delete(`games/${game._id}/players`).then(response => {
        this.games.find(_game => _game._id === game._id).updateFromJson(response.json());
        resolve();
      }).catch(reject);
    });
  }

  ////////
  // Tiles

  public gameTiles(gameId: string): Promise<Tile[]> {
    return new Promise((resolve, reject) => {
      this.get(`games/${gameId}/tiles`).then(response => {
        const tiles = response.json().map(object => Tile.fromJson(object));
        resolve(tiles);
      }).catch(reject);
    });
  }

  public matchTiles(gameId: string, tile1Id: string, tile2Id: string): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.put(`games/${gameId}/tiles`, {
        tile1Id: tile1Id,
        tile2Id: tile2Id
      }).then(response => {
        resolve(response.json());
      }).catch(reject);
    });
  }
}
