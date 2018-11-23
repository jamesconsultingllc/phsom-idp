import { Component, OnInit } from '@angular/core';
import { DataManager, Query } from '@syncfusion/ej2-data';

const SERVICE_URI = 'http://js.syncfusion.com/demos/ejServices/Wcf/Northwind.svc/Orders';

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.scss']
})
export class StudentListComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
