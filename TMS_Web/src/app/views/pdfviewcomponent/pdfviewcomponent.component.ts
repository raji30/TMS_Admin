import { Component, OnInit, Input } from "@angular/core";
import { PdfService } from "./pdf.service";
import { AppSettings } from "../../_constants/appsettings";
import { HttpHeaders } from "@angular/common/http";
import { DomSanitizer, SafeResourceUrl, SafeUrl } from '@angular/platform-browser';
@Component({
  selector: "app-pdfviewcomponent",
  templateUrl: "./pdfviewcomponent.component.html",
  styleUrls: ["./pdfviewcomponent.component.scss"],
  providers: [PdfService]
})
export class PdfviewcomponentComponent implements OnInit {
  page: number = 1;
  @Input() pdfSrc: any;
  test: string;
  constructor(private pdfService: PdfService,private sanitizer: DomSanitizer) {}

  ngOnInit() { 

    // this.pdfSrc = "https://vadimdez.github.io/ng2-pdf-viewer/assets/pdf-test.pdf";
    // const httpOptions = {
    //   headers: new HttpHeaders({
    //     "Content-Type": "application/json",
    //     Authorization: "Bearer " + token.token
    //   })
    // };
   // var token = JSON.parse(localStorage.getItem("currentUser"));
    //this.pdfSrc = this.sanitizer.bypassSecurityTrustResourceUrl(AppSettings._BaseURL + "App_Data/Files/DOJO200001/FORM16_2018.pdf");
    //this.pdfSrc = (AppSettings._BaseURL + "App_Data/Files/DOJO200001/FORM16_2018.pdf");

//  console.log("URL:", this.pdfSrc );

//     const headers = new HttpHeaders({
//       "Content-Type": "application/json",
//       "Access-Control-Allow-Origin": '*',
//       "Authorization": "Bearer " + token.token
//     });

    // this.pdfSrc = {
    //   url: this.sanitizer.bypassSecurityTrustResourceUrl(AppSettings._BaseURL + "App_Data/Files/DOJO200001/Name Script.pdf")      ,         
    //   httpHeaders: headers ,
    // };
   
  }
  

  onFileSelected() {
    let $img: any = document.querySelector('#file');
  
    if (typeof (FileReader) !== 'undefined') {
      let reader = new FileReader();
  
      reader.onload = (e: any) => {
        this.pdfSrc = e.target.result;
        console.log("e.target.result",e.target.result);
        console.log("this.pdfSrc",this.pdfSrc);

      };
  
     reader.readAsArrayBuffer($img.files[0]);
    }
  }
}
