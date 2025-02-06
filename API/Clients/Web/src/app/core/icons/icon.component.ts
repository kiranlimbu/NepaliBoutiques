import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-icon',
  template: `<lucide-icon [name]="name"></lucide-icon>`,
  styles: [
    `
      lucide-icon {
        display: inline-block;
        transform: translateY(12%);
      }
    `,
  ],
})
export class IconComponent {
  @Input() name!: string;
}
