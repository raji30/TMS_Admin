import { Component, OnInit, Input, SimpleChanges, OnChanges, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-tab',
  templateUrl: './tab.component.html',
  styleUrls: ['./tab.component.scss']
})
export class TabComponent implements OnInit ,OnChanges,OnDestroy{
  @Input() orderKey : string;
  subscription: Subscription;
  constructor(private router: Router,
    private route: ActivatedRoute) {this.orderKey = this.route.snapshot.paramMap.get("order");
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  ngOnInit() {
    this.orderKey = this.route.snapshot.paramMap.get("order");
    // this.orderKey ='43976812-5c31-11e9-be2a-93c1a1c5ac18';
  }

   ngOnChanges(changes: SimpleChanges) {
    this.orderKey = this.route.snapshot.paramMap.get("order");
    let newFocusedChallenge  = changes["orderKey"].currentValue;
  }
  ngOnDestroy()
{
  this.orderKey = null;
}
onClick(event) {
   alert('TEst');
}
}
