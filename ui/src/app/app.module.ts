import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { EmployeeComponent } from './employee/employee.component';
import { ShowEmpComponent } from './employee/show-emp/show-emp.component';
import { AddEditEmpComponent } from './employee/add-edit-emp/add-edit-emp.component';
import { PetComponent } from './pet/pet.component';
import { ShowPetComponent } from './pet/show-pet/show-pet.component';
import { AddEditPetComponent } from './pet/add-edit-pet/add-edit-pet.component';
import { SharedService } from './shared.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  {path:'pet', component:PetComponent},
  {path:'employee', component:EmployeeComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    EmployeeComponent,
    ShowEmpComponent,
    AddEditEmpComponent,
    PetComponent,
    ShowPetComponent,
    AddEditPetComponent
  ],
  imports: [
    BrowserModule,
	HttpClientModule,
	FormsModule,
	ReactiveFormsModule,
	RouterModule.forRoot(routes)
  ],
  providers: [SharedService],
  bootstrap: [AppComponent]
})
export class AppModule { }
