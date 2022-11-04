import { Injectable } from '@angular/core';
import axios from "axios";
import {Router} from "@angular/router";

export const customAxios = axios.create({
  baseURL: 'http://localhost:5252',
  headers: {
    Authorization: `Bearer ${localStorage.getItem('token')}`
  }
})

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor() { }

  async getCustomers()
  {
    const httpResponse = await customAxios.get<any>('customer');
    return httpResponse.data;
  }

  async createCustomer(dto: {firstName: any; lastName: any;}) {
    const httpResult = await customAxios.post('customer', dto)
    return httpResult.data;
  }

  async deleteCustomer(id: any) {
    const httpResult = await customAxios.delete('customer/'+id);
    return httpResult.data;
  }

  async getAccounts() {
    const httpResponse = await customAxios.get<any>('account');
    return httpResponse.data;
  }

  async createAccount(dto: {amount: number | undefined; accountName: any; customerId: number | undefined}) {
    const httpResult = await customAxios.post('account', dto);
    return httpResult.data;
  }

  async deleteAccount(id: any) {
    const httpResult = await customAxios.delete('account/'+id);
    return httpResult.data;
  }

  async login(dto: any) {
    const httpResult = await customAxios.post('auth/login', dto);
    return httpResult.data;
  }

  async register(dto: any) {
    const httpResult = await customAxios.post('auth/register', dto);
    return httpResult.data;
  }


}
