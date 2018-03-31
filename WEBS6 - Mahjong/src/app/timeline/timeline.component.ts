import {Component, Input, OnChanges, SimpleChanges} from '@angular/core';
import {Tile} from '../models/tile';

@Component({
  selector: 'app-timeline',
  templateUrl: './timeline.component.html',
  styleUrls: ['./timeline.component.scss']
})
export class TimelineComponent implements OnChanges {

  private currentTime = 0;

  private first = new Date(0);
  private last = new Date(0);

  @Input() tiles: Tile[];

  ngOnChanges(changes: SimpleChanges) {
    const tiles = changes['tiles'].currentValue;

    tiles.filter(tile => tile.isMatched).forEach(tile => {
      const date = new Date(tile.match.foundOn);

      if (this.last.getFullYear() <= 1970 || this.last < date) {
        this.last = date;
        this.currentTime = this.last.getTime();
      } else if (this.first.getFullYear() <= 1970 || this.first > date) {
        this.first = date;
      }
    });
  }

  historyChange() {
    this.tiles.forEach(tile => {
      tile.shownAtTime = (this.last.getTime() === this.currentTime ? null : this.currentTime);
    });
  }

}
