import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PdfService {

constructor() { }
getPDF():string {
  return 'C:/Users/Arun/Documents/GitHub/TMS_Admin/TMS.Api/App_Data/Files/DOJO200001/FORM16_2018.pdf';
}
}
