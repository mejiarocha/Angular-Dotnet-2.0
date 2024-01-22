import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Mascota } from 'src/app/interface/mascota';

@Component({
  selector: 'app-agregar-editar-mascota',
  templateUrl: './agregar-editar-mascota.component.html',
  styleUrls: ['./agregar-editar-mascota.component.css'],
})
export class AgregarEditarMascotaComponent implements OnInit {
  loading: boolean = false;

  formMascota: FormGroup;

  constructor(private fb: FormBuilder) {
    this.formMascota = fb.group({
      nombre: ['', Validators.required],
      raza: ['', Validators.required],
      edad: ['', Validators.required],
      peso: ['', Validators.required],
      color: ['', Validators.required],
    });
  }

  ngOnInit(): void {}

  agregarMascota() {
    const nombre = this.formMascota.get('nombre')!.value;
    const raza = this.formMascota.get('raza')!.value;
    const edad = this.formMascota.get('edad')!.value;
    const peso = this.formMascota.get('peso')!.value;
    const color = this.formMascota.get('color')!.value;

    const mascota: Mascota = {
      nombre: nombre,
      raza: raza,
      edad: edad,
      peso: peso,
      color: color,
    };
    console.log(mascota);
  }
}
