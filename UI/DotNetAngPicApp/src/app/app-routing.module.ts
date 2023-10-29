import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { EditCategoryComponent } from './features/category/edit-category/edit-category.component';

const routes: Routes = [
  // this is an object inside of an array....duh
  {
    path:'admin/categories',
    //render the component we made
    component:CategoryListComponent
  },
  {
    path:'admin/categories/add',
    //render the component we made
    component:AddCategoryComponent
  },
  {
    //the colon notates that this route requires the id query param
    path:'admin/categories/:id',
    //render the component we made
    component:EditCategoryComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
