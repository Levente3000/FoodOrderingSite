import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { MatMiniFabButton } from '@angular/material/button';

@Component({
	selector: 'app-file-upload',
	standalone: true,
	imports: [MatIcon, MatMiniFabButton],
	templateUrl: './file-upload.component.html',
	styleUrl: './file-upload.component.scss',
})
export class FileUploadComponent {
	@Input() public title = '';
	@Input() public file: File | null = null;
	@Output() fileChange = new EventEmitter<File | null>();

	onFileSelected(event: Event): void {
		const element = event.target as HTMLInputElement;
		this.file =
			element.files && element.files.length > 0 ? element.files[0] : null;
		this.fileChange.emit(this.file);
	}
}
