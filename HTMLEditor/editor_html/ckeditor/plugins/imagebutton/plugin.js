/*

	This file is a part of simplebuttion project.

	Copyright (C) Thanh D. Dang <thanhdd.it@gmail.com>

	simplebuttion is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	simplebuttion is distributed in the hope that it will be useful, but
	WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
	General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program.  If not, see <http://www.gnu.org/licenses/>.

*/


CKEDITOR.plugins.add( 'imagebutton', {
    require: 'image',
	init: function( editor ) {
		editor.addCommand( 'imagebutton', new CKEDITOR.dialogCommand( 'image', {
            required: 'img[alt,src]',
            allowedContent: 'img[alt,dir,id,lang,longdesc,!src,title]{*}(*)',
            contentTransformations: [
                [ 'img{width}: sizeToStyle', 'img[width]: sizeToAttribute' ],
                [ 'img{float}: alignmentToStyle', 'img[align]: alignmentToAttribute' ]
            ]
        } ) );
		editor.ui.addButton( 'imagebutton', {
			label: 'Image Button',
			command: 'imagebutton',
			icon: this.path + 'images/simplebutton.png'
		});
		editor.on( 'doubleclick', function( evt ) {
			var element = evt.data.element;
            if (element.is('img') && element.hasClass('imageButton') && !element.data('cke-realelement') &&
                !element.isReadOnly()) {
                evt.data.dialog = 'image';
            }
		});
	}
});
