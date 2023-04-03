import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-fushionchart',
  templateUrl: './fushionchart.component.html',
  styleUrls: ['./fushionchart.component.css']
})
export class FushionchartComponent implements OnInit {

  dataSource: Object;
  title: string;
  chart!: any;
  constructor() {
    this.title = 'Angular  FusionCharts Sample';

    this.dataSource = {
      chart: {
        caption: 'Countries With Most Oil Reserves [2017-18]',
        subCaption: 'In MMbbl = One Million barrels',
        xAxisName: 'Country',
        yAxisName: 'Reserves (MMbbl)',
        numberSuffix: 'K',
        theme: 'fusion'
      },
      data: [
        { label: 'Venezuela', value: '290' },
        { label: 'Saudi', value: '260' },
        { label: 'Canada', value: '180' },
        { label: 'Iran', value: '140' },
        { label: 'Russia', value: '115' },
        { label: 'UAE', value: '100' },
        { label: 'US', value: '30' },
        { label: 'China', value: '30' }
      ]
    };

  }

    ngOnInit(): void {
        
  }

  initialized($event:any) {
    this.chart = $event.chart; // Storing the chart instance
  }

  changeLabel() {
    this.chart.setChartAttribute('caption', 'Changed caption123');
  }












}
