import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/app/environments/environment';
import { Task } from 'src/app/shared/models/task';
import { Observable } from 'rxjs';
import { TaskCreateDto } from 'src/app/shared/models/task-create-dto';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private baseUrl = environment.apiBaseUrl + '/api/tasks';

  constructor(private http: HttpClient) { }

  getTasks(q?: string, sort?: string): Observable<Task[]> {
    let params = new HttpParams();
    if (q) params = params.set('q', q);
    if (sort) params = params.set('sort', sort);
    return this.http.get<Task[]>(this.baseUrl, { params });
  }

  getTask(id: number): Observable<Task> {
    return this.http.get<Task>(`${this.baseUrl}/${id}`);
  }

  createTask(dto: TaskCreateDto): Observable<Task> {
    return this.http.post<Task>(this.baseUrl, dto);
  }

  updateTask(id: number, dto: TaskCreateDto): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, dto);
  }

  deleteTask(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

}
