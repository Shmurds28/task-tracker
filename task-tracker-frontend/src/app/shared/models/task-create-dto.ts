export interface TaskCreateDto  {
  title: string;
  description?: string;
  status: 'New' | 'InProgress' | 'Done';
  priority: 'Low' | 'Medium' | 'High';
  dueDate?: string;
}
