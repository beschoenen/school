export class Tile {
  _id: string;
  xPos: number;
  yPos: number;
  zPos: number;
  tile: {
    _id: number;
    suit: string;
    name: string;
    matchesWholeSuit: boolean;
  };
  match: {
    foundBy: string,
    otherTileId: string,
    foundOn: Date
  };

  isMatched: boolean;

  forceShown = null;
  shownAtTime = null;

  public static fromJson(object) {
    const model = new this();

    for (const prop in object) {
      if (object.hasOwnProperty(prop)) {
        model[prop] = object[prop];

        if (prop === 'match') {
          model.isMatched = true;
        }
      }
    }

    // Set dummy tile for game creation view
    if (model.tile === undefined) {
      model.tile = {
        _id: 0,
        suit: 'bamboo',
        name: '1',
        matchesWholeSuit: false
      };
    }

    return model;
  }

  constructor() {
    this.isMatched = false;
  }

  get display() {
    if (this.shownAtTime !== null) {
      const force = this.forceShown === null ? true : !!this.forceShown;

      if (this.isMatched) {
        return force && new Date(this.match.foundOn) > new Date(this.shownAtTime) ? 'block' : 'none';
      }
    }

    if (this.forceShown !== null) {
      return this.forceShown ? 'block' : 'none';
    }

    return this.isMatched ? 'none' : 'block';
  }
}
