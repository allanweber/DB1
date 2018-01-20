import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { RepositoriesService } from '../../services/repositories.service';
import { Repository } from '../../models/repository.model';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-user-repositories',
  templateUrl: './user-repositories.component.html',
  styleUrls: ['./user-repositories.component.css'],
  providers: [RepositoriesService]
})
export class UserRepositoriesComponent implements OnInit, OnDestroy {

  @Input() userName: string;
  public model: Repository[];
  private serviceInscription: Subscription;

  constructor(
    private service: RepositoriesService,
  ) {}

  ngOnInit() {
    this.serviceInscription = this.service
        .getUserRepositories(this.userName)
        .subscribe(res => (this.model = res));
  }

  ngOnDestroy() {
    this.serviceInscription.unsubscribe();
  }

}
