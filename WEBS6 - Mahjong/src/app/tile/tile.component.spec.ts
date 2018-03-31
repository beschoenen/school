import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {TileComponent} from './tile.component';
import {Tile} from '../models/tile';

describe('TileComponent', () => {
  let component: TileComponent;
  let fixture: ComponentFixture<TileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [TileComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TileComponent);
    component = fixture.componentInstance;
    component.tile = Tile.fromJson({
      'xPos': 7,
      'yPos': 1,
      'zPos': 0,
      'tile': {
        '_id': 107,
        'id': 107,
        'suit': 'Character',
        'name': 9,
        'matchesWholeSuit': false
      },
      '_id': '58c0031dd394b61200f08d40',
      'match': {
        'foundBy': 'your.name@student.avans.nl',
        'otherTileId': '58c0031dd394b61200f08d33',
        'foundOn': '2017-03-08T13:20:42.676Z'
      }
    });
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
