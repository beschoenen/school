import {Component, OnInit, ViewContainerRef} from '@angular/core';
import {Game} from 'app/models/game';
import {GameTemplate} from 'app/models/game-template';
import {Router} from '@angular/router';
import {Tile} from '../models/tile';
import {ToastsManager} from 'ng2-toastr';
import {ApiService} from '../api.service';

@Component({
  selector: 'app-game-new',
  templateUrl: './game-new.component.html',
  styleUrls: ['./game-new.component.scss']
})
export class GameNewComponent implements OnInit {

  model: Game;

  tiles: Tile[] = null;

  gameTemplates: GameTemplate[] = [];

  constructor(private api: ApiService, private router: Router, public toastr: ToastsManager, vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit(): void {
    this.newGame();

    this.api.getTemplates().then(templates => {
      this.gameTemplates = templates;
      this.newGame();
    });
  }

  onSubmit() {
    this.api.createGame(this.model.gameTemplate.id, this.model.minPlayers, this.model.maxPlayers)
      .then(() => this.router.navigate(['games']))
      .catch(error => this.toastr.error(JSON.parse(error._body).message));
  }

  onChange(newValue: any) {
    const template = this.gameTemplates.find(item => item.id === newValue);

    this.tiles = [];
    template.tiles.forEach(item => this.tiles.push(Tile.fromJson(item)));
  }

  newGame() {
    this.tiles = null;

    this.model = new Game();
    this.model.minPlayers = 1;

    if (this.gameTemplates.length > 0) {
      this.model.gameTemplate.id = this.gameTemplates[0].id;
      this.onChange(this.gameTemplates[0].id);
    }
  }

}
