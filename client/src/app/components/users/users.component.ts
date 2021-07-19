import { Component, OnInit, ViewChild } from "@angular/core";
import { MatDialog, MatTable, MatTableDataSource } from "@angular/material";
import { User } from "src/app/shared/models/user";
import { AlertService } from "src/app/shared/services/alert.service";
import { UsersService } from "src/app/shared/services/users.service";
import { AddEditUserDialogBoxComponent } from "./add-edit-user-dialog-box/add-edit-user-dialog-box.component";


@Component({
  selector: "app-users",
  templateUrl: "./users.component.html",
  styleUrls: ["./users.component.css"],
})
export class UsersComponent implements OnInit {
  users: User[];
  user: User = new User();  
  pageLength: number = 0;

  @ViewChild(MatTable, { static: true }) table: MatTable<any>;
  
  filteredUsers: User[] = [];
  displayedColumns: string[] = [    
    "firstName",
    "lastName",
    "idNumberOrPassport",
    "nationality",
    "emailAddress",
    "phoneNumber",
    "dateOfBirth",
    "action",
  ];
  tableDataResource = new MatTableDataSource<User>();  
  userDetails: string;

  constructor(
    private _usersService: UsersService,
    public dialog: MatDialog,
    protected _alertService: AlertService    
  ) {}

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this._usersService.getUsers().subscribe((users) => {
      this.users = users;
      console.log();
      this.pageLength = this.users.length;
      this.onPageChanged(null);
    });
  }

  openDialog(action: any, user) {
    user.action = action;
    let width = "400px"; 
    let height = "800px"
    if (action === 'Delete') {
      width = "350px";
      height = "350px";
    }
    const dialogRef = this.dialog.open(AddEditUserDialogBoxComponent, {
      width: width,
      height: height,
      data: user,
    });

    dialogRef.afterClosed().subscribe((result) => {      
      if (result.event !== "Cancel") {        
        let regex: RegExp = new RegExp(/^[0-9]+(\.[0-9]*){0,1}$/g);
        let isValid = (result.data.firstName !== "" && result.data.lastName !== "" 
              && result.data.emailAddress !== "" && result.data.iDNumberOrPassport !== "");
        if (regex.test(result.data.phoneNumber) || result.data.phoneNumber === "" || result.data.phoneNumber === undefined) {
          if (action === "Add" && isValid) {
            this.addUser(result.data);
          } else if (action === "Update" && isValid) {
            this.updateUser(result.data);
          } else if (action === "Delete") {
            this.deleteUser(result.data);
          }
        } else {
          this._alertService.error("Invalid input!!!");
        }
      }
    });
  }

  async addUser(user: User) {
    await this._usersService
      .addUser(user)
      .then((result) => {
        this._alertService.success("User was saved successfully !!");        
      })
      .catch((error) => {
        this._alertService.error("Data was not saved, please try again");
      });
    this.refreshTable();
  }

  async updateUser(user: User) {
    this._usersService
      .updateUser(user)
      .then((result) => {
        this._alertService.success("User was updated successfully !!");        
      })
      .catch((error) => {
        this._alertService.error("Data was not updated, please try again");
      });
    this.refreshTable();
  }

  async deleteUser(user: User) {
    this._usersService
      .deleteUser(user)
      .then((result) => {
        this._alertService.success("User was deleted successfully !!");        
      })
      .catch((error) => {
        this._alertService.error("Data was not deleted, please try again");
      });
    this.refreshTable();
  }

  refreshTable() {
    this.getUsers();
  }

  private initializeTable(users: User[]) {
    this.tableDataResource = new MatTableDataSource<User>(users);
  }

  filter(query: any) {
    let searchTerm = query.target.value.toLocaleLowerCase();
    const filteredUsers = searchTerm
      ? this.users.filter((p) => p.firstName.toLowerCase().includes(searchTerm))
      : this.users;

    this.initializeTable(filteredUsers);
  }

  onPageChanged(e: { pageIndex: number; pageSize: number }): void {
    let filteredUsers = [];
    if (e == null) {
      let firstCut = 0;
      let secondCut = firstCut + 10;
      filteredUsers = this.users.slice(firstCut, secondCut);
    } else {
      let firstCut = e.pageIndex * e.pageSize;
      let secondCut = firstCut + e.pageSize;
      filteredUsers = this.users.slice(firstCut, secondCut);
    }
    this.initializeTable(filteredUsers);
  }
}
