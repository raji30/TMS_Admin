import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { first } from "rxjs/operators";
import { AuthenticationService } from "../../_services/authentication.service";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"]
})
export class LoginComponent implements OnInit {
  model: any = {};
  loading = false;
  submitted = false;
  returnUrl: string;
  error = "";
  isContainerAttributeVisible : boolean=true;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService
  ) {}

  ngOnInit() {
    // reset login status
    this.authenticationService.logout();
    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams["returnUrl"] || "/";
  }

  // convenience getter for easy access to form fields

  onSubmit() {
    this.loading = true;
    this.isContainerAttributeVisible = false;
    // stop here if form is invalid

    this.authenticationService
      .login(this.model.username, this.model.password, this.model.company)
      .pipe(first())
      .subscribe(
        data => {     
          this.router.navigate(["dashboard"]);
        },
        error => {
          this.error = "Invalid Credentials!";
          this.loading = false;
        }
      );
  }
}
