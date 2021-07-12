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
  private names: string[] = ['Billy', 'Sammy', 'Jill', 'Barry', 'Sam'];

  studentForm = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
  });

  /**
   *
   */
  constructor(private studentService: StudentService) {
    //this.createStudents();
  }

  public ngOnInit(): void {
    this.studentService.getStudents().subscribe((result) => this.students = result);
  }

  private createStudents(): void {
    for(let i = 0; i < 4; i++) {
      const student = new Student();
      student.id = i;
      student.firstName = this.names[i];
      student.age = 10 + i;

      this.students.push(student);
    }
  }

  onSubmit() {
    console.warn(this.studentForm.value);
  }
}
