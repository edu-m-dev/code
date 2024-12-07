import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeesService } from './employees.service';

interface Employee {
  id: number;
  name: string;
  // Add other employee properties as needed
}

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class EmployeesComponent implements OnInit {
  employees = signal<Employee[]>([]);
  loading = signal<boolean>(true);
  error = signal<string | null>(null);

  constructor(private employeeService: EmployeesService) {}

  ngOnInit() {
    this.loadEmployees();
  }

  loadEmployees() {
    this.loading.set(true);
    this.employeeService.allEmployees().subscribe({
      next: (data) => {
        this.employees.set(data);
        this.loading.set(false);
      },
      error: (err) => {
        this.error.set('Failed to load employees. Please try again later.');
        this.loading.set(false);
        console.error('Error fetching employees:', err);
      }
    });
  }
}
