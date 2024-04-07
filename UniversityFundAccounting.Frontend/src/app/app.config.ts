import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { CorpuseService } from './Services/corpuse.service';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideAnimations } from '@angular/platform-browser/animations'

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes),
     provideHttpClient(),
     provideAnimations(),
     provideAnimationsAsync(),
     {provide: CorpuseService }]
};
