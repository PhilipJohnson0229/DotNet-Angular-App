import { Injectable } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/category.model';
import { environment } from 'src/environments/environment.development';
import { UpdateCategoryRequest } from '../models/update-category-request.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { 
  }

  //the Observable type is a promise we have to subscribe to that will listen for the data to 
  //come back from the api
  //this is how we define our http requests
  addCategory(model : AddCategoryRequest) : Observable<void>{

    //with our http object we place the url to get our data from
    //after providing the url string we provide the body we will send back with the post request
    return this.http.post<void>(`${environment.baseUrl}/api/Categories`, model);
  }

  //this method will not need any parameters because were just returning a list with a GET method
  getAllCategories() : Observable<Category[]>{
    //when we use back ticks on the string we allow for the injection of variables
    //in this line were using the environment file we generated to provide the base url prefix
    return this.http.get<Category[]>(`${environment.baseUrl}/api/Categories`);
  }

  //this is how we pass in parameters to our service method
  getCategoryById(id:String) : Observable<Category>{
    return this.http.get<Category>(`${environment.baseUrl}/api/Categories/${id}`);
  }

  //here we pass in the id as well as the UpdateCategoryRequest object we defined
  updateCategory(id:String, updateCategoryRequest: UpdateCategoryRequest) : Observable<Category>{
    return this.http.put<Category>(`${environment.baseUrl}/api/Categories/${id}`, 
    updateCategoryRequest);
  }
}
