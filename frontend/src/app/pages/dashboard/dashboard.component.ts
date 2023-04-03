import { Component, OnInit, ElementRef } from '@angular/core';
import * as moment from 'moment';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    document.getElementById("today")?.click();
    setTimeout(() => {
      document.getElementById("today")?.click();
    }, 100);
  }

  ngAfterViewInit() {
    const t = document.getElementById("today");
    const w = document.getElementById("week");
    const m = document.getElementById("month");
    const y = document.getElementById("year");
    const date = document.getElementById("date");

    // logic for today button when the user is on dashboard
    t?.addEventListener('click', () => {
      date!.innerHTML = moment().format('MMMM, Do YYYY');
    });

    w?.addEventListener('click', () => {
      const startOfWeek = moment().startOf('week').format('MMMM Do');
      const endOfWeek = moment().endOf('week').format('MMMM Do, YYYY');
      date!.innerHTML = `From ${startOfWeek} to ${endOfWeek}`;
    });

    // logic for month button when the user is on dashboard
    m?.addEventListener('click', () => {
      date!.innerHTML = moment().format('MMMM, YYYY');
    });

    // logic for year button when the user is on dashboard
    y?.addEventListener('click', () => {
      date!.innerHTML = moment().format('YYYY');
    });




  }

}
