import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as moment from 'moment';
import { TablefilterComponent } from '../table-restapi/tablefilter/tablefilter.component';

@Injectable({
  providedIn: 'root'
})
export class ApexchartservicesService {

  constructor(private http: HttpClient) { }


  day1a = moment().startOf('day').subtract(1, 'day').format("YYYY-MM-DDTHH:mm:ss")
  day2a = moment().startOf('day').subtract(2, 'day').format("YYYY-MM-DDTHH:mm:ss")
  day3a = moment().startOf('day').subtract(3, 'day').format("YYYY-MM-DDTHH:mm:ss")
  day4a = moment().startOf('day').subtract(4, 'day').format("YYYY-MM-DDTHH:mm:ss")
  day5a = moment().startOf('day').subtract(5, 'day').format("YYYY-MM-DDTHH:mm:ss")
  day6a = moment().startOf('day').subtract(6, 'day').format("YYYY-MM-DDTHH:mm:ss")
  day7a = moment().startOf('day').subtract(7, 'day').format("YYYY-MM-DDTHH:mm:ss")

  day1b = moment().endOf('day').subtract(1, 'day').format("YYYY-MM-DDTHH:mm:ss")
  day2b = moment().endOf('day').subtract(2, 'day').format("YYYY-MM-DDTHH:mm:ss")
  day3b = moment().endOf('day').subtract(3, 'day').format("YYYY-MM-DDTHH:mm:ss")
  day4b = moment().endOf('day').subtract(4, 'day').format("YYYY-MM-DDTHH:mm:ss")
  day5b = moment().endOf('day').subtract(5, 'day').format("YYYY-MM-DDTHH:mm:ss")
  day6b = moment().endOf('day').subtract(6, 'day').format("YYYY-MM-DDTHH:mm:ss")
  day7b = moment().endOf('day').subtract(7, 'day').format("YYYY-MM-DDTHH:mm:ss")


  timedaya = moment().startOf('day').format("YYYY-MM-DDTHH:mm:ss")
  timedayb = moment().endOf('day').format("YYYY-MM-DDTHH:mm:ss")

  timeweek = moment().endOf('day').subtract(7, 'day').format("YYYY-MM-DDTHH:mm:ss")
  timemonth = moment().endOf('day').subtract(30, 'day').format("YYYY-MM-DDTHH:mm:ss")

  _api = 'https://localhost:5001/data';

  GetAll() {
    return this.http.get('https://localhost:5001/data/current')
  }


  Getday7() {
    return this.http.get(this._api + '/current?from=' + this.day7a + '&to=' + this.day7b)
  }
  Getday6() {
    return this.http.get(this._api + '/current?from=' + this.day6a + '&to=' + this.day6b)
  }
  Getday5() {
    return this.http.get(this._api + '/current?from=' + this.day5a + '&to=' + this.day5b)
  }
  Getday4() {
    return this.http.get(this._api + '/current?from=' + this.day4a + '&to=' + this.day4b)
  }
  Getday3() {
    return this.http.get(this._api + '/current?from=' + this.day3a + '&to=' + this.day3b)
  }
  Getday2() {
    return this.http.get(this._api + '/current?from=' + this.day2a + '&to=' + this.day2b)
  }
  Getday1() {
    return this.http.get(this._api + '/current?from=' + this.day1a + '&to=' + this.day1b)
  }


  //GetTimeday() {
  //  return this.http.get(this._api + '/current?from=' + this.timedaya + '&to=' + this.timedayb)
  //}
  GetTimeday() {
    return this.http.get('https://localhost:5001/data/current')
  }
  
  GetTimeweek() {
    return this.http.get(this._api + '/current?from=' + this.timeweek + '&to=' + this.timedayb)
  }
  GetTimemonth() {
    return this.http.get(this._api + '/current?from=' + this.timemonth + '&to=' + this.timedayb)
  }


}
