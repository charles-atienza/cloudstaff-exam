import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { UserApiService } from '../user-api.service';

@Component({
  selector: 'app-userpage',
  templateUrl: './userpage.component.html',
  styleUrls: ['./userpage.component.css']
})
export class UserpageComponent implements OnInit {
  public static userInfo:any = {} 

  @Output()
  toggleLogin = new EventEmitter<boolean>();

  public get userPin(): string {
    return UserpageComponent.userInfo?.pin;
  }

  public get userBalance() : number {
    return UserpageComponent.userInfo?.balance;
  }
  
  amountToCredit: number = 0;
  amountToDebit: number = 0;

  constructor(private userApi: UserApiService) { }

  ngOnInit(): void {
  }

  debitBalance() {
    //call api
    this.userApi.debitBalance(UserpageComponent.userInfo.userId, this.amountToDebit).subscribe(
      res => {
        UserpageComponent.userInfo = res;
      },
      err => {
        console.error(err);
      }
    )
  }

  creditBalance() {
    //call api
    this.userApi.creditBalance(UserpageComponent.userInfo.userId, this.amountToCredit).subscribe(
      res => {
        UserpageComponent.userInfo = res;
      },
      err => {
        console.error(err);
      }
    );
  }

  logoutUser() {
    this.toggleLogin.emit();
  }
}
