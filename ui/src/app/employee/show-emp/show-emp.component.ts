import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { getPetTypes } from 'src/app/common/common'
import { getErrorText } from 'src/app/common/common'

@Component({
  selector: 'app-show-emp',
  templateUrl: './show-emp.component.html',
  styleUrls: ['./show-emp.component.css']
})
export class ShowEmpComponent implements OnInit {

  constructor(private service: SharedService) { }

  EmployeeList: any = [];
  ModalTitle: string;
  ActivateAddEditEmpComp: boolean = false;
  emp: any;
  petTypes: any;

  ngOnInit(): void {
    var pTypes = {};
    for(let pType of getPetTypes()) {
      pTypes[pType.key] = pType.value;
    }
    this.petTypes = pTypes;
    this.refreshEmployeeList();
  }

  addClick(){
    this.emp = {
      id: 0,
      name: "",
      lastname: "",
      isEmployee: true
    };
    this.ModalTitle = "Add Employee";
    this.ActivateAddEditEmpComp = true;
  }

  editClick(item) {
    this.emp = item;
    this.ModalTitle = "Edit Employee";
    this.ActivateAddEditEmpComp = true;
  }

  closeClick() {
    this.ActivateAddEditEmpComp = false;
    this.refreshEmployeeList();
  }

  deleteClick(item) {
    if(confirm("Are you sure you want to delete this record?")) {
      this.service.deleteEmployee(item.id).subscribe(res => {
        this.refreshEmployeeList();
      }, err => {
        alert(getErrorText(err.error.errors));
      });
    }
  }
  
  refreshEmployeeList() {
    this.service.getEmployees().subscribe(data => {
      this.EmployeeList = data;
    });
  }
}
