import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Game} from '../models/game';

@Component({
  selector: 'app-game-filter',
  templateUrl: './game-filter.component.html',
  styleUrls: ['./game-filter.component.scss']
})
export class GameFilterComponent {

  gameStates: string[] = [];
  gameTemplates: string[] = [];
  me: string[] = [];

  @Input() games: Game[];

  @Output() filtered: EventEmitter<any> = new EventEmitter();

  toggleOn(list: string, item: string) {
    switch (list) {
      case 'me':
        this.toggle(this.me, item);
        break;
      case 'states':
        this.toggle(this.gameStates, item);
        break;
      case 'templates':
        this.toggle(this.gameTemplates, item);
        break;
    }

    this.filtered.emit([this.gameStates, this.gameTemplates, this.me]);
  }

  toggle(collection: any, item: string) {
    const idx = collection.indexOf(item);
    if (idx !== -1) {
      collection.splice(idx, 1);
    } else {
      collection.push(item);
    }
  }

}
