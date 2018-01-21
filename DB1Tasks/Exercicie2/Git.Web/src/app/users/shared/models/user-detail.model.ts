import { User } from './user.model';

export class UserDetail extends User {
  html_url: string;
  created_at: Date;
}
