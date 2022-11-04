import {Component, OnInit} from '@angular/core';
import {customAxios, HttpService} from "../../services/http.service";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {

  customerFirstName: string = '';
  customerLastName: string = '';

  customers: any;

  constructor(private http: HttpService, private matSnackBar: MatSnackBar) {
    customAxios.interceptors.response.use(
      response => {
        if (response.status == 201) {
          this.matSnackBar.open("You've successfully created a customer!")
        }
        return response;
      }, rejected => {
        if (rejected.response.status >= 400 && rejected.response.status < 500) {
          this.matSnackBar.open("Something went wrong, contact admin :-)")
        }
        return rejected;
      }
    )
  }

  async ngOnInit() {
    this.customers = await this.http.getCustomers();
  }

  async createCustomer() {
    let dto = {
      firstName: this.customerFirstName,
      lastName: this.customerLastName
    }
    const result = await this.http.createCustomer(dto);
    this.customers.push(result)
  }

  writeCustomerName() {
    console.log(this.customerFirstName);
    console.log(this.customerLastName);
  }

  async deleteCustomer(id: any) {
    if(confirm('Are you sure you want to delete the customer with ID: ' + id + '?')) {
      const customer = await this.http.deleteCustomer(id);
      this.customers = this.customers.filter((c: { id: any; }) => c.id != customer.id);
    }
  }
}
