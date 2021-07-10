import { Component } from '@angular/core';
import { Student } from '../model/student.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public students: Student[] = [];
  private names: string[] = ['Billy Bob', 'Sammy Feet', 'Jill Low', 'Barry Gordon', 'Sam Goodwill'];

  /**
   *
   */
  constructor() {
    this.createStudents();
  }

  private createStudents(): void {

    for(let i = 0; i < 4; i++) {
      const student = new Student();
      student.id = i;
      student.name = this.names[i];
      student.age = 10 + i;

      this.students.push(student);
    }
  }
}
