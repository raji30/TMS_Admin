import { Component, OnInit } from "@angular/core";
import { Router, NavigationEnd } from "@angular/router";
import { FiledownloadService } from "./_services/filedownload.service";
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  // tslint:disable-next-line
  selector: "body",
  template: "<router-outlet></router-outlet>",
  providers: [FiledownloadService]
})
export class AppComponent implements OnInit {
  constructor(private router: Router,private SpinnerService: NgxSpinnerService) {
    //Get the user data from users.json
  }

  ngOnInit() {
    this.SpinnerService.show();
    this.router.events.subscribe(evt => {
      if (!(evt instanceof NavigationEnd)) {
        return;
      }
      window.scrollTo(0, 0);
    });
    this.SpinnerService.hide();
  }
}
