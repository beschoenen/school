export class User {
  _id: string;
  name: string;
  numberOfMatches: number;

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
