import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomerService } from '../../../../_services/customer.service';

@Component({
  selector: 'app-addcustomer',
  templateUrl: './addcustomer.component.html',
  styleUrls: ['./addcustomer.component.scss']
})

export class AddcustomerComponent implements OnInit {  
  
  empformlabel: string = 'Add Customer';  
  empformbtn: string = 'Save';  
  constructor(private formBuilder: FormBuilder, private router: Router, private Service: CustomerService) {  
  }  
  
  addForm: FormGroup;  
  btnvisibility: boolean = true;  
  ngOnInit() {  
   
    }  

    numberOnly(event): boolean {
      const charCode = (event.which) ? event.which : event.keyCode;
      if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
      }
      return true;
  
    }
   
}  