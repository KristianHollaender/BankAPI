import { Component, OnInit } from '@angular/core';
import {customAxios, HttpService} from "../../services/http.service";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  accountName: string = '';
  amount?: number;
  customerId?: number;
  accounts: any;
  chosenValue!: customer;
  customers: any[] = [];

  constructor(private http: HttpService, private matSnackBar: MatSnackBar) {
    customAxios.interceptors.response.use(
      response => {
        if (response.status == 201) {
          this.matSnackBar.open("You've successfully created an account!", undefined, {duration: 3000})
        }
        return response;
      }, rejected => {
        if (rejected.response.status >= 400 && rejected.response.status < 500) {
          this.matSnackBar.open("Something went wrong, contact admin :-)", "error", {duration: 3000})
        }
        return rejected;
      }
    )
  }

  async ngOnInit() {
    this.accounts = await this.http.getAccounts();
    this.customers = await this.http.getCustomers();
  }

  async createAccount() {
    let dto = {
      accountName: this.accountName,
      amount: this.amount,
      customerId: this.chosenValue.id
    }
    const result = await this.http.createAccount(dto);
    this.accounts.push(result);
  }

  async deleteAccount(id: any) {
    if (confirm('Are you sure you want the delete the account with ID: ' + id + '?')) {
      const account = await this.http.deleteAccount(id);
      this.accounts = this.accounts.filter((a: { id: any; }) => a.id != account.id);
    }
  }
}

interface customer {
  id: number;
}
