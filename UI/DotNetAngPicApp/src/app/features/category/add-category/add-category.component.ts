import { Component, OnDestroy } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { CategoryService } from '../services/category.service';
import { Subscription } from 'rxjs/internal/Subscription';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent implements OnDestroy {

  // create an intialize the model
  model: AddCategoryRequest;
  //the ? char means we dont have to intialize this right away it can be undefined
  private addCategorySubscription?: Subscription;

  //initialize within the constructor
  //we define paramaters withe the access modifier and the name of the variable then
  //defint the type with the class so its like backwards compared to java or c#
  constructor(private categoryService: CategoryService, private router: Router) {
    this.model = {
        name:'',
        urlHandle:''
    };
  }

  onFormSubmit(){
    //if we dont subscribe to the method then it wont work
    //its euqally important to unsibscribe from callbacks to preent memoryleaks
    this.addCategorySubscription = this.categoryService.addCategory(this.model).subscribe({
      //this is what happens with a successful response
      //theres also error and complete just look them up
      //the arrow operator represents the method and the parentheses take the arguments
      next: (response) => {
        this.router.navigateByUrl('/admin/categories')
      }
    });
  }

  //the lifecycle hook we use to unsubscribe to handle memory leaks
  //every component has angular lifecyclehooks
  ngOnDestroy(): void {
    this.addCategorySubscription?.unsubscribe();
   }
}
