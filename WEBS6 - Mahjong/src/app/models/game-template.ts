export class GameTemplate {
  id: string;
  tiles: [{
    xPos: number;
    yPos: number;
    zPos: number;
  }];

  public static fromJson(object) {
    const model = new this();

    for (const prop in object) {
      if (object.hasOwnProperty(prop)) {
        model[prop] = object[prop];
      }
    }

    return model;
  }
}
