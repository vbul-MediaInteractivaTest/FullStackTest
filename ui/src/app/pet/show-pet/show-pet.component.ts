import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { getPetTypes } from 'src/app/common/common'

@Component({
  selector: 'app-show-pet',
  templateUrl: './show-pet.component.html',
  styleUrls: ['./show-pet.component.css']
})
export class ShowPetComponent implements OnInit {

  constructor(private service: SharedService) { }

  PetList: any = [];
  ModalTitle: string;
  ActivateAddEditPetComp: boolean = false;
  pet: any;
  petTypes: any;

  ngOnInit(): void {
    var pTypes = {};
    for(let pType of getPetTypes()) {
      pTypes[pType.key] = pType.value;
    }
    this.petTypes = pTypes;
    this.refreshPetList();
  }
  addClick(){
    this.pet = {
      id: 0,
      name: "",
      type: undefined,
      owner: undefined
    };
    this.ModalTitle = "Add Pet";
    this.ActivateAddEditPetComp = true;
  }

  editClick(item) {
    this.pet = item;
    this.ModalTitle = "Edit Pet";
    this.ActivateAddEditPetComp = true;
  }

  closeClick() {
    this.ActivateAddEditPetComp = false;
    this.refreshPetList();
  }

  deleteClick(item) {
    if(confirm("Are you sure you want to delete this record?")) {
      this.service.deletePet(item.id).subscribe(res => {
        this.refreshPetList();
      }, err => {
        alert(err.error.errors);
      });
    }
  }
  
  refreshPetList() {
    this.service.getPets().subscribe(data => {
      this.PetList = data;
    });
  }
}
