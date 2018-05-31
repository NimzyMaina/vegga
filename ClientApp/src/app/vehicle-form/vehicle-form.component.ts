import { VehicleService } from './../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { ThrowStmt } from '@angular/compiler';
import { ToastyService } from 'ng2-toasty';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

  makes: any[];
  errors: any[];
  models: any[];
  features: any[];
  vehicle: any = {
    features: [],
    contact: {
      email: ''
    }
  };

  constructor(private vehicleService: VehicleService, private toasty: ToastyService) { }

  ngOnInit() {
    this.vehicleService.getMakes().subscribe(
      (res) => this.makes = res,
      (err) => console.log(err)      
    );

    this.vehicleService.getFeatures().subscribe(
      (res) => this.features = res
    );

  }

  onMakeChange() {
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
    delete this.vehicle.modelId;
  }

  onFeatureToggle(id, $event) {
    if($event.target.checked){
      this.vehicle.features.push(id);
    }else{
      var index = this.vehicle.features.indexOf(id);
      this.vehicle.features.splice(index,1);
    }

  }

  submit() {
    this.vehicleService.addVehicle(this.vehicle).subscribe(
      (success) => {
        this.toasty.success({
          title: 'Success',
          msg: 'Vehicle Added Successfully',
          theme: 'bootstrap',
          showClose: true,
          timeout: 5000
        }) 
      },
      (err) => {
        console.log(err._body);
        var response = JSON.parse(err._body);
        this.errors = response.errors;
        throw err;
      }
    );
  }

}
