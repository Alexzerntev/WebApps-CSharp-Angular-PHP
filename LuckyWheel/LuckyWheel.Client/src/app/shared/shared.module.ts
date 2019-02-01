import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { BlankComponent } from './blank/blank.component';


@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [BlankComponent],
  exports: [
    FormsModule,
    MDBBootstrapModule,
    CommonModule
  ]

})
export class SharedModule { }
