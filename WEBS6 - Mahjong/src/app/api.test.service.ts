import {Api} from './api.interface';
import {Tile} from './models/tile';
import {Game} from './models/game';
import {GameTemplate} from './models/game-template';

export class ApiTestService implements Api {

  host = 'http://localhost';

  games: Game[] = [];

  gameTemplates: GameTemplate[] = [];

  email = 'email@test.com';

  token = '123';

  loginUrl = '';

  isLoggedIn = true;

  logout(): void {
    this.isLoggedIn = false;
  }

  getTemplates(): Promise<GameTemplate[]> {
    return new Promise(resolve => resolve([]));
  }

  getGames(): Promise<Game[]> {
    return new Promise(resolve => resolve([]));
  }

  createGame(template: string, minPlayers: number, maxPlayers: number): Promise<Game> {
    return undefined;
  }

  getGame(gameId: string): Promise<Game> {
    return new Promise(resolve => resolve(new Game()));
  }

  deleteGame(game: Game): Promise<boolean> {
    return new Promise(resolve => resolve(true));
  }

  startGame(game: Game): Promise<boolean> {
    return new Promise(resolve => resolve(true));
  }

  joinGame(game: Game): Promise<boolean> {
    return new Promise(resolve => resolve(true));
  }

  leaveGame(game: Game): Promise<boolean> {
    return new Promise(resolve => resolve(true));
  }

  gameTiles(gameId: string): Promise<Tile[]> {
    return new Promise(resolve => resolve([]));
  }

  matchTiles(gameId: string, tile1Id: string, tile2Id: string): Promise<boolean> {
    return new Promise(resolve => resolve(true));
  }

}
