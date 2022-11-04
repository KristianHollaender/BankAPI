import { Component, OnInit } from '@angular/core';
import {customAxios, HttpService} from "../../services/http.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Router, RouterLink} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  email: string = '';
  password: string = '';

  constructor(private http: HttpService, private matSnackBar: MatSnackBar, private router: Router) {
    customAxios.interceptors.response.use(
      rejected => {
        if (rejected.status >= 400 && rejected.status < 500) {
          this.matSnackBar.open("Something went wrong")
        }
        return rejected;
      }
    )
  }

  ngOnInit(): void {
  }

  async login() {
    let dto = {
      email: this.email,
      password: this.password
    }
    this.http.login(dto).then(token => {
      console.log(token);
      localStorage.setItem('token', token)
      this.router.navigate(['./customers']);
    })

  }

}
