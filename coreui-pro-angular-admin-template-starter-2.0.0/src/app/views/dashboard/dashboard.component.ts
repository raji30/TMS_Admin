import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  templateUrl: 'dashboard.component.html'
})
export class DashboardComponent implements OnInit {

  public pieChartLabels: string[] = ['Scheduled', 'Pending Verification', 'Dispatched','Completed'];
  public pieChartData: number[] = [30, 5, 10,5];
  public pieChartType = 'pie';
  public barChartOptions: any = {
    scaleShowVerticalLines: false,
    responsive: true
  };
  public barChartLabels: string[] = ['Dec 2018'];
  public barChartType = 'bar';
  public barChartLegend = true;

  public barChartData: any[] = [
    {data: [65], label: 'Total Orders'},
    {data: [28], label: 'Delivery in Progress'}
  ];
  ngOnInit(): void { }
}
