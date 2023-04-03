import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ChartsApexchartsComponent } from './components/charts-apexcharts/charts-apexcharts.component';
import { PagesBlankComponent } from './pages/pages-blank/pages-blank.component';
import { PagesContactComponent } from './pages/pages-contact/pages-contact.component';
import { PagesError404Component } from './pages/pages-error404/pages-error404.component';
import { PagesFaqComponent } from './pages/pages-faq/pages-faq.component';
import { PagesLoginComponent } from './pages/pages-login/pages-login.component';
import { PagesRegisterComponent } from './pages/pages-register/pages-register.component';
import { UsersProfileComponent } from './pages/users-profile/users-profile.component';
import { TableRestapiComponent } from './components/table-restapi/table-restapi.component';
import { PostComponent } from './components/table-restapi/post/post.component';
import { TablefilterComponent } from './components/table-restapi/tablefilter/tablefilter.component';
import { FushionchartComponent } from './components/fushionchart/fushionchart.component';
import { FirstchartComponent } from './pages/dashboard/dashboard-chart/firstchart/firstchart.component';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'charts-apexcharts', component: ChartsApexchartsComponent },
   { path: 'pages-blank', component: PagesBlankComponent },
  { path: 'pages-contact', component: PagesContactComponent },
  { path: 'pages-error404', component: PagesError404Component },
  { path: 'pages-faq', component: PagesFaqComponent },
  { path: 'pages-login', component: PagesLoginComponent },
  { path: 'pages-register', component: PagesRegisterComponent },
  { path: 'user-profile', component: UsersProfileComponent },
  //-------------------Orther-------------------
  { path: 'table', component: TableRestapiComponent },
  { path: 'tablefilter', component: TablefilterComponent },
  { path: 'post', component: PostComponent },
  { path: 'fusion', component: FushionchartComponent },
  { path: 'first', component: FirstchartComponent },


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
