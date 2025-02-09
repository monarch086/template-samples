import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgSelectComponent, NgOptionComponent, } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    NgSelectComponent,
    NgOptionComponent,
    FormsModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'select-example-19-st';

  public selectedCar: number | null = null;

  public cars = [
      { id: 1, name: 'Volvo' },
      { id: 2, name: 'Saab' },
      { id: 3, name: 'Opel' },
      { id: 4, name: 'Audi' },
  ];
}
