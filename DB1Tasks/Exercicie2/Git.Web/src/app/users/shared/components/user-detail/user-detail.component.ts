import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersService } from '../../services/users.service';
import { UserDetail } from '../../models/user-detail.model';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit, OnDestroy {
  public model: UserDetail;
  private serviceInscription: Subscription;
  private routeInscription: Subscription;

  constructor(
    private service: UsersService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.routeInscription = this.route.params.subscribe((params: any) => {
      const user = params['user'];
      this.serviceInscription = this.service
        .getUser(user)
        .subscribe(res => (this.model = res));
    });
  }

  ngOnDestroy() {
    this.serviceInscription.unsubscribe();
    this.routeInscription.unsubscribe();
  }
}
