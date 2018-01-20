import { Component, OnInit, OnDestroy } from '@angular/core';
import { UsersService } from './shared/services/users.service';
import { User } from './shared/models/user.model';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit, OnDestroy {
  public model: User[];
  private serviceInscription: Subscription;

  constructor(private service: UsersService) {}

  ngOnInit() {
    this.serviceInscription = this.service
      .getAllUsers()
      .subscribe(res => (this.model = res));
  }

  ngOnDestroy() {
    this.serviceInscription.unsubscribe();
  }
}
