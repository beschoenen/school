import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Tile} from '../models/tile';
import {TileComponent} from '../tile/tile.component';

@Component({
  selector: 'app-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.scss']
})
export class GameBoardComponent {

  private selected: TileComponent[] = [];

  @Input() tiles: Tile[];
  @Input() viewOnly = false;

  @Output() tilesMatched: EventEmitter<Tile[]> = new EventEmitter();
  @Output() error: EventEmitter<string> = new EventEmitter();

  isSelected(component: TileComponent): Boolean {
    return this.selected.find(item => item.tile._id === component.tile._id) !== undefined;
  }

  select(component: TileComponent) {
    if (!this.isSelected(component)) {
      component.isSelected = true;
      this.selected.push(component);
    } else {
      return this.unSelect();
    }

    if (this.selected.length === 2) {
      if (this.isMatch(this.selected[0].tile, this.selected[1].tile)) {
        this.tilesMatched.emit(this.selected.map(item => item.tile));
      } else {
        this.error.emit('These tiles are not a match!');
      }

      this.unSelect();
    }
  }

  isMatch(tileOne: Tile, tileTwo: Tile): boolean {
    return tileOne.tile.suit === tileTwo.tile.suit &&
      (tileOne.tile.matchesWholeSuit ? true : tileOne.tile.name === tileTwo.tile.name);
  }

  unSelect() {
    this.selected.forEach(item => item.isSelected = false);
    this.selected = [];
  }

  canSelect(component: TileComponent) {
    if (this.can(component.tile)) {
      this.select(component);
    }
  }

  can(tile: Tile): boolean {
    let can = true;

    let hasRight = false;
    let hasLeft = false;

    const tiles = this.tiles
      .filter(item => !item.isMatched && item._id !== tile._id)
      .filter(item => [tile.zPos, tile.zPos + 1].indexOf(item.zPos) > -1)
      .filter(item => [tile.yPos - 1, tile.yPos, tile.yPos + 1].indexOf(item.yPos) > -1)
      .filter(item => [tile.xPos - 2, tile.xPos - 1, tile.xPos, tile.xPos + 1, tile.xPos + 2].indexOf(item.xPos) > -1);

    tiles.forEach(item => {
      if (item.zPos === tile.zPos) {
        if (item.xPos === tile.xPos + 2) {
          hasRight = true;
        } else if (item.xPos === tile.xPos - 2) {
          hasLeft = true;
        }

        if (hasRight && hasLeft) {
          can = false;
          return false;
        }
      } else if (item.zPos === tile.zPos + 1) {
        if ([tile.yPos - 1, tile.yPos, tile.yPos + 1].indexOf(item.yPos) > -1 &&
          [tile.xPos - 1, tile.xPos, tile.xPos + 1].indexOf(item.xPos) > -1) {
          can = false;
          return false;
        }
      }
    });

    return can;
  }

  hint() {
    const tiles = this.tiles.filter(tile => !tile.isMatched && this.can(tile));
    const possibilities: Tile[] = [];

    tiles.forEach(item => {
      const i = tiles.find(tile => tile.tile._id !== item.tile._id && this.isMatch(tile, item));
      if (!!i) {
        possibilities.push(item);
      }
    });

    return possibilities[Math.floor(Math.random() * possibilities.length + 1)];
  }

}
