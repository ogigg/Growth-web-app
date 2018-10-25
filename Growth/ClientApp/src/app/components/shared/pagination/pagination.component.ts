import { Component, Input, 	Output, EventEmitter, OnInit, OnChanges, AfterViewChecked, AfterContentChecked, AfterViewInit } from '@angular/core';
import { PlantListComponent } from '../../plant-list/plant-list.component';

@Component({
  selector: 'pagination',
  //providers: [PlantListComponent],
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnChanges {
  @Input('total-items') totalItems ;
	@Input('page-size') pageSize = 10;
  @Output('page-changed') pageChanged = new EventEmitter();
  
	pages: any[];
  currentPage = 1; 
  

  constructor()  { }

  ngOnChanges() {
    var pagesCount = Math.ceil(this.totalItems / this.pageSize); 
		this.pages = [];
		for (var i = 1; i <= pagesCount; i++)
      this.pages.push(i);
    this.onChangePage(1);
    console.log(pagesCount);
    console.log(this);
  }

  onChangePage(page){
    this.currentPage=page;
    this.pageChanged.emit(page);
  }

  previousPage(){
    if(this.currentPage!=1){
      this.currentPage=this.currentPage-1;
      this.onChangePage(this.currentPage);
    }
  }
  nextPage(){
    if(this.currentPage!=this.pages.length){
      this.currentPage=this.currentPage+1;
      this.onChangePage(this.currentPage);
    }
      
  }

}
