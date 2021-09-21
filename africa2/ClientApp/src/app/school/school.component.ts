import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { School } from '../model/school.model';
import { SchoolService } from '../services/school.service';

@Component({
  selector: 'app-school',
  templateUrl: './school.component.html',
  styleUrls: ['./school.component.css']
})
export class SchoolComponent implements OnInit {
  public schools: School[] = [];

  schoolForm = new FormGroup({
    name: new FormControl(''),
    address: new FormControl(''),
  });

  constructor(private schoolService: SchoolService) {
  }

  public ngOnInit(): void {
    this.schoolService.getSchools().subscribe((result) => this.schools = result);
  }

  onSubmit() {
    console.warn(this.schoolForm.value);
    this.schoolService.postSchool(this.schoolForm.value).subscribe((result) => { console.log(result) })
  }
}
