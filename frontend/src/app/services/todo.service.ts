import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from './config.service';

export type TodoItem = {
  id: string;
  title: string;
  isCompleted: boolean;
};

@Injectable({ providedIn: 'root' })
export class TodoService {
  private readonly http = inject(HttpClient);
  private readonly configService = inject(ConfigService);
  private get baseUrl() {
    return `${this.configService.getConfig().apiBaseUrl}/todos`;
  }

  readonly items = signal<TodoItem[]>([]);
  readonly loading = signal<boolean>(false);
  readonly error = signal<string | null>(null);

  load(page: number = 1, pageSize: number = 100): void {
    this.loading.set(true);
    this.error.set(null);
    const params = { page, pageSize } as const;
    this.http.get<TodoItem[]>(this.baseUrl, { params }).subscribe({
      next: (items) => {
        this.items.set(items);
        this.loading.set(false);
      },
      error: (err) => {
        this.error.set(err?.message ?? 'Failed to load');
        this.loading.set(false);
      }
    });
  }

  add(title: string): void {
    const body = { title };
    this.http.post<TodoItem>(this.baseUrl, body).subscribe({
      next: (created) => this.items.update((list) => [created, ...list]),
      error: (err) => this.error.set(err?.message ?? 'Failed to add')
    });
  }

  remove(id: string): void {
    this.http.delete<void>(`${this.baseUrl}/${id}`).subscribe({
      next: () => this.items.update((list) => list.filter((i) => i.id !== id)),
      error: (err) => this.error.set(err?.message ?? 'Failed to delete')
    });
  }
}


