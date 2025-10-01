import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

export interface AppConfig {
  apiBaseUrl: string;
}

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  private config: AppConfig | null = null;

  constructor(private http: HttpClient) {}

  async loadConfig(): Promise<void> {
    this.config = await firstValueFrom(
      this.http.get<AppConfig>('/assets/config/config.json')
    );
  }

  getConfig(): AppConfig {
    if (!this.config) {
      throw new Error('Config not loaded');
    }
    return this.config;
  }
}
