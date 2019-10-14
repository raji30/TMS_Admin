import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppSettings } from '../_constants/appsettings';
import { Item } from '../_models/item';

@Injectable({
  providedIn: 'root'
})
export class ItemService {

constructor(private http:HttpClient) { }

public GetItems() {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };
  
  return this.http.get<Item[]>(AppSettings._BaseURL + 'GetItems');  
}

CreateItem(item: Item) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };
    return this.http.post<Item >(
      AppSettings._BaseURL + "CreateItem",
      item,
      httpOptions
    );
  }

  UpdateItem(item: Item ) {
    var token = JSON.parse(localStorage.getItem("currentUser"));
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.post<Item>(
      AppSettings._BaseURL + "UpdateItem",
      item,
      httpOptions
    );
  }
  GetItemByID(id: string) {
    var token = JSON.parse(localStorage.getItem("currentUser"));

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + token.token
      })
    };

    return this.http.get<Item>(
      AppSettings._BaseURL + "GetItemByID" + "/" + id
    );
  }
}
