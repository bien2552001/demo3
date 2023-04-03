import { Component, Input, OnInit } from '@angular/core';
@Component({
  selector: 'app-firstchart',
  templateUrl: './firstchart.component.html',
  styleUrls: ['./firstchart.component.css']
})
export class FirstchartComponent implements OnInit {
  title: string;
  chart: any;
  constructor() {
    this.title = 'Angular test';
    
  }
  first_chart_today = {
      "chart": {
        "showBorder": "0",
        "showShadow": "0",
        "use3DLighting": "0",
        "showLabels": "0",
        "showValues": "0",
        "paletteColors": "#58E2C2, #F7E53B",
        "bgColor": "#1D1B41",
        "bgAlpha": "0",
        "canvasBgAlpha": "0",
        "doughnutRadius": "75",
        "pieRadius": "95",
        // "pieRadius": "100",
        "numberPrefix": "$",
        "plotBorderAlpha": "0",
        "toolTipBgcolor": "#484E69",
        "toolTipPadding": "7",
        "toolTipBorderRadius": "3",
        "toolTipBorderAlpha": "30",
        "tooltipBorderThickness": "0.7",
        "toolTipColor": "#FDFDFD",
        "baseFont": "Nunito Sans",
        "baseFontSize": "14",
        "baseFontColor": "#FDFDFD",
        "showLegend": "1",
        "legendShadow": "0",
        "legendBorderAlpha": "0",
        "drawCustomLegendIcon": "1",
        "legendBgAlpha": "0",
        "chartTopMargin": "-10",
        "canvasTopMargin": "-10",
        "chartBottomMargin": "20",
        "canvasBottomMargin": "20",
        "legendNumColumns": "1",
        "legendPosition": "RIGHT",
        "defaultCenterLabel": "Total <br> $6.2",
        "centerLabel": "$label<br>$value",
        "centerLabelBold": "1",
        "centerLabelFontSize": "20",
        "enableRotation": "0",
        "transposeAnimation": "1",
        "plotToolText": "<div>$label<br>$dataValue ($percentValue)<div>"
      },
      "data": [{
        "label": "Electricity",
        "value": "3.9"
      }, {
        "label": "Gas",
        "value": "2.3"
      }]
  };
  @Input() chartData: any;

  setChartdata(chartData: any) {
    this.chartData = chartData;
  }
  //ngOnInit() {
  //  FusionChartsModule.fcRoot(FusionCharts, Charts, FusionTheme);
  //  const chartConfig = {
  //    type: 'doughnut2d',
  //    width: '100%',
  //    height: '400',
  //    dataFormat: 'json',
  //    dataSource: this.first_chart_today
  //  };
  //  const chartInstance = new FusionCharts(chartConfig);
  //  chartInstance.render('chart-container');
  //}


  ngOnInit() {
  }
}
