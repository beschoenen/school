import {Pipe, PipeTransform} from '@angular/core';
import {Game} from '../models/game';
import {ApiService} from '../api.service';

@Pipe({
  name: 'gameFilter'
})
export class GameFilterPipe implements PipeTransform {

  constructor(private api: ApiService) {
  }

  transform(games: Game[], args?: any): Game[] {
    if (games === undefined) {
      return [];
    }

    let filteredGames: Game[] = games.slice(0);

    filteredGames = this.filterGameState(filteredGames, args[0] || []);
    filteredGames = this.filterGameTemplates(filteredGames, args[1] || []);
    filteredGames = this.filterMe(filteredGames, args[2] || []);

    return filteredGames;
  }

  filterGameState(games: Game[], states: string[]): Game[] {
    if (states.length < 1) {
      return games;
    }

    return games.filter(game => states.indexOf(game.state) > -1);
  }

  filterGameTemplates(games: Game[], templates: string[]): Game[] {
    if (templates.length < 1) {
      return games;
    }

    return games.filter(game => templates.indexOf(game.gameTemplate.id) > -1);
  }

  filterMe(games: Game[], me: string[]): Game[] {
    if (me.length < 1) {
      return games;
    }

    const list = [];

    me.forEach(item => games.forEach(game => {
      if (item === 'created' && game.createdBy._id === this.api.email) {
        list.push(game);
      } else if (item === 'joined' && game.players.map(player => player._id).indexOf(this.api.email) > -1) {
        list.push(game);
      }
    }));

    return list;
  }

}
