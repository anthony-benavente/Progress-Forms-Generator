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


CKEDITOR.plugins.add( 'simplebutton', {
	init: function( editor ) {

		var styles = '<!--[if IE]><html xmlns="http://www.w3.org/1999/xhtml" class="ie"><![endif]--><!--[if !IE]><!--><!--<![endif]--><meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/><meta name="format-detection" content="telephone=no"><meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=no;"><meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=7; IE=EDGE; chrome=1" /> <style> #outlook a { padding:0; } body{ width:100% !important; -webkit-text; size-adjust:100%; -ms-text-size-adjust:100%; margin:0; padding:0; } .ReadMsgBody { width: 100%; } .ExternalClass {width:100%;} .backgroundTable {margin:0 auto; padding:0; width:100%;!important;} table td {border-collapse: collapse;} .ExternalClass * {line-height: 115%;} @media screen and (max-width: 630px){ *[class="mobile-column"] {display: block;} *[class="mob-column"] {float: none !important;width: 100% !important;} *[class="hide"] {display:none !important;} *[class="100p"] {width:100% !important; height:auto !important;} *[class="condensed"] {padding-bottom:40px !important; display: block;} *[class="center"] {text-align:center !important; width:100% !important; height:auto !important;} *[class="100pad"] {width:100% !important; padding:20px;} *[class="100padleftright"] {width:100% !important; padding:0 20px 0 20px;} *[class="100padtopbottom"] {width:100% !important; padding:20px 0px 20px 0px;} } </style>';
		var carlosButton = '<div class="table-responsive" align="center"><style type="text/css">a {font-size: {{fontSize}}} a {font-family: {{fontFamily}};} a {color:{{color}};} a:link {color:{{color}}; background-color:transparent; text-decoration:none} a:visited {color:{{color}}; background-color:transparent; text-decoration:none} a:hover {color:{{color}}; background-color:transparent; text-decoration:none} a:active {color:{{color}}; background-color:transparent; text-decoration:none}</style><table border="0" cellpadding="0" cellspacing="0" class="table"><tbody> <tr> <td align="center" bgcolor="{{backgroundColor}}" style="border: {{border}}; padding: 12px 18px; border-radius: 3px; -webkit-border-radius: 3px;"><a href="{{href}}" rel="nofollow" style="font-weight: normal;" target="_blank">{{text}}</a></td> </tr></tbody></table></div></div>';
		
		
		// editor.addCommand( 'simplebutton', {
		// 	exec: function(editor) {
		// 		var text = prompt("Button text: ");
		// 		var href = prompt("Button url: ");
		// 		var options = {
		// 			backgroundColor: "#fff",
		// 			color: "#000",
		// 			text: text,
		// 			fontSize: "13px",
		// 			fontFamily: "Arial, Helvetica, sans-serif",
		// 			href: href,
		// 			border: "none"
		// 		};
		// 		var toInsert = carlosButton;
		// 		for (var key in options) {
		// 			toInsert = toInsert.replace(new RegExp("{{" + key + "}}", "g"), options[key]);
		// 		}
		// 		editor.insertHtml(toInsert);
		// 	}
		// } );
		// editor.ui.addButton( 'simplebutton', {
		// 	label: 'Simple Button',
		// 	command: 'simplebutton',
		// 	icon: this.path + 'images/simplebutton.png'
		// });
		editor.addCommand( 'simplebutton', new CKEDITOR.dialogCommand( 'simplebuttonDialog' ) );
		editor.ui.addButton( 'simplebutton', {
			label: 'Simple Button',
			command: 'simplebutton',
			icon: this.path + 'images/simplebutton.png'
		});
		editor.on( 'doubleclick', function( evt ) {
			var element = evt.data.element;
			if (element) {
				element = element.getAscendant({ table: 1 });
			}
			if ( element && element.hasClass('simple-button-plugin') ) {
				evt.data.dialog = 'simplebuttonDialog';
			}
		});

		CKEDITOR.dialog.add( 'simplebuttonDialog', this.path + 'dialogs/simplebutton.js' );

	}
});

