import {Component, OnInit} from '@angular/core';
import {Game} from 'app/models/game';
import {Router} from '@angular/router';
import {ApiService} from '../api.service';

@Component({
  selector: 'app-game-list',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.scss']
})
export class GameListComponent implements OnInit {

  games: Game[];

  filters = [];

  constructor(private api: ApiService, private router: Router) {
  }

  ngOnInit(): void {
    this.api.getGames().then(games => this.games = games);
  }

  lobby(game: Game) {
    this.router.navigate([`games/${game._id}/lobby`]);
  }

  setFilter(filters: any) {
    this.filters = filters;
  }

}
