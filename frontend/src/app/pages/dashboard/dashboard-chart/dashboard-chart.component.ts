import { Component, OnInit } from '@angular/core';
import * as FusionCharts from 'fusioncharts';
//import charts from 'fusioncharts/fusioncharts.charts';
//import widgets from 'fusioncharts/fusioncharts.widgets';
//import powercharts from 'fusioncharts/fusioncharts.powercharts';
//import theme from 'fusioncharts/themes/fusioncharts.theme.ocean';
import { FusionChartsModule } from 'angular-fusioncharts';
import * as Charts from 'fusioncharts/fusioncharts.charts';
import * as World from 'fusioncharts/maps/fusioncharts.world';
import * as Maps from 'fusioncharts/fusioncharts.maps';
import * as Widgets from 'fusioncharts/fusioncharts.widgets';
import * as FusionTheme from 'fusioncharts/themes/fusioncharts.theme.fusion';
import * as PowerCharts from 'fusioncharts/fusioncharts.powercharts';
import * as TimeSeries from 'fusioncharts/fusioncharts.timeseries';
import * as moment from 'moment';
import { FirstchartComponent } from './firstchart/firstchart.component';
//charts(FusionCharts) /*=Charts*/
//widgets(FusionCharts)/*=Widgets*/
//powercharts(FusionCharts)/*=PowerCharts*/
//theme(FusionCharts)/*=FusionTheme*/


//FusionCharts.options.creditLabel = false;
// Pass the fusioncharts library and chart modules
FusionChartsModule.fcRoot(FusionCharts, Charts, FusionTheme, World, Maps, Widgets, PowerCharts, TimeSeries);
@Component({
  selector: 'app-dashboard-chart',
  templateUrl: './dashboard-chart.component.html',
  styleUrls: ['./dashboard-chart.component.css']
})

export class DashboardChartComponent implements OnInit {

  constructor(private chart:FirstchartComponent) { }
   ngOnInit(): void {
     document.getElementById("today")?.click();
     setTimeout(() => {
       document.getElementById("today")?.click();
     }, 100);
   }

  //componentDidMount(): void {
  //  document.getElementById("today")?.click();
  //  setTimeout(function () {
  //    document.getElementById("today")?.click();
  //  }, 300);
  //}

  //componentDidUpdate() {
  //  var t = document.getElementById("today");
  //  var m = document.getElementById("month");
  //  var y = document.getElementById("year");
  //  const date = document.getElementById("date");
  //  t?.addEventListener('click', () => {
  //    date!.innerHTML = moment().format('MMMM, Do YYYY');

  //    const todaynewdata1 = this.first_chart_today;

  //  });
 
  //}

}
