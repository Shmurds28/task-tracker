import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { TaskService } from 'src/app/core/services/task.service';
import { Task } from 'src/app/shared/models/task';
import { TaskFormComponent } from '../task-form/task-form.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss']
})
export class TaskListComponent implements OnInit {
  tasks: Task[] = [];
  search = '';
  sort = 'dueDate:asc';
  loading: boolean = false;

  constructor(
    private service: TaskService,
    private dialog: MatDialog
  ) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.service.getTasks(this.search, this.sort).subscribe(res => {
      this.tasks = res;
    });
  }

  openForm(id?: number) {
    const dialogRef = this.dialog.open(TaskFormComponent, {
      width: '450px',
      data: { id }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) this.load();
    });
  }

  delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete task?',
      icon: 'warning', // 'type' is deprecated
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it'
    }).then((result) => {
      if (result.isConfirmed) {
        this.service.deleteTask(id).subscribe(() => {
          Swal.fire({
            toast: true,
            position: 'bottom',
            showConfirmButton: false,
            timer: 5000,
            timerProgressBar: true,
            icon: 'success',
            title: "Task successfully deleted"
          });
          this.load()
        });
      }
    });
  }
}
