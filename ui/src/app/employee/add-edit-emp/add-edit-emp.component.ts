import { Component, OnInit, Input } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { getErrorText } from 'src/app/common/common'

@Component({
  selector: 'app-add-edit-emp',
  templateUrl: './add-edit-emp.component.html',
  styleUrls: ['./add-edit-emp.component.css']
})
export class AddEditEmpComponent implements OnInit {

  constructor(private service: SharedService) { }

  @Input() emp: any;
  name: string;
  lastname: string;
  isEmployee: boolean;
  id: bigint;

  ngOnInit(): void {
    this.name = this.emp.name;
    this.lastname = this.emp.lastname;
    this.isEmployee = this.emp.isEmployee;
	this.id = this.emp.id;
  }

  addEmployee() {
    var val = {
      name: this.name,
      lastname: this.lastname,
      isEmployee: this.isEmployee
    };
    if(!this.validateEmployee(val))
      return;

    this.service.addEmployee(val).subscribe(res => {
        document.getElementById('closeEmpModal').click();
    }, err => {
      alert(getErrorText(err.error.errors));
    })
  }

  updateEmployee() {
    var val = {
      id: this.id,
      name: this.name,
      lastname: this.lastname,
      isEmployee: this.isEmployee
    };
    if(!this.validateEmployee(val))
      return;

    this.service.updateEmployee(this.id, val).subscribe(res => {
      document.getElementById('closeEmpModal').click();
    }, err => {
      alert(getErrorText(err.error.errors));
    })
  }

  validateEmployee(val) {
    var errors = '';
    if(val.name === undefined || val.name === null || val.name.length === 0) {
      errors += '\nName is not specified';
    }
    if(val.lastname === undefined || val.lastname === null || val.lastname.length === 0) {
      errors += '\nLast Name is not specified';
    }
    if(errors.length > 0){
      /*trim leading newline char*/
      alert(errors.substring(1));
      return false;
    }
    return true;
  }
}
