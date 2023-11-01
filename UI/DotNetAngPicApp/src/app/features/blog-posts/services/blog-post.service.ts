import { Injectable } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { Observable } from 'rxjs/internal/Observable';
import { BlogPost } from '../models/blog-post.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  constructor(private http: HttpClient) { }

  createBlogPost(data:AddBlogPost): Observable<BlogPost>{
    return this.http.post<BlogPost>(`${environment.baseUrl}/api/blogpost`, data);
  }

   //this method will not need any parameters because were just returning a list with a GET method
   getAllBlogPosts() : Observable<BlogPost[]>{
    
    //when we use back ticks on the string we allow for the injection of variables
    //in this line were using the environment file we generated to provide the base url prefix
    return this.http.get<BlogPost[]>(`${environment.baseUrl}/api/BlogPost`);
  }
}
