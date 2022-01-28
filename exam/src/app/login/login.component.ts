import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { AppComponent } from '../app.component';
import { UserApiService } from '../user-api.service';
import { UserpageComponent } from '../userpage/userpage.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  @Output()
  toggleLogin = new EventEmitter<boolean>();

  username: string = '';
  password: string = '';

  constructor(private userApi: UserApiService) { }

  ngOnInit(): void {
  }

  async LoginUser() {
    //call backend api

    this.userApi.login(this.username, this.password).subscribe(
      res => {
        if (res?.successful) {
          this.toggleLogin.emit();
          UserpageComponent.userInfo = res.account;
        }
      },
      err => {
        console.error(err);
      }
    )
    // let results = await this.userApi.getUsers().subscribe(
    //   res => {
    //     console.log(res);
    //   },
    //   err => {
    //     console.error(err);
    //   }
    // );
  }

}
