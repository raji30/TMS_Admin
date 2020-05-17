import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppSettings } from '../_constants/appsettings';
import { Item } from '../_models/item';
import { ItemType } from '../_models/ItemType';

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

public GetItemTypes() {
  var token = JSON.parse(localStorage.getItem("currentUser"));

  const httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: "Bearer " + token.token
    })
  };
  
  return this.http.get<ItemType[]>(AppSettings._BaseURL + 'GetItemTypes');  
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
      AppSettings._BaseURL + "AddItem",
      item      
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

    return this.http.put<Item>(
      AppSettings._BaseURL + "UpdateItem",
      item      
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
      AppSettings._BaseURL + "GetItemByKey" + "/" + id
    );
  }
}
