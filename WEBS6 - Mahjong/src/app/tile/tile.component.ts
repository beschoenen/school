import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Tile} from '../models/tile';

@Component({
  selector: 'app-tile',
  templateUrl: './tile.component.html',
  styleUrls: ['./tile.component.scss']
})
export class TileComponent implements OnInit {

  private width = 40;
  private height = 55;

  private top: number;
  private left: number;
  private background: string;

  isSelected = false;

  @Input() tile: Tile;
  @Input() clickable: boolean;

  @Output() clicked: EventEmitter<TileComponent> = new EventEmitter();

  ngOnInit() {
    this.background = `${this.tile.tile.suit}-${this.tile.tile.name}`.toLowerCase();

    this.top = (this.tile.yPos * (this.height / 2)) - (5 * this.tile.zPos);
    this.left = (this.tile.xPos * (this.width / 2)) + (5 * this.tile.zPos);
  }

  canSelect() {
    if (!this.clickable) {
      return;
    }

    this.clicked.emit(this);
  }

}
