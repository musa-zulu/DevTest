import { Inject, Optional } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { User } from 'src/app/shared/models/user';

@Component({
  selector: 'app-add-edit-user-dialog-box',
  templateUrl: './add-edit-user-dialog-box.component.html',
  styleUrls: ['./add-edit-user-dialog-box.component.css']
})
export class AddEditUserDialogBoxComponent {

  action: string;
  localData: any;
  user: User = new User();

  constructor(    
    public dialogRef: MatDialogRef<AddEditUserDialogBoxComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: User
  ) {
    this.localData = { ...data };
    this.action = this.localData.action;
  }

  autoPopulateDOB(event) {
    let data = event.target.value.toLocaleLowerCase();
    if (data.length === 13) {
      var day = data.substring(4, 6);
      var month = data.substring(2, 4);
      var year = data.substring(0, 2);
      var century = this.getCentury(year);
      var fullYear = century + year;
      
      var dateOfBirth = fullYear + "/" + month + "/" + day;
      this.localData.dateOfBirth = dateOfBirth;
      this.localData.nationality = "South African"
    } else {
      this.localData.dateOfBirth = '';
      this.localData.nationality = ''
    }
  }

  getCentury(year: number)  {
    return Number((year) <= 20 ? "20" : "19");
  }

  doAction() {
    this.dialogRef.close({ event: this.action, data: this.localData });
  }

  closeDialog() {
    this.dialogRef.close({ event: "Cancel" });
  }
}
