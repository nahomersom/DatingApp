import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/users';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  model:any = {};

  constructor(public accountService:AccountService) { }

  ngOnInit(): void {
   
  }
 login(){
 this.accountService.login(this.model).subscribe(
    {
  next:(res)=>{
    console.log(res);
    
  },
  error:(err)=>{
   
  }
  }
 )
}
logout(){
  this.accountService.logout();
  
}


}
