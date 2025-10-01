import { Component, DestroyRef, computed, effect, inject, signal } from '@angular/core';
import { AsyncPipe, JsonPipe, NgClass, NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { CdkDragDrop, DragDropModule, moveItemInArray } from '@angular/cdk/drag-drop';
import { TodoService } from '../services/todo.service';

@Component({
  selector: 'app-todos',
  standalone: true,
  imports: [NgIf, NgFor, FormsModule, NgClass, AsyncPipe, JsonPipe, MatFormFieldModule, MatInputModule, MatButtonModule, MatListModule, MatIconModule, MatCardModule, DragDropModule],
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

  protected trackById(index: number, item: { id: string }): string {
    return item.id;
  }

  protected drop(event: CdkDragDrop<unknown>): void {
    const current = this.items();
    moveItemInArray(current, event.previousIndex, event.currentIndex);
    this.items.set([...current]);
  }
}


