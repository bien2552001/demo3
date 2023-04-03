import { Component, Input, OnInit, ViewChild } from '@angular/core';
import * as moment from 'moment';
import { ApexAxisChartSeries, ApexChart, ApexDataLabels, ApexResponsive, ApexStroke, ApexTooltip, ApexXAxis, ChartComponent } from 'ng-apexcharts';
import { Observable } from 'rxjs';
import { TableRestApiModel } from '../table-restapi/model/tablerestapimodel';
import { TablefilterservicesService } from '../table-restapi/tablefilter/tablefilterservices.service';
import { ApexchartservicesService } from './apexchartservices.service';

export type ChartOptions = {
  series: ApexAxisChartSeries | any;
  chart: ApexChart | any;
  xaxis: ApexXAxis | any;
  stroke: ApexStroke | any;
  tooltip: ApexTooltip | any;
  dataLabels: ApexDataLabels | any;
  labels: ApexDataLabels | any;
  responsive: ApexResponsive | any;
};

@Component({
  selector: 'app-charts-apexcharts',
  templateUrl: './charts-apexcharts.component.html',
  styleUrls: ['./charts-apexcharts.component.css']
})
export class ChartsApexchartsComponent implements OnInit {

  @Input() dynamicdata: any;
  chartData$!: Observable<TableRestApiModel[]>;
  public data1: any;
  public chartOptions: Partial<ChartOptions> | any;

  constructor(private http: ApexchartservicesService) { }

  @ViewChild("chart")
  public chart!: ChartComponent;

  ngOnInit(): void {
    this.getdata();
  }

  async getdata() {
    await this.http.GetTimeday().subscribe(
      result => {
        this.data1 = result;
        let arr2 = [];
        for (let i = 0; i < this.data1.length; i++) {
          arr2.push(this.data1[i].value);
        }
        let arr3 = [];
        for (let i = 0; i < this.data1.length; i++) {
          arr3.push(this.data1[i].createdDate);
        }

        this.chartOptions = {
          series: [
            {
              name: "Current",
              data: arr2.map(num => num.toFixed(2))
            }
          ],
          chart: {
            height: 400,
            type: 'line',
            dropShadow: {
              enabled: true,
              color: '#000',
              top: 18,
              left: 10,
              blur: 5,
              opacity: 0.2
            },
            toolbar: {
              show: true
            }
          },
          colors: ['#77B6EA', '#545454'],
          dataLabels: {
            enabled: true,
          },
          stroke: {
            curve: 'smooth'
          },
          title: {
            text: 'Average High & Low Temperature',
            align: 'left'
          },
          grid: {
            borderColor: '#e7e7e7',
            row: {
              colors: ['#f3f3f3', 'transparent'], // takes an array which will be repeated on columns
              opacity: 0.5
            },
          },
          markers: {
            size: 1
          },
          xaxis: {
            title: {
              style: {
                color: 'black',
                fontFamily: 'Roboto,helvetica neue,helvetica,arial,sans-serif',
                fontSize: '12.2px',
                fontWeight: 380,
              }
            },
            labels: {
              formatter: function (val: any) {
                return moment(val).format('DD/MM/YYYY -- HH:MM A')
              },
            },
            categories: arr3
          },
          yaxis: {
            title: {
              text: 'Current (A)',
              //rotate: -90,
              style: {
                color: 'black',
                fontFamily: 'Roboto,helvetica neue,helvetica,arial,sans-serif',
                fontSize: '15px',
                fontWeight: 600,
              },
            },
          },
          legend: {
            position: 'top',
            horizontalAlign: 'right',
            floating: false,
            offsetY: 0,
            offsetX: 0
          }
        };
      }
    )
  }


}
