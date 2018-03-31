import {Component, EventEmitter, Input, Output} from '@angular/core';
import {User} from '../models/user';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent {

  private checkedUsers: User[] = [];

  @Input() players: User[];
  @Input() canCheck: boolean;

  @Output() checked: EventEmitter<User[]> = new EventEmitter();

  userChecked(user: User) {
    const index = this.checkedUsers.findIndex(u => u._id === user._id);
    if (index > -1) {
      this.checkedUsers.splice(index, 1);
    } else {
      this.checkedUsers.push(user);
    }

    this.checked.emit(this.checkedUsers);
  }

}
