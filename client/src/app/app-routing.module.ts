import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemeberListComponent } from './member/memeber-list/memeber-list.component';
import { MemeberDetailComponent } from './member/memeber-detail/memeber-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';

const routes:Routes = [
  {
    path:'',
    component: HomeComponent
  },
  {
    path:'',
   
    canActivate: [AuthGuard],
  runGuardsAndResolvers:'always',
    children: [ 
      
  
    
      {path:'memebers',component: MemeberListComponent},
      {path:'memebers/:id',component: MemeberDetailComponent},
      {path:'lists',component: ListsComponent},
      {path:'messages',component: MessagesComponent},
      
    ]
  },
  {path:'not-found',component: NotFoundComponent},
  {path:'server-error',component: ServerErrorComponent},
  {path:'errors',component: TestErrorsComponent},
  {path:'**',component: NotFoundComponent,pathMatch:'full'}

]  

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
