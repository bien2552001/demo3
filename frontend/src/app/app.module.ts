import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './layouts/header/header.component';
import { FooterComponent } from './layouts/footer/footer.component';
import { SidebarComponent } from './layouts/sidebar/sidebar.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ChartsApexchartsComponent } from './components/charts-apexcharts/charts-apexcharts.component';
import { UsersProfileComponent } from './pages/users-profile/users-profile.component';
import { PagesFaqComponent } from './pages/pages-faq/pages-faq.component';
import { PagesContactComponent } from './pages/pages-contact/pages-contact.component';
import { PagesRegisterComponent } from './pages/pages-register/pages-register.component';
import { PagesLoginComponent } from './pages/pages-login/pages-login.component';
import { PagesError404Component } from './pages/pages-error404/pages-error404.component';
import { PagesBlankComponent } from './pages/pages-blank/pages-blank.component';
import { TableRestapiComponent } from './components/table-restapi/table-restapi.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PostComponent } from './components/table-restapi/post/post.component';
import { NgApexchartsModule } from 'ng-apexcharts';
import { TablefilterComponent } from './components/table-restapi/tablefilter/tablefilter.component';
import { DataTablesModule } from 'angular-datatables';
import { ConnectionServiceModule } from 'ng-connection-service';
//Fushion Chart
import { FusionChartsModule } from 'angular-fusioncharts';
import { FushionchartComponent } from './components/fushionchart/fushionchart.component';

// Import FusionCharts library and chart modules
import * as FusionCharts from 'fusioncharts';
import * as Charts from 'fusioncharts/fusioncharts.charts';

// For Powercharts , Widgets, and Maps
 import * as PowerCharts from 'fusioncharts/fusioncharts.powercharts';
 import * as Widgets from 'fusioncharts/fusioncharts.widgets';
 import * as Maps from 'fusioncharts/fusioncharts.maps';
// To know more about suites,
// read this https://www.fusioncharts.com/dev/getting-started/plain-javascript/install-using-plain-javascript

// For Map definition files
 import * as World from 'fusioncharts/maps/fusioncharts.world';
//Theme
import * as FusionTheme from 'fusioncharts/themes/fusioncharts.theme.fusion';
// For TimeSeries
import * as TimeSeries from 'fusioncharts/fusioncharts.timeseries';
import { DateComponent } from './pages/date/date.component';
import { DashboardChartComponent } from './pages/dashboard/dashboard-chart/dashboard-chart.component';
import { FirstchartComponent } from './pages/dashboard/dashboard-chart/firstchart/firstchart.component'; // Import timeseries



// Pass the fusioncharts library and chart modules
FusionChartsModule.fcRoot(FusionCharts, Charts, FusionTheme, World, Maps, Widgets, PowerCharts, TimeSeries);

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    SidebarComponent,
    DashboardComponent,
    ChartsApexchartsComponent,
    UsersProfileComponent,
    PagesFaqComponent,
    PagesContactComponent,
    PagesRegisterComponent,
    PagesLoginComponent,
    PagesError404Component,
    PagesBlankComponent,
    TableRestapiComponent,
    PostComponent,
    TablefilterComponent,
    FushionchartComponent,
    DateComponent,
    DashboardChartComponent,
    FirstchartComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    NgApexchartsModule,
    DataTablesModule,
    ConnectionServiceModule,
    FusionChartsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
