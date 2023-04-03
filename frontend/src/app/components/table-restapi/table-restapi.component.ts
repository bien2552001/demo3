import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ConnectionService, ConnectionState } from 'ng-connection-service';
import { TableRestApiModel } from './model/tablerestapimodel';
import { RestapiservicesService } from './restapiservices.service';

@Component({
  selector: 'app-table-restapi',
  templateUrl: './table-restapi.component.html',
  styleUrls: ['./table-restapi.component.css']
})
export class TableRestapiComponent implements OnInit {
  public allCurrent!: Array<TableRestApiModel>;
  signInForm = new FormGroup({
    value: new FormControl('') // <== default value
  });
  isLoading = true;
  online = true; // khởi tạo trạng thái ban đầu là online

  constructor(private http: RestapiservicesService, private connectionService: ConnectionService) { }

  ngOnInit(): void {
    this.getAll();

  }



  getAll() {
    this.http.getcurrent().subscribe(
      (res) => {
        this.allCurrent = res;
        this.isLoading = false;
      },
      (err) => {
        console.log(err);
        this.isLoading = false;
      }
    );
  }




  onDelete(id: string) {
    if (confirm('Bạn có chắc không?')) {
      this.http.deleteCurrent(id).subscribe(data1 => {
        this.http.getcurrent().subscribe(data => {
          this.allCurrent = data;
          location.reload(); // Reload trang web
        })
      });
    }
  }

  onPut(id: string) {
    this.http.putcurrent(id, this.signInForm.value).subscribe(da => { });
  }

  Put2(id: string) {
    this.http.putcurrent(id, this.signInForm.value).subscribe(da => {
      this.http.getcurrent().subscribe(data1 => {
        this.allCurrent = data1;
      })
    });
  }
}
