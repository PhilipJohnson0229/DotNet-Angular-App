import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { UpdateCategoryRequest } from '../models/update-category-request.model';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit, OnDestroy{

  id: String | null = null;
  paramsSubscription?: Subscription; 
  editCategorySubscription?: Subscription;
  category?: Category;

  constructor(private route: ActivatedRoute, 
    private categoryService:CategoryService, private router: Router){
    
  }

  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params)=>{
        this.id = params.get('id');

        if(this.id){
          //get the data for the category and update the ui based on this id
          this.categoryService.getCategoryById(this.id).subscribe({
            next: (response)=>{
              this.category = response;
            }
          });
        }
      }
    });
  }

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.editCategorySubscription?.unsubscribe();
  }

  onFormSubmit():void{
    const updateCategoryRequest: UpdateCategoryRequest = {
      //the double question marks acts as a shorthand for a turnary operator
      // that only checks for null
      name:this.category?.name ?? '',
      urlHandle:this.category?.urlHandle ?? ''
    };

    //pass this object to the service
    if(this.id){
      this.editCategorySubscription = this.categoryService.updateCategory(this.id, updateCategoryRequest).subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/categories');
        }
      });
    }
  }

  onDelete():void{

  }
}
