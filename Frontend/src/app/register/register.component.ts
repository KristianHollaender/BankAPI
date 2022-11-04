import { Component, OnInit } from '@angular/core';
import {customAxios, HttpService} from "../../services/http.service";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {

  email: string = '';
  password: string = '';
  roles: string[] = [
    'Admin',
    'User'
  ]
  chosenValue: any;

  constructor(private http: HttpService, private matSnackBar: MatSnackBar) {
    customAxios.interceptors.response.use(
      response => {
        if (response.status == 200) {
          this.matSnackBar.open("You've successfully created a login!")
        }
        return response;
      }, rejected => {
        if (rejected.response.status >= 400 && rejected.response.status < 500) {
          this.matSnackBar.open("That email is already taken!")
        }
        return rejected;
      }
    )
  }

  ngOnInit(): void {
  }

  async register() {
    let dto = {
      email: this.email,
      password: this.password,
      role: this.chosenValue
    }
    await this.http.register(dto);
    console.log(dto);
  }
}
