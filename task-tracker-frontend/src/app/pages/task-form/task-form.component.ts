import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
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
   form = this.fb.group({
    title: ['', Validators.required],
    description: ['', Validators.required],
    dueDate: [null as string | null, []],
    status: ['New', Validators.required],
    priority: ['Medium', Validators.required]
  });

  constructor(
    private fb: FormBuilder,
    private service: TaskService,
    private dialogRef: MatDialogRef<TaskFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit() {
    if (this.data?.id) {
      this.service.getTask(this.data.id).subscribe(task => {
        this.form.patchValue(task);
      });
    }
  }

  submit() {
    if (this.form.invalid) return;

    const payload = this.form.value as TaskCreateDto;

    if (this.data?.id) {
      this.service.updateTask(this.data.id, payload).subscribe(() => {
        Swal.fire({
          toast: true,
          position: 'bottom',
          showConfirmButton: false,
          timer: 5000,
          timerProgressBar: true,
          icon: 'success',
          title: "Task successfully updated"
        });
        this.dialogRef.close(true);
      });
    } else {
      this.service.createTask(payload).subscribe(() => {
        Swal.fire({
          toast: true,
          position: 'bottom',
          showConfirmButton: false,
          timer: 5000,
          timerProgressBar: true,
          icon: 'success',
          title: "Task successfully created"
        });
        this.dialogRef.close(true);
      });
    }
  }

}
