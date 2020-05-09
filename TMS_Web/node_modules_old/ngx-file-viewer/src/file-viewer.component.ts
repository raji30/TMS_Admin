import { Component,
    OnInit,
    HostListener,
    Input } from '@angular/core';

@Component({
    moduleId: module.id,
    selector: 'file-viewer',
    templateUrl: './file-viewer.component.html',
    styleUrls: ['./file-viewer.component.css']
})
export class FileViewerComponent {

    @Input() public controls: boolean;
    @Input() public src: string;
    @Input() public width: number;
    @Input() public height: number; 
    @Input() public zoom: number = 1; 

    private x = 0;
    private y = 0;
    private mouseClick = false;

    constructor() { }

    ngOnInit() {
    }

    reset() {
        this.zoom = 1;
        this.x = 0;
        this.y = 0;
    }

    changeZoom(diff:number) {
        this.zoom += diff;
    }

    moveX(diff:number) {
        this.x += diff;
    }

    moveY(diff:number) {
        this.y += diff;
    }

    moveImage(event: MouseEvent) {
        if (this.mouseClick && event.button === 0) {
            this.x += event.movementX;
            this.y += event.movementY;
        }
    }

    @HostListener('mousedown', ['$event'])
    onMousedown(event:MouseEvent) {
        this.mouseClick = true;
    }

    @HostListener('mouseup', ['$event'])
    onMouseup(event:MouseEvent) {
        this.mouseClick = false;
    }

    @HostListener('wheel', ['$event'])
    onWheel(event:MouseWheelEvent) {
        this.changeZoom(event.wheelDelta/2400);
        return false;
    }
}
