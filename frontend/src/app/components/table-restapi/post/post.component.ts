import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { RestapiservicesService } from '../restapiservices.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {


  signInForm = new FormGroup({
    value: new FormControl('') // <== default value

  });
  constructor(private http: RestapiservicesService) { }

  ngOnInit(): void {
  }
  onSubmit() {
    console.log(this.signInForm.value);
  }
  onPost() {
    this.http.postcurrent(this.signInForm.value).subscribe(data => {
      console.log(data);
    });

    //console.log(this.signInForm.value);
  }
  postsuccess() {
    alert('Post_successful');
  }

}
