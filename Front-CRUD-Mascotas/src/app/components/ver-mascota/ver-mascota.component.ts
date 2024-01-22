import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Mascota } from 'src/app/interface/mascota';
import { MascotaService } from 'src/app/service/mascota.service';

@Component({
  selector: 'app-ver-mascota',
  templateUrl: './ver-mascota.component.html',
  styleUrls: ['./ver-mascota.component.css'],
})
export class VerMascotaComponent implements OnInit {
  id: number;
  mascota!: Mascota;
  loading: boolean = false;

  constructor(
    private _mascotaService: MascotaService,
    private aRoute: ActivatedRoute
  ) {
    this.id = parseInt(this.aRoute.snapshot.paramMap.get('id')!);
  }

  ngOnInit(): void {
    this.getMascotaById();
  }

  getMascotaById() {
    this.loading = true;
    this._mascotaService.getMascotaById(this.id).subscribe((data) => {
      this.mascota = data;
      this.loading = false;
    });
  }
}
