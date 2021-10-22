import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})


export class TableComponent implements OnInit {

  constructor(private http: HttpClient) { }


  headElements = ['Select', 'Firstname', 'Lastname', 'Email', 'Phone number', 'Position in office', 'Edit'];

  url = 'https://localhost:5001/api/EmployeeGet/basic-info/paged'

  ngOnInit(): void {
    this.showEmployess()
  }

  showEmployess() {
    let data: any = [];
    fetch(this.url).then(function (response) {
      return response.json();
    }).then(function (myJson) {
      data = myJson
      let table = document.getElementById("table")
      for (let i in data.data) {
        let row = document.createElement("tr");
        let checkbox = document.createElement("input");
        checkbox.setAttribute("type","checkbox");
        checkbox.setAttribute("class", "form-check-input");
        checkbox.setAttribute("style", "text-align:center; margin-left:8px;");
        let td = document.createElement("td")
        td.appendChild(checkbox)
        row.appendChild(td);

        let div1 = document.createElement("td");
        if (div1 != null) {
          div1.textContent = data.data[i].firstName;
          row.appendChild(div1);
        }
        let div2 = document.createElement("td");
        if (div2 != null) {
          div2.textContent = data.data[i].lastName;
          row.appendChild(div2);
        }
        let div3 = document.createElement("td");
        if (div3 != null) {
          div3.textContent = data.data[i].email;
          row.appendChild(div3);
        }
        let div4 = document.createElement("td");
        if (div4 != null) {
          div4.textContent = data.data[i].phoneNumber;
          row.appendChild(div4);
        }
        let div5 = document.createElement("td");
        if (div5 != null) {
          div5.textContent = data.data[i].position;
          row.appendChild(div5);
        }
        
        let button = document.createElement("button")
        row.appendChild(button)

        if (table != null){table.appendChild(row)}
      }
    })
  }

}
