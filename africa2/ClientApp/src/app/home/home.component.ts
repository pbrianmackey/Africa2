import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Student } from '../model/student.model';
import { StudentService } from '../services/student.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  public students: Student[] = [];

  studentForm = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
  });

  constructor(private studentService: StudentService) {
  }

  public ngOnInit(): void {
    this.studentService.getStudents().subscribe((result) => this.students = result);
  }

  onSubmit() {
    console.warn(this.studentForm.value);
    this.studentService.postStudent(this.studentForm.value).subscribe((result) => { console.log(result) })
  }
}
