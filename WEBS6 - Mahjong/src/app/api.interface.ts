import {Game} from './models/game';
import {GameTemplate} from './models/game-template';
import {Tile} from './models/tile';
import 'rxjs/add/operator/toPromise';

export interface Api {

  host: string;

  games: Game[];

  gameTemplates: GameTemplate[];

  email: string;

  token: string;

  loginUrl: string;

  isLoggedIn: boolean;

  logout(): void;

  ////////////
  // Templates

  getTemplates(): Promise<GameTemplate[]>;

  ////////
  // Games

  getGames(): Promise<Game[]>;

  createGame(template: string, minPlayers: number, maxPlayers: number): Promise<Game>;

  getGame(gameId: string): Promise<Game>;

  deleteGame(game: Game): Promise<boolean>;

  startGame(game: Game): Promise<boolean>;

  joinGame(game: Game): Promise<boolean>;

  leaveGame(game: Game): Promise<boolean>;

  ////////
  // Tiles

  gameTiles(gameId: string): Promise<Tile[]>;

  matchTiles(gameId: string, tile1Id: string, tile2Id: string): Promise<boolean>;
}
