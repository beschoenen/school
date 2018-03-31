import {User} from './user';
import {GameTemplate} from './game-template';
import {Tile} from './tile';

export class Game {
  _id: string;
  gameTemplate: GameTemplate;
  createdOn: string;
  startedOn: string;
  endedOn: string;
  createdBy: User;
  minPlayers: number;
  maxPlayers: number;
  players: User[];
  state: string;

  tiles: Tile[];

  public static fromJson(object) {
    const model = new this();

    for (const prop in object) {
      if (object.hasOwnProperty(prop)) {
        if (prop === 'tiles') {
          model.tiles = object[prop].map(tile => Tile.fromJson(tile));
        } else if (prop === 'players') {
          model.players = object[prop].map(player => User.fromJson(player));
        } else {
          model[prop] = object[prop];
        }
      }
    }

    return model;
  }

  constructor() {
    this.gameTemplate = new GameTemplate();
    this.players = [];
    this.tiles = [];
  }

  public updateFromJson(object) {
    for (const prop in object) {
      if (object.hasOwnProperty(prop)) {
        this[prop] = object[prop];
      }
    }
  }
}
