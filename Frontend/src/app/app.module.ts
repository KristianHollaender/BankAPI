import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatFormFieldModule} from "@angular/material/form-field";
import {FormsModule} from "@angular/forms";
import {MatInputModule} from "@angular/material/input";
import {MatButtonModule} from "@angular/material/button";
import {MatCardModule} from "@angular/material/card";
import {MatButtonToggleModule} from "@angular/material/button-toggle";
import { LoginComponent } from './login/login.component';
import { CustomerComponent } from './customer/customer.component';
import {RouterModule, RouterOutlet, Routes} from "@angular/router";
import {AuthguardService} from "../services/authguard.service";
import { RegisterComponent } from './register/register.component';
import {MatSelectModule} from "@angular/material/select";
import { AccountComponent } from './account/account.component';
import {MatSnackBar} from "@angular/material/snack-bar";
import {Overlay} from "@angular/cdk/overlay";

const routes: Routes = [
  {path: 'customers', component: CustomerComponent, canActivate: [AuthguardService]},
  {path: 'accounts', component: AccountComponent, canActivate: [AuthguardService]},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: '**', redirectTo: 'login'}
]

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CustomerComponent,
    RegisterComponent,
    AccountComponent,
  ],
  imports: [
    RouterModule.forRoot(routes),
    BrowserModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    FormsModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatButtonToggleModule,
    RouterOutlet,
    MatSelectModule
  ],
  providers: [MatSnackBar, Overlay],
  bootstrap: [AppComponent]
})
export class AppModule { }
