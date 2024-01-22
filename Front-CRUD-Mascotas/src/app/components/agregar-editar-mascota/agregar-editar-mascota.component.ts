import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Mascota } from 'src/app/interface/mascota';
import { MascotaService } from 'src/app/service/mascota.service';

@Component({
  selector: 'app-agregar-editar-mascota',
  templateUrl: './agregar-editar-mascota.component.html',
  styleUrls: ['./agregar-editar-mascota.component.css'],
})
export class AgregarEditarMascotaComponent implements OnInit {
  loading: boolean = false;
  id: number;
  formMascota: FormGroup;

  operacion: string = 'Agregar ';

  constructor(
    private fb: FormBuilder,
    private _mascotaService: MascotaService,
    private _snackBar: MatSnackBar,
    private router: Router,
    private aroute: ActivatedRoute
  ) {
    this.formMascota = fb.group({
      nombre: ['', Validators.required],
      raza: ['', Validators.required],
      edad: ['', Validators.required],
      peso: ['', Validators.required],
      color: ['', Validators.required],
    });

    this.id = Number(this.aroute.snapshot.paramMap.get('id'));
  }

  ngOnInit(): void {
    if (this.id != 0) {
      this.operacion = 'Editar ';
      this.getMascota(this.id);
    }
  }

  getMascota(id: number) {
    this.loading = true;
    this._mascotaService.getMascotaById(id).subscribe((data) => {
      // Agrega la informacion al formulario para editarla
      this.formMascota.patchValue({
        nombre: data.nombre,
        raza: data.raza,
        edad: data.edad,
        peso: data.peso,
        color: data.color,
      });
      this.loading = false;
      console.log(data);
    });
  }

  agregarEditarMascota() {
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
    // comprobar id para ver si se quiere editar o crear, cuando es 0 es crear

    if(this.id == 0){
      this.agregarMascota(mascota);
    }else{
      mascota.id = this.id;
      this.editarMascota(mascota, this.id);
    }

  }

  agregarMascota(mascota: Mascota) {
    this.loading = true;
    this._mascotaService.createMascota(mascota).subscribe((data) => {
      this.mensajeExito();
      this.loading = false;
      this.router.navigate(['/listMascotas']);
    });
  }
  editarMascota(mascota: Mascota, id: number) {
    this.loading = true;
    this._mascotaService.updateMascota(id, mascota).subscribe(() =>{
      this.mensajeExito();
      this.loading = false;
      this.router.navigate(['/listMascotas']);
    })
  }

  mensajeExito() {
    if(this.id == 0){
      this._snackBar.open('Mascota registrada con exito', 'OK', {
        duration: 1500,
        horizontalPosition: 'right',
      });
    }else{
      this._snackBar.open('Mascota actualizada con exito', 'OK', {
        duration: 1500,
        horizontalPosition: 'right',
      });
    }

  }
}
