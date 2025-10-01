import { Component, DestroyRef, computed, effect, inject, signal } from '@angular/core';
import { AsyncPipe, JsonPipe, NgClass, NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TodoService } from '../services/todo.service';

@Component({
  selector: 'app-todos',
  standalone: true,
  imports: [NgIf, NgFor, FormsModule, NgClass, AsyncPipe, JsonPipe],
  templateUrl: './todos.component.html',
  styleUrl: './todos.component.scss'
})
export class TodosComponent {
  private readonly service = inject(TodoService);

  protected readonly items = this.service.items;
  protected readonly loading = this.service.loading;
  protected readonly error = this.service.error;

  protected readonly newTitle = signal<string>('');
  protected readonly canAdd = computed(() => this.newTitle().trim().length > 0);

  constructor() {
    this.service.load();
  }

  protected add(): void {
    const title = this.newTitle().trim();
    if (!title) return;
    this.service.add(title);
    this.newTitle.set('');
  }

  protected remove(id: string): void {
    this.service.remove(id);
  }
}


