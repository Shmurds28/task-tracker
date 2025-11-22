export interface Task {
  id: number;
  title: string;
  description?: string;
  status: 'New' | 'InProgress' | 'Done';
  priority: 'Low' | 'Medium' | 'High';
  dueDate?: string;
  createdAt: string;
}
