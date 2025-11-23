import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskService } from 'src/app/core/services/task.service';
import { Task } from 'src/app/shared/models/task';
import { TaskCreateDto } from 'src/app/shared/models/task-create-dto';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-task-form',
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.scss']
})
export class TaskFormComponent implements OnInit  {
    task: Task | undefined;
    taskId: number | undefined;

   form = this.fb.group({
    title: ['', Validators.required],
    description: ['', Validators.required],
    dueDate: [null as string | null, []],
    status: ['New', Validators.required],
    priority: ['Medium', Validators.required]
  });

  constructor(
    private fb: FormBuilder,
    private taskService: TaskService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      this.taskId = id ? Number(id) : undefined;
      if(this.taskId) {
        this.taskService.getTask(this.taskId).subscribe(task => {
          this.form.patchValue(task);
        });
      }
    });
  }

  submit() {
    if (this.form.invalid) return;

    const payload = this.form.value as TaskCreateDto;

    if (this.taskId) {
      this.taskService.updateTask(this.taskId, payload).subscribe(() => {
        Swal.fire({
          toast: true,
          position: 'bottom',
          showConfirmButton: false,
          timer: 5000,
          timerProgressBar: true,
          icon: 'success',
          title: "Task successfully updated"
        });
        
        this.router.navigate(['/tasks']);
      });
    } else {
      this.taskService.createTask(payload).subscribe(() => {
        Swal.fire({
          toast: true,
          position: 'bottom',
          showConfirmButton: false,
          timer: 5000,
          timerProgressBar: true,
          icon: 'success',
          title: "Task successfully created"
        });

        this.router.navigate(['/tasks']);
      });
    }
  }

}
