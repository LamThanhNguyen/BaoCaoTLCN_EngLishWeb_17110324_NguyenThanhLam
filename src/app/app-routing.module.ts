import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddGrammarComponent } from './admin/grammar/add-grammar.component';
import { AddPracticeComponent } from './admin/practice/add-practice/add-practice.component';
import { AddVocabulary } from './admin/vocabulary/add-vocabulary/add-vocabulary.component';
import { ShowGrammarComponent } from './grammar/show-grammar/show-grammar.component';
import { LoginWithGoogleComponent } from './loginwithgoogle/login-with-google';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { ShowPracticeComponent } from './practice/show-practice/show-practice.component';
import { RegisterComponent } from './register/register.component';
import { VocabularyDetailComponent } from './vocabulary/vocabulary-detail/vocabulary-detail.component';
import { VocabularyEditComponent } from './vocabulary/vocabulary-edit/vocabulary-edit.component';
import { VocabularyListComponent } from './vocabulary/vocabulary-list/vocabulary-list.component';
import { AdminGuard } from './_guards/admin.guard';
import { AuthGuard } from './_guards/auth.guard';


const routes: Routes = [
  {path: '', component: VocabularyListComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'loginwithgoogle', component: LoginWithGoogleComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'member/edit', component: MemberEditComponent},
      {path: 'showgrammar', component: ShowGrammarComponent},
      {path: 'showpractice', component: ShowPracticeComponent},
      {path: 'vocabulary/add', component: AddVocabulary, canActivate: [AdminGuard]},
      {path: 'vocabulary/detail/:id', component: VocabularyDetailComponent},
      {path: 'vocabulary/edit/:id', component: VocabularyEditComponent, canActivate: [AdminGuard]},
      {path: 'addgrammar', component: AddGrammarComponent, canActivate: [AdminGuard]},
      {path: 'addpractice', component: AddPracticeComponent, canActivate: [AdminGuard]},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})


export class AppRoutingModule { }
