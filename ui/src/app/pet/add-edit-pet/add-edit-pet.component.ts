import { Component, OnInit, Input } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { BrowserModule } from '@angular/platform-browser'
import { getPetTypes } from 'src/app/common/common'
import { getErrorText } from 'src/app/common/common'

@Component({
  selector: 'app-add-edit-pet',
  templateUrl: './add-edit-pet.component.html',
  styleUrls: ['./add-edit-pet.component.css']
})
export class AddEditPetComponent implements OnInit {

  constructor(private service: SharedService) { }

  @Input() pet: any;
  name: string;
  type: number;
  ownerId: any;
  id: bigint;
  EmployeeList: any;
  petTypes: any;

  ngOnInit(): void {
    this.petTypes = getPetTypes();
    this.service.getEmployees().subscribe(data => {
      this.EmployeeList = data;
    });
    this.name = this.pet.name;
    this.type = this.pet.type;
    this.ownerId = this.pet.ownerId;
    this.id = this.pet.id;
  }

  addPet() {
    var val = {
      name: this.name,
      type: this.type,
      ownerId: this.ownerId
    };
    if(!this.validatePet(val))
      return;

    this.service.addPet(val).subscribe(res => {
        document.getElementById('closePetModal').click();
    }, err => {
      alert(getErrorText(err.error.errors));
    })
  }

  updatePet() {
    var val = {
      id: this.id,
      name: this.name,
      type: this.type,
      ownerId: this.ownerId
    };
    if(!this.validatePet(val))
      return;

    this.service.updatePet(this.id, val).subscribe(res => {
      document.getElementById('closePetModal').click();
    }, err => {
      alert(getErrorText(err.error.errors));
    })
  }

  validatePet(val) {
    var errors = '';
    if(val.name === undefined || val.name === null || val.name.length === 0) {
      errors += '\nPet name is not specified';
    }
    if(val.type === undefined || val.type === null) {
      errors += '\nType of animal is not specified';
    }
    if(val.ownerId === undefined || val.ownerId === null) {
      errors += '\nOwner is not specified';
    }
    if(errors.length > 0){
      /*trim leading newline char*/
      alert(errors.substring(1));
      return false;
    }
    return true;
  }
}
