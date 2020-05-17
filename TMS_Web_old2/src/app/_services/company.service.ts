import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { AppSettings } from "../_constants/appsettings";
import { Company } from '../_models/company';

@Injectable({
  providedIn: "root"
})
export class CompanyService {
  headers: any;
  constructor(private http: HttpClient) {}

  public getCompanies() {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.get<Company[]>(AppSettings._BaseURL + "GetCompanies");
  }

  createCompany(company: Company) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    return this.http.post<Company>(
      AppSettings._BaseURL + "CreateCompany",
      company,
      httpOptions
    );
  }

  updateCompany(company: Company) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.put<Company>(
      AppSettings._BaseURL + "UpdateCompany",
      company,
      httpOptions
    );
  }
  getCompanyById(id: string) {

    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    
    return this.http.get<Company>(
      AppSettings._BaseURL + "GetCompanyByID" + "/" + id
    );
  }
}
